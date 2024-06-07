using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Save Quest Index đi :V
public class QuestGiver : RPGMonoBehaviour
{
    [SerializeField] public int shopNumber;
    public GameObject messageBox;
    //Dialogues
    [Header("Talk & Quest")]
    [SerializeField] protected Quest[] quests;
    [SerializeField] public int questIndex = 0;
    public int dialogueIndex;
    public bool isAnimatingText = false;
    private Coroutine textAnimationCoroutine;
    //Full Text không cho F nữa
    public bool isFullText = false;
    [SerializeField] private bool noQuest = false;
    [SerializeField] private bool canNotification;
    [SerializeField] private bool notificated;
    [SerializeField] private bool nextQuest;
    //Name của npc
    [SerializeField] private QuestIndexManager questIndexManager;

    protected override void LoadComponents()
    {
        LoadIndexQuestManager();
    }
    protected virtual void LoadIndexQuestManager()
    {
        if (this.questIndexManager != null) return;
        this.questIndexManager = GetComponent<QuestIndexManager>();
    }
    private void Start()
    {
        if (!SaveGame.newGame)
        {
            questIndexManager.LoadData();
        }
        else
        {
            ResetAllQuest.Instance.ResetQuests();
            questIndexManager.DeleteData();
        }

    }
    private void Update()
    {
        if (quests.Length <= 0) noQuest = true;
       // Debug.LogWarning("Message Accept" + MessageManager.isAccept);
        if (MessageManager.isAccept)
        {
            ShowDialogue();
            StartCoroutine(WaitToFalse());
            SaveGame.Instance.Save();
        }
        if (messageBox.GetComponent<MessageManager>().questTalk.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isAnimatingText)//&& !isFullText)
            {
                ShowDialogue();
            }
        }
        if (nextQuest)
        {
            ShowDialogue();
            nextQuest = false;
            Debug.Log("Cal Next Quest Update");
        }
        if (quests[questIndex].questState == QuestState.NotStarted && isFullText)
        {
            messageBox.GetComponent<MessageManager>().ShowButton();
            isFullText = false;
        }
        Notification();
    }
    public void Notification()
    {
        if (quests.Length <= 0) return;
        if (quests[questIndex].questState == QuestState.Complete)
        {
            canNotification = true;
            if (canNotification && !notificated && !noQuest)
            {
                notificated = true;
                canNotification = false;
                SpawnNotification();
            }
        }
    }
    public void ShowDialogue()
    {

        string[] dialogues = new string[0];
        if (this.shopNumber != messageBox.GetComponent<MessageManager>().numbShop) return;
        if (quests.Length <= 0)
        {
            messageBox.GetComponent<MessageManager>().currentQuest = null;
            Debug.Log("No Quest");
            noQuest = true;
        }
        else
        {
            messageBox.GetComponent<MessageManager>().currentQuest = quests[questIndex];
            Debug.Log("Title :" + quests[questIndex]);
            Debug.Log("QuestState : " + quests[questIndex].questState.ToString() + "QuestName :" + quests[questIndex].questTitle);

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
                  //  Debug.LogWarning("DialoguesLength" + dialogues.Length);
                    break;
            }
        }
        FullTextCheck(dialogues);

        if (textAnimationCoroutine != null)
            StopCoroutine(textAnimationCoroutine);


        string fullText = "";
        if (noQuest)
        {
            if (shopNumber > 4)
            {
                fullText = "Bạn đã tiêu diệt được bọn tay sai của quỷ vương rồi cảm ơn cậu. Tôi đã mở cổng dịch chuyển ở thị trấn rồi cậu hãy tới đó để giúp những người dân làng tội nghiệp khác .Chúc may mắn !";
            }
            else
            {
                fullText = "Tôi không còn nhiệm vụ nào cho bạn nữa .Hãy thường xuyên ghé qua đây nhé";
            }
            messageBox.GetComponent<MessageManager>().Refuse();
        }
        else
        {
            fullText = dialogues[dialogueIndex];
        }

        dialogueIndex++;
        textAnimationCoroutine = StartCoroutine(AnimateText(fullText));
        NextQuest();
        QuestInProgress();

    }
    public void FullTextCheck(string[] dialogues)
    {
        if (dialogueIndex >= dialogues.Length - 1 && (quests[questIndex].questState == QuestState.NotStarted || quests[questIndex].questState == QuestState.InProgress))
        {
            //Debug.LogWarning("Length -1 :" + (dialogues.Length - 1));
            dialogueIndex = dialogues.Length - 1;
            isFullText = true;
        }
        if (dialogueIndex >= dialogues.Length && quests[questIndex].questState == QuestState.Complete)
        {
          //  Debug.LogWarning("Length -1 :" + (dialogues.Length - 1));
            dialogueIndex = dialogues.Length - 1;
            isFullText = true;
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
    public virtual void NextQuest()
    {
        if (noQuest) return;
        if (isFullText && quests[questIndex].questState == QuestState.Complete)
        {

            MoneyManager.Instance.AddGold(quests[questIndex].goldReward);
            LevelSystem.Instance.GainExperienceFlatRate(quests[questIndex].experienceReward);
            Debug.Log("EXP : " + quests[questIndex].experienceReward);
            questIndex++;
            //QuestManager.Instance.RemoveQuest(quests[questIndex]);
            dialogueIndex = 0;
            notificated = false;
            isFullText = false;
            nextQuest = true;
            questIndexManager.SaveData();//Save QuestIndex
            if (questIndex >= quests.Length - 1)
            {
                questIndex = quests.Length - 1;
                noQuest = true;
            }

            messageBox.GetComponent<MessageManager>().Refuse();
        }
    }
    public virtual void QuestInProgress()
    {
        if (noQuest) return;
        if (quests[questIndex].questState == QuestState.InProgress && isFullText)
        {
            dialogueIndex = 0;
            isFullText = false;
            messageBox.GetComponent<MessageManager>().Refuse();
            Debug.Log("Call Check");

        }
    }
    protected virtual void SpawnNotification()
    {
        string fxName = FXSpawner.notification;
        Transform fxObj = FXSpawner.Instance.Spawn(fxName, transform.position, Quaternion.identity);
        NotificationText nofText = fxObj.GetComponentInChildren<NotificationText>();
        nofText.SetText("Hoàn thành nhiệm vụ");
       // Debug.Log("Quest Notification Called" + transform.parent.name);
        fxObj.gameObject.SetActive(true);
    }
    IEnumerator WaitToFalse()
    {
        yield return new WaitForSeconds(0.2f);
        MessageManager.isAccept = false;
    }
}
