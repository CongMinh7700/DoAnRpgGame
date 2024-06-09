using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestItemUI : RPGMonoBehaviour
{
    public TextMeshProUGUI questTitleText;
    public Quest quest;
    private Button button; 
    protected override void LoadComponents()
    {
        LoadText();
        LoadButton(); 
    }
    public virtual void LoadText()
    {
        if (this.questTitleText != null) return;
        this.questTitleText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public virtual void LoadButton()
    {
        if (this.button != null) return;
        this.button = GetComponent<Button>();

    }
    public void SetQuest(Quest quest)//,QuestState questState)
    {
       // Debug.Log($"Quest {quest.questTitle} set with state {questState}" + " Quest State :" + quest.questState);
        this.quest = quest;

       // this.quest.questState = questState;
        
       // Debug.Log($"BeLow Quest {quest.questTitle} set with state {questState}" + " Quest State :"+ quest.questState);
        if (quest.questState == QuestState.Complete)
        {
            Debug.Log("Quest Complete");
            questTitleText.text = "<s>" + quest.questTitle + "</s>\n"; ;
            questTitleText.color = Color.yellow;
        }
        else 
        {
            questTitleText.text = quest.questTitle;
            questTitleText.color = Color.white;
            //  questTitleText.fontStyle = FontStyles.Normal;
        }
     

    }
    public void LoadSetQuest(Quest quest,int currentCount)//,QuestState questState)
    {
        // Debug.Log($"Quest {quest.questTitle} set with state {questState}" + " Quest State :" + quest.questState);
        this.quest = quest;
        this.quest.currentCount = currentCount;
        // this.quest.questState = questState;

        // Debug.Log($"BeLow Quest {quest.questTitle} set with state {questState}" + " Quest State :"+ quest.questState);
        if (quest.questState == QuestState.Complete)
        {
            Debug.Log("Quest Complete");
            questTitleText.text = "<s>" + quest.questTitle + "</s>\n"; ;
            questTitleText.color = Color.yellow;
        }
        else
        {
            questTitleText.text = quest.questTitle;
            questTitleText.color = Color.white;
            //  questTitleText.fontStyle = FontStyles.Normal;
        }


    }
    public void OnQuestItemClicked()
    {
        SFXManager.Instance.PlaySFXClick();
        QuestManager.Instance.DisplayQuestDetails(quest);
    }
}
