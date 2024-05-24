using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScripts : RPGMonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private QuestGiver questGiver;
    protected override void LoadComponents()
    {
        this.LoadAnimator();
        this.LoadQuestGiver();
    }
    private void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInParent<Animator>();
        Debug.LogWarning(transform.name + "|LoadNpcAnim|", gameObject);

    }
    private void LoadQuestGiver()
    {
        if (this.questGiver != null) return;
        this.questGiver = GetComponent<QuestGiver>();
        Debug.LogWarning(transform.name + "|LoadQuestGiver|", gameObject);

    }
    private void Start()
    {
        questGiver.messageBox.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        this.questGiver.messageBox.GetComponent<MessageManager>().numbShop = questGiver.shopNumber;
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Stay", true);
            questGiver.messageBox.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questGiver.messageBox.GetComponent<MessageManager>().firstTask.SetActive(true);
            questGiver.messageBox.GetComponent<MessageManager>().HideButton();
            questGiver.isFullText = false;
            questGiver.dialogueIndex = 0;
            questGiver.ShowDialogue();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Stay", false);
            questGiver.messageBox.SetActive(false);
            questGiver.messageBox.GetComponent<MessageManager>().shops[questGiver.shopNumber].SetActive(false);
            questGiver.messageBox.GetComponent<MessageManager>().firstTask.SetActive(false);
            questGiver.messageBox.GetComponent<MessageManager>().questTask.SetActive(false);
        }
    }



}

