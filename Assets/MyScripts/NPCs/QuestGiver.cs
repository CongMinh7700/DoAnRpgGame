using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Save Quest Index đi :V
public class QuestGiver : MonoBehaviour
{
    [SerializeField] public int shopNumber;
    public GameObject messageBox;
    //Dialogues
    [Header("Talk & Quest")]
    [SerializeField] protected Quest[] quests;
    [SerializeField] public int questIndex = 0;
    public int dialogueIndex;
    private bool isAnimatingText = false;
    private Coroutine textAnimationCoroutine;
    //Full Text không cho F nữa
    public bool isFullText = false;
    [SerializeField] private bool noQuest = false;
    [SerializeField] private bool canNotification;
    [SerializeField] private bool notificated;
    //Name của npc
    private void Update()
    {
        if (messageBox.GetComponent<MessageManager>().questTask.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isAnimatingText && !isFullText)
            {
                ShowDialogue();
            }
        }
        if (quests[questIndex].questState == QuestState.Complete)
        {
            canNotification = true;
            if (canNotification && !notificated && !noQuest)
            {
                notificated = true;
                canNotification = false;
                QuestNotificationComplete();
            }


        }
    }
    public void ShowDialogue()
    {
        if (this.shopNumber != messageBox.GetComponent<MessageManager>().numbShop) return;
        string[] dialogues = new string[0];
        if (quests[questIndex] == null) Debug.Log("No Quest");
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

        if (dialogueIndex >= dialogues.Length)
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
        NextQuest();
        string fullText = "";
        if (noQuest)
        {
            fullText = "Tôi không còn nhiệm vụ nào cho bạn nữa .Hãy thường xuyên ghé qua đây để mua đồ nhé";
            messageBox.GetComponent<MessageManager>().Refuse();
        }
        else
        {
            fullText = dialogues[dialogueIndex];
        }
        textAnimationCoroutine = StartCoroutine(AnimateText(fullText));

        dialogueIndex++;
        QuestInProgress();

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
    public virtual void NextQuest()
    {
        if (isFullText && quests[questIndex].questState == QuestState.Complete)
        {
            MoneyManager.Instance.AddGold(quests[questIndex].goldReward);//GiveReward
            QuestManager.Instance.RemoveQuest(quests[questIndex]);
            questIndex++;
            dialogueIndex = 0;
            notificated = false;
            isFullText = false;
            if (questIndex >= quests.Length)
            {
                questIndex = quests.Length - 1;
                noQuest = true;
                // notificated = true;
            }
            messageBox.GetComponent<MessageManager>().Refuse();
        }
    }
    public virtual void QuestInProgress()
    {
        if (quests[questIndex].questState == QuestState.InProgress && isFullText)
        {
            messageBox.GetComponent<MessageManager>().Refuse();
            dialogueIndex = 0;
            isFullText = false;
            Debug.Log("Call Check");
            ShowDialogue();

        }
    }
    protected virtual void QuestNotificationComplete()
    {
        string fxName = FXSpawner.notification;
        Transform fxObj = FXSpawner.Instance.Spawn(fxName, transform.position, Quaternion.identity);
        NotificationText nofText = fxObj.GetComponentInChildren<NotificationText>();
        nofText.SetText("Hoàn thành nhiệm vụ");
        Debug.Log("Quest Notification Called");
        fxObj.gameObject.SetActive(true);
    }

    protected virtual void SaveQuest()
    {
        //Save quest Index;
        //Save quest index theo numbshop (ví dụ black smith thì cho vào 1 cái riêng)
    }

    protected virtual void QuestPointer()
    {
        //Hiển thị trên canvas ,
        //Hiển thị trên map
        //if(questComplete) => !
        //if(haveNextQuest) => ?
        //if(noquest) => null
    }
}
