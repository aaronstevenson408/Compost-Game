using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WeeklyTurnManager weeklyTurnManager;

    public void AdvanceWeek()
    {
        if (weeklyTurnManager != null)
        {
            weeklyTurnManager.NextWeek();
        }
    }
}