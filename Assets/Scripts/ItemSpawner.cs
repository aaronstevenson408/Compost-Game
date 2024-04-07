// using System.Collections.Generic;
// using UnityEngine;

// public class ItemSpawner : MonoBehaviour
// {
//     public int minItemsToSpawn = 5; // Minimum number of items to spawn
//     public int maxItemsToSpawn = 10; // Maximum number of items to spawn

//     public List<CompostableItem> SpawnItems()
//     {
//         List<CompostableItem> spawnedItems = new List<CompostableItem>();

//         // Determine the number of items to spawn
//         int itemCount = Random.Range(minItemsToSpawn, maxItemsToSpawn + 1);

//         for (int i = 0; i < itemCount; i++)
//         {
//             // Generate random values for the CompostableItem parameters
//             string randomName = "Item " + i; // You can generate more descriptive names if needed
//             float randomNitrogen = Random.Range(0.1f, 1.0f); // Adjust the range as needed
//             float randomCarbon = Random.Range(0.1f, 1.0f); // Adjust the range as needed
//             float randomWeight = Random.Range(0.1f, 10.0f);
//             // Create a new instance of CompostableItem with the generated values
//             CompostableItem spawnedItem = new CompostableItem(randomName, randomNitrogen, randomCarbon, randomWeight);

//             // Log the details of the spawned item
//             Debug.Log($"Spawned item: {spawnedItem.itemName}, Nitrogen: {spawnedItem.nitrogenContent}, Carbon: {spawnedItem.carbonContent}");

//             spawnedItems.Add(spawnedItem);
//         }

//         return spawnedItems;
//     }
// }

using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<CompostableItem> itemPrefabs; // A list of CompostableItem prefabs to spawn
    public int minItemsToSpawn = 5; // Minimum number of items to spawn
    public int maxItemsToSpawn = 10; // Maximum number of items to spawn
    public GameObject spawnArea; // Reference to the spawn area (table) GameObject
    public float spawnAreaRadius = 1f; // Radius of the spawn area around the table
    public List<CompostableItem> SpawnItems()
    {
        List<CompostableItem> spawnedItems = new List<CompostableItem>();

        // Determine the number of items to spawn
        int itemCount = Random.Range(minItemsToSpawn, maxItemsToSpawn + 1);
        for (int i = 0; i < itemCount; i++)
        {
            // Select a random prefab from the list
            CompostableItem prefab = itemPrefabs[Random.Range(0, itemPrefabs.Count)];

            // Calculate a random position within the spawn area
            Vector3 randomPosition = spawnArea.transform.position + Random.insideUnitCircle * spawnAreaRadius;

            // Instantiate the prefab at the random position and add it to the list
            CompostableItem spawnedItem = Instantiate(prefab.gameObject, randomPosition, Quaternion.identity).GetComponent<CompostableItem>();
            spawnedItems.Add(spawnedItem);
        }
        return spawnedItems;
    }
}