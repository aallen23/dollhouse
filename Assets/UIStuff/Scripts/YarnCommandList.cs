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

    [YarnCommand("setLocation")]
    public void SetLocation(string locationName)
    {
        FindObjectOfType<LocationName>().ChangeLocationName(locationName);
    }

    [YarnCommand("showSprite")]
    public void ShowSprite(string spriteName, string direction, bool isActive = true)
    {
        DialogueController.Instance.ShowSprite(spriteName, direction, isActive);
    }

    [YarnCommand("showIcon")]
    public void ShowIcon(string spriteName, bool isActive = true)
    {
        DialogueController.Instance.ShowIcon(spriteName, isActive);
    }

    [YarnCommand("changeEmotion")]
    public void ChangeEmotion(string spriteName, string emotion = "neutral")
    {
        DialogueController.Instance.ChangeEmotion(spriteName, emotion);   
    }

    [YarnCommand("moveNameTab")]
    public void MoveNameTab(string direction)
    {
        DialogueController.Instance.MoveNameTab(direction);
    }

    [YarnCommand("highlightFirstOption")]
    public void HighlightFirstOption()
    {
        DialogueController.Instance.SelectFirstOption();
    }
}
