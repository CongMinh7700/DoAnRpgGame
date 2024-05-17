using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScripts : RPGMonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private QuestGiver questGiver;
    //[SerializeField] protected GameObject messageBox;
    //[SerializeField] public int shopNumber;
    ////Dialogues
    //[Header("Talk & Quest")]
    //public Quest[] quests;
    //public int questIndex = 0;
    //public int dialogueIndex;
    //private bool isAnimatingText = false;
    //private Coroutine textAnimationCoroutine;
    ////Full Text không cho F nữa
    //[SerializeField] private bool isFullText = false;
    //[SerializeField] private bool noQuest = false;
    ////Name của npc
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
            questGiver.ShowDialogue();
            questGiver.messageBox.GetComponent<MessageManager>().firstTask.SetActive(true);
            questGiver.isFullText = false;
            questGiver.messageBox.GetComponent<MessageManager>().HideButton();
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

