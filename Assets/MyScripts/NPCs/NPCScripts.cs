using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScripts : RPGMonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] protected GameObject messageBox;
    [SerializeField] public int shopNumber;
    public static bool canHide ;
    protected override void LoadComponents()
    {
        this.LoadAnimator();
    }
    private void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInParent<Animator>();
        Debug.LogWarning(transform.name + "|LoadNpcAnim|", gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        this.messageBox.GetComponentInChildren<MesageScripts>().numbShop = shopNumber;
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Stay", true);
            messageBox.SetActive(true);
            canHide = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Stay", false);
            messageBox.SetActive(false);
            messageBox.GetComponentInChildren<MesageScripts>().shops[shopNumber].SetActive(false);
           
        }
    }
}
