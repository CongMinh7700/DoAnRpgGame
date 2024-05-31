using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//nên để trong quest MAnager
public class ResetAllQuest : MonoBehaviour
{

    // Singleton pattern
    protected static ResetAllQuest instance;
    public static ResetAllQuest Instance => instance;

    private void Awake()
    {
        if (ResetAllQuest.instance != null) Debug.LogWarning("Only 1 ResetAllQuest Allow to exits");
        ResetAllQuest.instance = this;
    }

    public void ResetQuests()
    {
        try
        {
            Quest[] allQuests = Resources.LoadAll<Quest>(""); // Load tất cả các ScriptableObject Quest

            foreach (Quest quest in allQuests)
            {
                quest.questState = QuestState.NotStarted; // Đặt trạng thái về NotStarted
                quest.currentCount = 0; // Đặt lại currentCount về 0
            }

            Debug.Log("Quests have been reset.");
        }
        catch
        {
            Debug.LogWarning("<color=green>Kiểm tra lại đi cha nội,không load được quest nè </color>");
        }
    
    }


}
