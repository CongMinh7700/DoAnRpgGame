using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScripts : RPGMonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] protected GameObject messageBox;
    [SerializeField] public int shopNumber;
    //Dialogues
    [Header("Talk & Quest")]
    public Quest[] quests;
    public int questIndex = 0;
    public int dialogueIndex;
    private bool isAnimatingText = false;
    private Coroutine textAnimationCoroutine;
    //Full Text không cho F nữa
    [SerializeField] private bool isFullText = false;
    [SerializeField] private bool noQuest = false;
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

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueIndex = 0;
            ShowDialogue();
            messageBox.GetComponent<MessageManager>().firstTask.SetActive(true);
            isFullText = false;
            messageBox.GetComponent<MessageManager>().HideButton();
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

    private void Update()
    {
        if (messageBox.GetComponent<MessageManager>().questTask.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isAnimatingText && !isFullText)
            {
                ShowDialogue();
            }
        }
    }

    //Xử lý back ra FirstTask là oke

    public void ShowDialogue()
    {
        if (this.shopNumber != messageBox.GetComponent<MessageManager>().numbShop) return;
        string[] dialogues = new string[0];
        if (quests[questIndex] == null) return;
        messageBox.GetComponent<MessageManager>().currentQuest = quests[questIndex];
        Debug.Log("QuestState : " + quests[questIndex].questState.ToString());

        switch (quests[questIndex].questState)
        {
            case QuestState.NotStarted:
                dialogues = quests[questIndex].dialogues;
                break;
            case QuestState.InProgress:
                dialogues = quests[questIndex].dialoguesInProgress;
                break;
            case QuestState.Complete:
                dialogues = quests[questIndex].dialoguesComplete;

                break;
        }

        if (dialogueIndex >= dialogues.Length - 1)
        {
            isFullText = true;
            dialogueIndex = dialogues.Length - 1;

        }
        if (textAnimationCoroutine != null)
            StopCoroutine(textAnimationCoroutine);
        if (quests[questIndex].questState == QuestState.NotStarted && isFullText)
        {
            messageBox.GetComponent<MessageManager>().ShowButton();
        }

        string fullText = dialogues[dialogueIndex];
        if (noQuest) fullText = "Tôi không còn nhiệm vụ nào cho bạn nữa .Hãy thường xuyên ghé qua đây để mua đồ nhé";
        textAnimationCoroutine = StartCoroutine(AnimateText(fullText));
        dialogueIndex++;
        if (isFullText && quests[questIndex].questState == QuestState.Complete)
        {
            questIndex++;
            dialogueIndex = 0;
        }
        if (questIndex > quests.Length - 1)
        {
            questIndex = questIndex - 1;
            noQuest = true;
        }
    }
    IEnumerator AnimateText(string fullText)
    {
        isAnimatingText = true;
        string displayedText = "";
        for (int i = 0; i <= fullText.Length; i++)
        {
            displayedText = fullText.Substring(0, i);
            messageBox.GetComponent<MessageManager>().dialogeText.text = displayedText;
            yield return new WaitForSeconds(0.03f);
        }
        isAnimatingText = false;
    }
}
