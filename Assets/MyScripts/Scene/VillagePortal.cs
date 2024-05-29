using UnityEngine;
using UnityEngine.SceneManagement;

public class VillagePortal : RPGMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    protected override void LoadComponents()
    {
        LoadPlayerCtrl();
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (playerCtrl != null) return;
        this.playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
    }
    private void Start()
    {
        if (!playerCtrl.usingPortal)
        {
            playerCtrl.transform.position = new Vector3(62, 27, 302);
            //playerCtrl.usingPortal = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(3);
            playerCtrl.usingPortal = true;
        }
    }
  
}
