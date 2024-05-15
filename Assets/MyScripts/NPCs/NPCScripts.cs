using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScripts : RPGMonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] protected GameObject messageBox;
    [SerializeField] public int shopNumber;
    public static bool canHide ;
    //Dialogues
    public Quest[] quests;
    
    //Name của npc
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
    private void Start()
    {
        messageBox.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        this.messageBox.GetComponent<MessageManager>().numbShop = shopNumber;
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Stay", true);
            messageBox.SetActive(true);

            canHide = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageBox.GetComponent<MessageManager>().firstTask.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Stay", false);
            messageBox.SetActive(false);
            messageBox.GetComponent<MessageManager>().shops[shopNumber].SetActive(false);
            messageBox.GetComponent<MessageManager>().firstTask.SetActive(false);
            messageBox.GetComponent<MessageManager>().questTask.SetActive(false);
        }
    }

}
