using UnityEngine;
using System.Collections.Generic;
// using UIManager;
public class GameManager : MonoBehaviour
{
    // Variables to store game state
    public int currentWeek = 0;
    public int reputation = 0;
    public int money = 0;

    // List to store compost layers
    public List<CompostLayer> compostLayers = new List<CompostLayer>();

    // References to other game components
    public CompostBin compostBin;
    public ItemSpawner itemSpawner;
    public UIManager uiManager;

    void Start()
    {
        // Initialize game state
        InitializeGame();
    }

    void Update()
    {
        // Handle user input and game state updates
    }

    void InitializeGame()
    {
        // Reset game state
        currentWeek = 0;
        reputation = 0;
        money = 0;
        compostLayers.Clear();

        // Spawn initial compostable items
        SpawnCompostableItems();
    }

    public void AdvanceWeek(List<CompostableItem> addedItems, float waterLevel, float ventilation)
    {
        // Add a new compost layer based on the added items and variables
        CompostLayer newLayer = new CompostLayer(addedItems, waterLevel, ventilation);
        compostLayers.Add(newLayer);

        // Merge adjacent layers with the same decay state
        MergeSimilarLayers();

        // Update compost bin UI
        compostBin.UpdateCompostBinUI(compostLayers);

        // Update week and spawn new items
        currentWeek++;
        SpawnCompostableItems();
        uiManager.UpdateWeekDisplay(currentWeek);
    }

    void MergeSimilarLayers()
    {
        // Iterate through the compost layers
        for (int i = 0; i < compostLayers.Count - 1; i++)
        {
            // If two adjacent layers have the same decay state, merge them
            if (compostLayers[i].decayState == compostLayers[i + 1].decayState)
            {
                CompostLayer mergedLayer = compostLayers[i].MergeWith(compostLayers[i + 1]);
                compostLayers[i] = mergedLayer;
                compostLayers.RemoveAt(i + 1);
            }
        }
    }

    void SpawnCompostableItems()
    {
        // Spawn new compostable items for the week
        itemSpawner.SpawnItems();
    }

    public void StirLayers(int layerIndex1, int layerIndex2)
    {
        // Merge the two specified layers
        CompostLayer mergedLayer = compostLayers[layerIndex1].MergeWith(compostLayers[layerIndex2]);
        compostLayers[layerIndex1] = mergedLayer;
        compostLayers.RemoveAt(layerIndex2);

        // Update compost bin UI
        compostBin.UpdateCompostBinUI(compostLayers);
    }

    public void PullOutFertilizer(float amountToPull, bool mixEntireBin)
    {
        // Calculate the total amount of fertilizer in the compost bin
        float totalFertilizer = CalculateTotalFertilizer();

        // Check if the requested amount is available
        if (amountToPull > totalFertilizer)
        {
            Debug.LogWarning("Not enough fertilizer in the compost bin.");
            return;
        }

        // Mix the compost bin if requested
        if (mixEntireBin)
        {
            MixEntireBin();
        }

        // Remove the requested amount of fertilizer from the compost bin
        float remainingFertilizer = totalFertilizer - amountToPull;
        AdjustCompostLayersAfterPull(remainingFertilizer);

        // Update UI and sell the pulled fertilizer
        float compostQuality = CalculateCompostQuality();
        SellFertilizer(amountToPull, compostQuality);
    }

    void MixEntireBin()
    {
        List<CompostableItem> allItems = new List<CompostableItem>();
        // Merge all layers into a single layer
        CompostLayer mergedLayer = new CompostLayer(allItems, 0.5f, 0.5f);
        foreach (CompostLayer layer in compostLayers)
        {
            mergedLayer = mergedLayer.MergeWith(layer);
        }
        compostLayers.Clear();
        compostLayers.Add(mergedLayer);

        // Update compost bin UI
        compostBin.UpdateCompostBinUI(compostLayers);
    }

    void AdjustCompostLayersAfterPull(float remainingFertilizer)
    {
        // Adjust the compost layers based on the remaining fertilizer
        // Implement your logic here
        // You might need to remove layers or adjust their properties

        // Example implementation (remove layers until the remaining fertilizer is less than the next layer's amount):
        float remainingAmount = remainingFertilizer;
        for (int i = compostLayers.Count - 1; i >= 0; i--)
        {
            CompostLayer layer = compostLayers[i];
            if (remainingAmount >= layer.fertilizer)
            {
                remainingAmount -= layer.fertilizer;
                compostLayers.RemoveAt(i);
            }
            else
            {
                layer.fertilizer = remainingAmount;
                break;
            }
        }

        // Update compost bin UI
        compostBin.UpdateCompostBinUI(compostLayers);
    }

    float CalculateTotalFertilizer()
    {
        // Calculate the total amount of fertilizer in the compost bin
        // Implement your logic here
        float totalFertilizer = 0f;
        foreach (CompostLayer layer in compostLayers)
        {
            totalFertilizer += layer.fertilizer;
        }
        return totalFertilizer;
    }

    void SellFertilizer(float amount, float quality)
    {
        // Calculate the sale amount based on the amount and quality of fertilizer
        int saleAmount = (int)(amount * quality * 10); // Example calculation

        // Update money and reputation
        money += saleAmount;
        reputation += (int)(quality * 10); // Example calculation

        // Update UI
        float compostQuality = CalculateCompostQuality();
        uiManager.UpdateGameState(money, reputation, compostQuality);

    }

    float CalculateCompostQuality()
    {
        // Calculate the overall compost quality based on the layers
        // Implement your compost quality calculation logic here
        // Example implementation (average of all layer qualities):
        float totalQuality = 0f;
        foreach (CompostLayer layer in compostLayers)
        {
            totalQuality += layer.quality;
        }
        return totalQuality / compostLayers.Count;
    }
}