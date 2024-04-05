using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentWeek; // Holds the current week number

    // Events to signal week changes and item updates (optional)
    public delegate void WeekChanged(int newWeek);
    public WeekChanged onWeekChanged;

    public delegate void ItemsUpdated(List<CompostableItemManager.CompostableItem> updatedItems);
    public ItemsUpdated onItemsUpdated;

    void Start()
    {
        currentWeek = 1; // Initialize starting week
    }

    public void NextWeek()
    {
        currentWeek++;

        // Trigger week changed event (optional)
        if (onWeekChanged != null)
        {
            onWeekChanged(currentWeek);
        }
    }
}
