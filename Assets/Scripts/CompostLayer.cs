using System.Collections.Generic;
using UnityEngine;

public class CompostLayer
{
    public List<CompostableItem> items;
    public float waterLevel;
    public float ventilation;
    public float decayState; // 0 (fresh) to 1 (fully decayed)
    public float fertilizer; // Amount of fertilizer in this layer
    public float quality; // Quality of the fertilizer in this layer

    public CompostLayer(List<CompostableItem> addedItems, float water, float vent)
    {
        items = addedItems;
        waterLevel = water;
        ventilation = vent;
        decayState = 0f;
        fertilizer = 0f; // Initial fertilizer amount
        quality = 0.5f; // Initial quality (adjust as needed)
    }

    public CompostLayer MergeWith(CompostLayer otherLayer)
    {
        // Implement the logic to merge two layers
        // Calculate the new water level, ventilation, decay state, fertilizer amount, and quality based on the merged items
        // Return a new CompostLayer instance with the merged properties
        return new CompostLayer(new List<CompostableItem>(), 0.5f, 0.5f);
    }
}