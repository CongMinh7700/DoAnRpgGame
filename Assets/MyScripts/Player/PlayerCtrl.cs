using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCtrl : HitableObjectCtrl
{
    [Header("PlayerSO")]
    [SerializeField ]protected PlayerSO playerSO;
    public PlayerSO PlayerSO => playerSO;
    [SerializeField] protected Interactor interactor;
    public Interactor Interactor => interactor;
    [SerializeField] protected Transform spawnPoint;
    public Transform SpawnPoint => spawnPoint;
    [SerializeField] protected PlayerAttack playerAttack;
    public PlayerAttack PlayerAttack => playerAttack;
    [SerializeField] protected UsingSkill usingSkill;
    public UsingSkill UsingSkill => usingSkill;
    [SerializeField] protected PlayerAnim playerAnim;
    public PlayerAnim PlayerAnim => playerAnim;
    [SerializeField] protected PlayerSFX playerSFX;
    public PlayerSFX PlayerSFX => playerSFX;
    public static bool shieldOn;
    public static bool strengthOn;
    public static GameObject theTarget;
    private RaycastHit raycastHit;
    public  bool usingPortal;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerSO();
        this.LoadInteractor();
        this.LoadPlayerAttack();
        this.LoadUsingSkill();
        this.LoadPlayerAnim();
        this.LoadPlayerSFX();
    }
    protected virtual void LoadPlayerSO()
    {
        if (this.playerSO != null) return;
        string resPath = "HitableObject/" + this.GetObjectTypeString() + "/" + transform.name;
        this.playerSO = Resources.Load<PlayerSO>(resPath);
        //Debug.LogWarning(transform.name + "||LoadSO||" + resPath, gameObject);
    }
    public virtual void LoadInteractor()
    {
        if (this.interactor != null) return;
        this.interactor = GetComponent<Interactor>();
    }
    protected virtual void LoadPlayerAttack()
    {
        if (this.playerAttack != null) return;
        this.playerAttack = GetComponentInChildren<PlayerAttack>();
    }
    protected virtual void LoadUsingSkill()
    {
        if (this.usingSkill != null) return;
        this.usingSkill = GetComponentInChildren<UsingSkill>();
        spawnPoint = usingSkill.transform;

    }
    protected virtual void LoadPlayerAnim()
    {
        if (this.playerAnim != null) return;
        this.playerAnim = GetComponent<PlayerAnim>();
    }
    protected virtual void LoadPlayerSFX()
    {
        if (this.playerSFX != null) return;
        this.playerSFX = GetComponentInChildren<PlayerSFX>();
    }
    protected override string GetObjectTypeString()
    {
        return ObjectType.Player.ToString();
    }


    private void Update()
    {
        Debug.Log("Using Portal :" + usingPortal);
        SelectionTarget();//turn outline and set target
    }
    public void SelectionTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click Mouse");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 50, Color.red, 2f);
            if (Physics.Raycast(ray, out raycastHit, 50))
            {
                if (raycastHit.transform.gameObject.CompareTag("Enemy"))
                {
                    theTarget = raycastHit.transform.gameObject;
                    Vector3 targetPosition = new Vector3(theTarget.transform.position.x, transform.position.y, theTarget.transform.position.z);
                    transform.LookAt(targetPosition); // Chỉ xoay theo trục Y
                    Debug.LogWarning("Enemy Set Target: " + theTarget.name);
                }
                else
                {
                    theTarget = null;
                    Debug.Log("No target set.");
                }
            }
        }
    }

    public virtual void PlaySound()
    {
        playerSFX.audioSource.Play();
    }
}


