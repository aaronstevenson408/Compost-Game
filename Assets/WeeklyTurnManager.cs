using UnityEngine;
using TMPro;


public class WeeklyTurnManager : MonoBehaviour
{
    public TMP_Text weekText; // Optional (public variable for reference)
    public int currentWeek;

    void Start()
    {
        currentWeek = 1;

        // Find TextMeshPro object using its name
        weekText = GameObject.Find("WeekNumberText").GetComponent<TMP_Text>();

        if (weekText != null) // Check if text object is found
        {
            weekText.text = "Week: " + currentWeek;
        }
    }

    public void NextWeek()
    {
        currentWeek++;
        if (weekText != null) // Check if text object is found
        {
            weekText.text = "Week: " + currentWeek;
        }
    }
}