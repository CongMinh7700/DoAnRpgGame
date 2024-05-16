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
        questTitleText.text = quest.questTitle;
    }
    public void OnQuestItemClicked()
    {
        QuestManager.Instance.DisplayQuestDetails(quest);
    }
}
