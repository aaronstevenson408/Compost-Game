using UnityEngine;
using TMPro;


public class WeeklyTurnManager : MonoBehaviour
{
    public GameManager gameManager;
    public int currentWeek;

    void Start()
    {
        if (gameManager != null)
        {
            currentWeek = gameManager.currentWeek; // Get current week from GameManager
            // ... update UI based on currentWeek ...

            // Subscribe to week changed event (optional)
            if (gameManager.onWeekChanged != null)
            {
                gameManager.onWeekChanged += OnWeekChanged;
            }
        }
    }

    public void NextWeek()
    {
        if (gameManager != null)
        {
            gameManager.NextWeek(); // Trigger week change in GameManager
        }
    }

    void OnWeekChanged(int newWeek)
    {
        currentWeek = newWeek;
        // Update UI or handle weekly changes based on newWeek
    }
}