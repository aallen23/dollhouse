using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestLogManager : MonoBehaviour
{
    [SerializeField, Tooltip("The button that shows the quest log.")] private Button questLogButton;
    [SerializeField, Tooltip("The transform that shows if there are no quests available.")] private RectTransform noQuestAvailableTransform;
    [SerializeField, Tooltip("The transform that shows all of the quest log data.")] private RectTransform questContentTransform;

    [SerializeField, Tooltip("The quest log entry prefab.")] private QuestLogEntry questLogEntryPrefab;

    [SerializeField, Tooltip("The position of the quest log when hidden.")] private Vector2 startQuestLogPosition;
    [SerializeField, Tooltip("The position of the quest log when shown.")] private Vector2 endQuestLogPosition;
    [SerializeField, Tooltip("The duration in seconds of the showing quest log animation.")] private float showQuestLogDuration;
    [SerializeField, Tooltip("The duration in seconds of the hiding quest log animation.")] private float hideQuestLogDuration;
    [SerializeField, Tooltip("The ease type for the show quest log animation.")] private LeanTweenType showQuestLogEaseType;
    [SerializeField, Tooltip("The ease type for the hide quest log animation.")] private LeanTweenType hideQuestLogEaseType;

    private RectTransform questLogTransform;

    private bool animationActive = false;
    private bool questLogOpen = false;

    private void Awake()
    {
        questLogTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        ResetQuestLog();
        UpdateQuestContentTransform();
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetQuestLog();
    }

    /// <summary>
    /// Resets the quest log to its default data.
    /// </summary>
    private void ResetQuestLog()
    {
        questLogOpen = false;
        animationActive = false;
        questLogTransform.anchoredPosition = startQuestLogPosition;
        questLogTransform.localScale = Vector3.zero;
        ClearLog();
    }

    /// <summary>
    /// Clears the quest log UI.
    /// </summary>
    private void ClearLog()
    {
        foreach(Transform trans in questContentTransform)
            Destroy(trans.gameObject);
    }

    /// <summary>
    /// Updates the quest log content, depending on whether there are quests recorded in the log or not.
    /// </summary>
    private void UpdateQuestContentTransform()
    {
        bool questsAvailable = questContentTransform.GetComponentsInChildren<QuestLogEntry>().Length > 0;
        noQuestAvailableTransform.gameObject.SetActive(!questsAvailable);
        questContentTransform.gameObject.SetActive(questsAvailable);
    }

    /// <summary>
    /// Shows or hides the quest log UI.
    /// </summary>
    /// <param name="showQuestLog">If true, the quest log is shown. If false, the quest log is hidden.</param>
    public void ShowQuestLog(bool showQuestLog)
    {
        if (animationActive)
            return;

        questLogOpen = showQuestLog;
        animationActive = true;

        questLogButton.interactable = !questLogOpen;

        LeanTween.scale(questLogTransform.gameObject, questLogOpen ? Vector3.one : Vector3.zero, questLogOpen ? showQuestLogDuration : hideQuestLogDuration)
            .setEase(questLogOpen ? showQuestLogEaseType : hideQuestLogEaseType).setOnComplete(() => animationActive = false);
        LeanTween.move(questLogTransform, questLogOpen ? endQuestLogPosition : startQuestLogPosition, questLogOpen ? showQuestLogDuration : hideQuestLogDuration)
            .setEase(questLogOpen ? showQuestLogEaseType : hideQuestLogEaseType);
    }

    /// <summary>
    /// Adds a quest to the quest log UI.
    /// </summary>
    /// <param name="newQuest">The new quest to add.</param>
    public void AddToQuestLog(Quest newQuest)
    {
        //If the quest already exists in the log, return
        if (GetLogEntry(newQuest) != null)
            return;

        QuestLogEntry newEntry = Instantiate(questLogEntryPrefab, questContentTransform);
        newEntry.UpdateQuestLogData(newQuest);

        if (!questContentTransform.gameObject.activeInHierarchy)
        {
            noQuestAvailableTransform.gameObject.SetActive(false);
            questContentTransform.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Updates the quest log to show the most up to date information on a quest.
    /// </summary>
    /// <param name="currentQuest">The current quest to update.</param>
    public void UpdateQuestLog(Quest currentQuest)
    {
        QuestLogEntry currentLogEntry = GetLogEntry(currentQuest);

        if (currentLogEntry != null)
            currentLogEntry.UpdateQuestLogData(currentQuest);
    }

    private QuestLogEntry GetLogEntry(Quest logData) => System.Array.Find(questContentTransform.GetComponentsInChildren<QuestLogEntry>(), quest => quest.GetQuestData().name == logData.name);
}
