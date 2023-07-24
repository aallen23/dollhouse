using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField, Tooltip("The list of quests in the game.")] private Quest[] masterQuestList;

    public static QuestManager Instance;

    internal List<Quest> foundQuests = new List<Quest>();
    private int activeQuestsNum;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Adds a quest to the list of active quests.
    /// </summary>
    /// <param name="questName">The name of the master quest.</param>
    public void AddQuest(string questName)
    {
        //If the quest has already been added, return
        if (foundQuests.Contains(FindQuest(questName)))
            return;

        Quest newQuest = FindQuest(questName);

        //If the quest is found in the master list of quests, add it to the list
        if (newQuest != null)
        {
            foundQuests.Add(newQuest);
            Debug.Log("Quest Added: " + newQuest.ToString());
            GameObject.FindGameObjectWithTag("QuestPopup").GetComponent<PopupBox>().ShowPopup("<size=70>New Quest Added.</size>\n" + newQuest.name.ToString());
            activeQuestsNum++;
        }
    }

    /// <summary>
    /// Progresses the quest, focusing on sub quests before completing the main quest.
    /// </summary>
    /// <param name="questName">The name of the main quest.</param>
    public void ProgressQuest(string questName)
    {
        Quest currentQuest = Array.Find(foundQuests.ToArray(), quest => quest.name == questName);

        //If the current quest is already complete, return
        if (currentQuest.IsQuestCompleted())
            return;

        //If the quest is now complete, add to the list of completed quests, and remove from the list of active quests
        if (UpdateQuestCompletion(currentQuest))
        {
            Debug.Log("Quest Completed: " + currentQuest.ToString());
            GameObject.FindGameObjectWithTag("QuestPopup").GetComponent<PopupBox>().ShowPopup("<size=70>Quest Completed.</size>\n" + currentQuest.name.ToString());
            activeQuestsNum--;
        }
    }

    /// <summary>
    /// Chooses a random active quest and returns a hint from it.
    /// </summary>
    /// <returns>The hint for the randomly chosen active quest.</returns>
    public string GetActiveQuestHint()
    {
        //If there are no active quests, don't return anything
        if (activeQuestsNum == 0)
            return null;

        UnityEngine.Random.InitState(DateTime.Now.Millisecond);  //Seeds the randomizer
        Quest[] activeQuests = GetActiveQuests();

        int currentQuest = UnityEngine.Random.Range(0, activeQuests.Length);
        
        return GetQuestHint(activeQuests[currentQuest]);
    }

    /// <summary>
    /// Checks the quest and every sub quest to get the most recent hint.
    /// </summary>
    /// <param name="currentQuest">The current quest being checked.</param>
    /// <returns>The hint text for the most recent incomplete sub quest or main quest.</returns>
    private string GetQuestHint(Quest currentQuest)
    {
        if (currentQuest == null)
            return null;

        //If there are sub quests present
        if (currentQuest.subQuests != null)
        {
            //Check the list of sub quests for an incomplete sub quest
            foreach (var subQuest in currentQuest.subQuests)
            {
                string subQuestHint = GetQuestHint(subQuest);
                if (subQuestHint != null)
                    return subQuestHint;
            }
        }

        //If the current quest being checked is not complete, return the quest hint
        if (!currentQuest.IsQuestCompleted())
            return currentQuest.hint;

        return null;
    }

    /// <summary>
    /// Checks the quest and every sub quest for completion. Completes the most recent quest.
    /// </summary>
    /// <param name="currentQuest">The current quest being checked.</param>
    /// <returns>If true, all sub quests are complete.</returns>
    private bool UpdateQuestCompletion(Quest currentQuest)
    {
        if (currentQuest == null)
            return true;

        bool allSubQuestsCompleted = true;

        //If there are sub quests present
        if (currentQuest.subQuests != null)
        {
            //If any recursive sub quests aren't completed, note that all sub quests are not complete
            foreach (var subQuest in currentQuest.subQuests)
            {
                if (!UpdateQuestCompletion(subQuest))
                    allSubQuestsCompleted = false;
            }
        }

        //If there are sub quests completed and the main quest is completed, return true
        if (allSubQuestsCompleted && currentQuest.IsQuestCompleted())
            return true;

        //If all sub quests are completed, make sure to complete the main quest
        if (allSubQuestsCompleted)
        {
            currentQuest.CompleteQuest();
            return true;
        }

        //If the sub quests are not completed, find the most recent quest and complete it
        if (!allSubQuestsCompleted)
        {
            foreach (var subQuest in currentQuest.subQuests)
            {
                if (!subQuest.IsQuestCompleted())
                {
                    subQuest.CompleteQuest();
                    break;
                }
            }
        }

        return false;
    }

    public Quest[] GetMasterQuestList() => masterQuestList;
    public Quest[] GetActiveQuests()
    {
        List<Quest> activeQuests = new List<Quest>();
        foreach (var currentQuest in foundQuests)
        {
            if (!currentQuest.IsQuestCompleted())
                activeQuests.Add(currentQuest);
        }

        return activeQuests.ToArray();
    }
    public Quest[] GetCompletedQuests()
    {
        List<Quest> completedQuests = new List<Quest>();
        foreach (var currentQuest in foundQuests)
        {
            if (currentQuest.IsQuestCompleted())
                completedQuests.Add(currentQuest);
        }

        return completedQuests.ToArray();
    }

    public int GetActiveQuestNumber() => activeQuestsNum;

    private Quest FindQuest(string name) => Array.Find(masterQuestList, quest => quest.name == name);
}
