using UnityEngine;
using TMPro;

public class TaskDescriptorUI : ObjectUI
{
    [SerializeField] TextMeshProUGUI textM;

    [SerializeField] private Color normalColor;
    [SerializeField] private Color taskCompletionColor;
    [SerializeField] private Color taskSuccessCountColor;
    [SerializeField] private Color strikeThroughColor;

    public void UpdateText(string text)
    {
        textM.fontStyle = FontStyles.Normal;
        textM.text = text;
    }

    public void UpdateText(Task task)
    {
        textM.fontStyle = FontStyles.Normal;

        if (task.IsComplete)
        {
            var colorCode = ColorUtility.ToHtmlStringRGB(taskCompletionColor);
            textM.text = BuildText(task, colorCode, colorCode);
        }
        else
            textM.text = BuildText(task, ColorUtility.ToHtmlStringRGB(normalColor), ColorUtility.ToHtmlStringRGB(taskSuccessCountColor));
    }

    public void UpdateTextUsingStrikeThrough(Task task)
    {
        var colorCode = ColorUtility.ToHtmlStringRGB(strikeThroughColor);
        textM.fontStyle = FontStyles.Strikethrough;
        textM.text = BuildText(task, colorCode, colorCode);
    }

    private string BuildText(Task task, string textColorCode, string successCountColorCode)
    {
        return $"<color=#{textColorCode}>¡Ü {task.Description} <color=#{successCountColorCode}>{task.CurrentCondition}</color>/{task.NeededConditionToComplete}</color>";
    }
}
