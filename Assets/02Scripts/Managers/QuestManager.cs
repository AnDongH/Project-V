using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : DontDestroySingleton<QuestManager>
{
    #region Events
    public delegate void QuestRegisteredHandler(Quest newQuest);
    public delegate void QuestCompletedHandler(Quest quest);
    public delegate void QuestCanceledHandler(Quest quest);
    #endregion

    private List<Quest> activeQuests = new List<Quest>();
    private List<Quest> completedQuests = new List<Quest>();
    private List<Quest> activeAchievements = new List<Quest>();
    private List<Quest> completedAchievements = new List<Quest>();

    private QuestDatabase questDatatabase;
    private QuestDatabase achievementDatabase;

    public event QuestRegisteredHandler OnQuestRegisteredHandler;
    public event QuestCompletedHandler OnQuestCompletedHandler;
    public event QuestCanceledHandler OnQuestCanceledHandler;
    public event QuestRegisteredHandler OnAchievementRegisteredHandler;
    public event QuestCompletedHandler OnAchievementCompletedHandler;

    public IReadOnlyList<Quest> ActiveQuests => activeQuests;
    public IReadOnlyList<Quest> CompletedQuests => completedQuests;
    public IReadOnlyList<Quest> ActiveAchievements => activeAchievements;
    public IReadOnlyList<Quest> CompletedAchievements => completedAchievements;

    protected override void Awake()
    {
        base.Awake();
        questDatatabase = Resources.Load<QuestDatabase>("QuestDatabase");
        achievementDatabase = Resources.Load<QuestDatabase>("AchievementDatabase");
    }

    /// <summary>
    /// Register the quest that was passed in as a parameter.
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
    public Quest Register(Quest quest)
    {
        // Use a copy of the original quest scriptable object for runtime operations.
        var newQuest = quest.Clone();

        // register
        if (newQuest is Achievement)
        {
            newQuest.OnCompleted += OnAchievementCompleted;

            activeAchievements.Add(newQuest);

            newQuest.OnRegister();
            OnAchievementRegisteredHandler?.Invoke(newQuest);
        }
        else
        {
            newQuest.OnCompleted += OnQuestCompleted;
            newQuest.OnCanceled += OnQuestCanceled;

            activeQuests.Add(newQuest);

            newQuest.OnRegister();
            OnQuestRegisteredHandler?.Invoke(newQuest);
        }

        return newQuest;
    }

    /// <summary>
    /// To update the values for all quests, 
    /// determine whether the category of the task you set 
    /// matches the category of the given parameter, 
    /// and whether the target of of the task you set 
    /// matches the parameter's target. 
    /// If both conditions are met, update the quest values.
    /// Since multiple quests and tasks can have the same category and target simultaneously, 
    /// this section allows for batch updates.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="target"></param>
    /// <param name="conditionCount"></param>
    public void ReceiveReport(string category, object target, int conditionCount)
    {
        ReceiveReport(activeQuests, category, target, conditionCount);
        ReceiveReport(activeAchievements, category, target, conditionCount);
    }

    /// <summary>
    /// Overloading of `ReceiveReport(string category, object target, int conditionCount)`.
    /// For ease of use, you can use a category as an argument.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="target"></param>
    /// <param name="conditionCount"></param>
    public void ReceiveReport(Category category, TaskTarget target, int conditionCount) => ReceiveReport(category.ID, target.Value, conditionCount);

    /// <summary>
    /// Overloading of `ReceiveReport(string category, object target, int conditionCount)`.
    /// Perform `ReceiveReport` on all quests at once.
    /// </summary>
    /// <param name="quests"></param>
    /// <param name="category"></param>
    /// <param name="target"></param>
    /// <param name="conditionCount"></param>
    private void ReceiveReport(List<Quest> quests, string category, object target, int conditionCount)
    {
        // To avoid errors when iterating through the quests list
        // since a quest might be completed and removed from the list
        // so use a copy of the list for iteration.
        foreach (var quest in quests.ToArray())
            quest.ReceiveReport(category, target, conditionCount);
    }

    public bool IsTarget(string category, object target) {
        return IsTarget(activeQuests, category, target);
    }

    public bool IsTarget(Category category, TaskTarget target) => IsTarget(category.ID, target.Value);

    private bool IsTarget(List<Quest> quests, string category, object target) {
        foreach (var quest in quests.ToArray()) {
            return quest.IsTarget(category, target);
        }
        return false;
    }

    /// <summary>
    /// Is the given quest part of the current quests in progress?
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
    public bool ContainsInActiveQuests(Quest quest) => activeQuests.Any(x => x.ID == quest.ID);

    /// <summary>
    /// Is the given quest part of the current quests in completed?
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
    public bool ContainsInCompleteQuests(Quest quest) => completedQuests.Any(x => x.ID == quest.ID);

    /// <summary>
    /// Is the given achievement part of the current achievements in progress?
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
    public bool ContainsInActiveAchievements(Quest quest) => activeAchievements.Any(x => x.ID == quest.ID);

    /// <summary>
    /// Is the given achievement part of the current achievements in completed?
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
    public bool ContainsInCompletedAchievements(Quest quest) => completedAchievements.Any(x => x.ID == quest.ID);

    #region Callback
    private void OnQuestCompleted(Quest quest)
    {
        activeQuests.Remove(quest);
        completedQuests.Add(quest);
        OnQuestCompletedHandler?.Invoke(quest);
    }

    private void OnQuestCanceled(Quest quest)
    {
        activeQuests.Remove(quest);
        OnQuestCanceledHandler?.Invoke(quest);

        // Destroy next frame
        Destroy(quest, Time.deltaTime);
    }

    private void OnAchievementCompleted(Quest achievement)
    {
        activeAchievements.Remove(achievement);
        completedAchievements.Add(achievement);
        OnAchievementCompletedHandler?.Invoke(achievement);
    }
    #endregion
}
