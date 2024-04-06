using UnityEngine;
using TMPro;

public class WeeklyTurnManager : MonoBehaviour
{
    public int currentWeek = 1; // Initialize starting week
    public TextMeshProUGUI weekNumberText; // Reference to the UI element

    void Start()
    {
        UpdateWeekNumberUI(); // Update the UI with the initial week number
    }

    public void NextWeek()
    {
        currentWeek++;
        UpdateWeekNumberUI(); // Update the UI with the new week number
    }

    void UpdateWeekNumberUI()
    {
        if (weekNumberText != null)
        {
            weekNumberText.text = "Week " + currentWeek.ToString();
        }
    }
}