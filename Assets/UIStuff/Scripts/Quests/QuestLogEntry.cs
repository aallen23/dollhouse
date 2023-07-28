using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLogEntry : MonoBehaviour
{
    [SerializeField, Tooltip("The text that displays the name of the quest.")] private TextMeshProUGUI questTitle;
    [SerializeField, Tooltip("The text that shows whether the quest has been completed or not.")] private TextMeshProUGUI completedQuestLabel;
    [SerializeField, Tooltip("The text that shows the quest description.")] private TextMeshProUGUI questDescription;

    private Quest questData;

    /// <summary>
    /// Displays the current quest data and shows differently depending on whether the quest is complete or not.
    /// </summary>
    public void RefreshDisplay()
    {
        questTitle.text = (questData.IsQuestCompleted() ? "<s>" : "") + questData.name.ToString() + (questData.IsQuestCompleted() ? "</s>" : "");
        questDescription.text = (questData.IsQuestCompleted() ? "<s>" : "") + questData.description.ToString() + (questData.IsQuestCompleted() ? "</s>" : "");

        Color newCompletedTextColor = completedQuestLabel.color;
        completedQuestLabel.color = new Color(newCompletedTextColor.r, newCompletedTextColor.g, newCompletedTextColor.b, questData.IsQuestCompleted() ? 1 : 0);
    }

    /// <summary>
    /// Updates the log entry's quest data with new information.
    /// </summary>
    /// <param name="newData">The new quest data for the log.</param>
    public void UpdateQuestLogData(Quest newData)
    {
        questData = newData;
        RefreshDisplay();
    }

    public Quest GetQuestData() => questData;
}
