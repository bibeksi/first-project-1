using UnityEngine;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTilePrefab;   // Tile prefab
    public Transform player;              // Player to follow
    public int numberOfTiles = 5;         // How many tiles to keep
    public float tileLength = 20f;        // Length of one tile

    private float zSpawn = 0f;
    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        // When player gets close to the end of the spawned tiles
        if (player.position.z - 40f > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnTile()
    {
        GameObject tile = Instantiate(groundTilePrefab, new Vector3(0, 0, zSpawn), Quaternion.identity);
        tile.GetComponent<GroundTile>().SpawnObstacle();
        activeTiles.Add(tile);
        zSpawn += tileLength;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
