using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string name;
    public string description;
    public Quest[] subQuests;
    public string hint;

    private bool completed;

    public bool IsQuestCompleted() => completed;
    public void CompleteQuest() => completed = true;

    public override string ToString()
    {
        return name + "\n" + description + "\n Sub-Quests Available: " + subQuests.Length; 
    }
}