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
    public static bool shieldOn;
    public static bool strengthOn;
    public static GameObject theTarget;
    private RaycastHit raycastHit;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerSO();
        this.LoadInteractor();
        this.LoadPlayerAttack();
        this.LoadUsingSkill();
        this.LoadPlayerAnim();
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

    }
    protected virtual void LoadPlayerAnim()
    {
        if (this.playerAnim != null) return;
        this.playerAnim = GetComponent<PlayerAnim>();
    }
    protected override string GetObjectTypeString()
    {
        return ObjectType.Player.ToString();
    }
    private void Update()
    {
        SlectionTarget();//turn outline and set target
    }
    public void SlectionTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click Mouse");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit,50))
            {
                if (raycastHit.transform.gameObject.CompareTag("Enemy"))
                {
                    theTarget = raycastHit.transform.gameObject;
                    transform.LookAt(theTarget.transform);//Cho player keke
                    Debug.Log("Enemy Set Target" + theTarget.name);
                }
                else
                {
                    theTarget = null;

                }
            }
        }

    }
}


