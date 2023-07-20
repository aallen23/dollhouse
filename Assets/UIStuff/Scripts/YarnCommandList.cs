using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnCommandList : MonoBehaviour
{
    [YarnCommand("addQuest")]
    public void AddQuest(string questName)
    {
        QuestManager.Instance.AddQuest(questName);
    }

    [YarnCommand("updateQuest")]
    public void UpdateQuest(string questName)
    {
        QuestManager.Instance.ProgressQuest(questName);
    }
}
