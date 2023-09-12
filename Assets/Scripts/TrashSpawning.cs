using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawning : MonoBehaviour
{
    // Trash objects
    public GameObject bottle;
    public GameObject trashBag;
    public GameObject barrel;

    // Trashbag Sizes
    [SerializeField] private float smallTrashBagSize = 0.8f;
    [SerializeField] private float mediumTrashBagSize = 2f;

    // Trash counts
    [SerializeField] public int bottleCount = 20;
    [SerializeField] public int trashBagCount = 10;
    [SerializeField] public int barrelCount = 5;

    // Trash spawn area
    private float minX = -98f;
    private float maxX = 98f;
    private float minZ = -22f;
    private float maxZ = 22f;

    public void StartGame()
    {
        // Destroy all bottles, trash bags and barrels
        destroyTrash();

        // Spawn bottles
        SpawnTrash(bottle, bottleCount);

        // Spawn barrels
        SpawnTrash(barrel, barrelCount);

        // Spawn trash bags
        GameObject[] trashBags = SpawnTrash(trashBag, trashBagCount);

        // Loop through trash bags
        foreach (GameObject trashBag in trashBags)
        {
            // Get the trash script
            Trash trashScript = trashBag.GetComponent<Trash>();

            // Get the trashbag script
            Trashbag trashbagScript = trashBag.GetComponent<Trashbag>();

            // Randomly choose if trash bag is small or medium
            float trashBagSize = Random.Range(0f, 1f);
            if (trashBagSize < 0.5f)
            {
                // Set trash bag size to small
                trashBag.transform.localScale = new Vector3(smallTrashBagSize, smallTrashBagSize, smallTrashBagSize);

                // Set throw force to small bag force
                trashScript.throwForce = trashbagScript.smallBagForce;

                // Set trash bag to small
                trashScript.isLargeTrash = false;
            }
            else
            {
                // Set trash bag size to medium
                trashBag.transform.localScale = new Vector3(mediumTrashBagSize, mediumTrashBagSize, mediumTrashBagSize);

                // Set throw force to medium bag force
                trashScript.throwForce = trashbagScript.mediumBagForce;

                // Set trash bag to large
                trashScript.isLargeTrash = true;
            }
        }
    }

    void destroyTrash()
    {
        GameObject[] bottles = GameObject.FindGameObjectsWithTag("Bottle");
        foreach (GameObject bottle in bottles)
        {
            Destroy(bottle);
        }

        GameObject[] trashBags = GameObject.FindGameObjectsWithTag("Trashbag");
        foreach (GameObject trashBag in trashBags)
        {
            Destroy(trashBag);
        }

        GameObject[] barrels = GameObject.FindGameObjectsWithTag("Barrel");
        foreach (GameObject barrel in barrels)
        {
            Destroy(barrel);
        }
    }

    Vector3 GetRandomSpawnLocation()
    {
        // Get random x and z
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        return new Vector3(x, 6f, z);
    }

    GameObject[] SpawnTrash(GameObject trash, int count)
    {
        // Create array of game objects
        GameObject[] trashInstances = new GameObject[count];

        // Spawn trash at random locations and rotations
        for (int i = 0; i < count; i++)
        {
            // Get random spawn location
            Vector3 spawnLocation = GetRandomSpawnLocation();

            // Get random rotation on all axes
            Quaternion spawnRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

            // Spawn trash
            GameObject trashInstance = Instantiate(trash, spawnLocation, spawnRotation);

            // Add trash instance to array
            trashInstances[i] = trashInstance;
        }

        return trashInstances;
    }

}
