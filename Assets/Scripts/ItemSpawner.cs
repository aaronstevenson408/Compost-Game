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
            Vector2 randomOffset = Random.insideUnitCircle * spawnAreaRadius;
            Vector3 randomPosition = spawnArea.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

            // Instantiate the prefab at the random position and add it to the list
            CompostableItem spawnedItem = Instantiate(prefab.gameObject, randomPosition, Quaternion.identity).GetComponent<CompostableItem>();
            spawnedItems.Add(spawnedItem);
        }
        return spawnedItems;
    }
}