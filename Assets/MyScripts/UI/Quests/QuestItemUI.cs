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
    public void SetQuest(Quest quest)
    {
        this.quest = quest;
        if(quest.questState == QuestState.Complete)
        {
            Debug.Log("Quest Complete");
            questTitleText.text = "<s>" + quest.questTitle + "</s>\n"; ;
            questTitleText.color = Color.yellow;
        }
        else if(quest.questState == QuestState.InProgress)
        {
            questTitleText.text = quest.questTitle;
            questTitleText.color = Color.white;
            //  questTitleText.fontStyle = FontStyles.Normal;
        }
        else
        {
            questTitleText.text = quest.questTitle;
            questTitleText.color = Color.white;
        }

    }
    public void OnQuestItemClicked()
    {
        SFXManager.Instance.PlaySFXClick();
        QuestManager.Instance.DisplayQuestDetails(quest);
    }
}
