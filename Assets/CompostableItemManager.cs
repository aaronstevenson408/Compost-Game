using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostableItemManager : MonoBehaviour
{
    public class CompostableItem
    {
        public string itemName;
        public int quantity;
    }

    private List<CompostableItem> availableItems; // List for compostable items

    // Event to signal a change in available items
    public delegate void AvailableItemsChanged(List<CompostableItem> updatedItems);
    public AvailableItemsChanged onAvailableItemsChanged;

    void Start()
    {
        availableItems = new List<CompostableItem>();
        // Define initial quantities and item names by adding CompostableItem objects to availableItems list
    }

    public List<CompostableItem> GetAvailableItems()
    {
        return new List<CompostableItem>(availableItems); // Return a copy of the list
    }

    public void UseItem(string itemName)
    {
        // Find the item in the list using itemName
        // Decrement the quantity for that item

        // Trigger the event with the updated list
        if (onAvailableItemsChanged != null)
        {
            onAvailableItemsChanged(new List<CompostableItem>(availableItems)); // Send a copy of the updated list
        }
    }

    // Additional methods (optional)
    public void ResetItemsForWeek(int newWeek)
    {
        // Implement logic to reset item quantities based on the new week
    }
}
