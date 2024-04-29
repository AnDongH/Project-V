using System.Linq;
using UnityEngine;

public class QuestTrackerViewUI : ObjectUI
{
    [SerializeField] private QuestTrackerUI questTrackerPrefab;
    [SerializeField] private CategoryColor[] categoryColors;

    private void Start()
    {
        Access.QuestM.OnQuestRegisteredHandler += CreateQuestTracker;

        foreach (var quest in Access.QuestM.ActiveQuests)
            CreateQuestTracker(quest);

        GetComponent<CanvasGroup>().alpha = 0f;
    }

    private void OnDestroy()
    {
        if (Access.QuestM) Access.QuestM.OnQuestRegisteredHandler -= CreateQuestTracker;
    }

    private void CreateQuestTracker(Quest quest)
    {
        var categoryColor = categoryColors.FirstOrDefault(x => x.category == quest.Category);
        var color = categoryColor.category == null ? Color.white : categoryColor.color;
        Instantiate(questTrackerPrefab, transform).Setup(quest, color);
    }

    [System.Serializable]
    private struct CategoryColor
    {
        public Category category;
        public Color color;
    }
}