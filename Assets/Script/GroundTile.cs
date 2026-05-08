using UnityEngine;

public class GroundTile : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;

    // Lane positions (-2, 0, +2)
    float[] lanes = new float[] { -2f, 0f, 2f };

    public static float difficulty = 1f;  // Difficulty multiplier

    void Start()
    {
        SpawnObstacle();
    }

    public void SpawnObstacle()
    {
        // -----------------------------
        // INCREASE DIFFICULTY OVER TIME
        // -----------------------------
        // Example: after 200m -> difficulty = 3
        if (GameObject.FindWithTag("Player") != null)
        {
            float z = GameObject.FindWithTag("Player").transform.position.z;
            difficulty = 1f + (z / 150f);
        }

        difficulty = Mathf.Clamp(difficulty, 1f, 4f); // Avoid going too high


        // -----------------------------
        //   HOW MANY OBSTACLES?
        // -----------------------------
        int obstaclesThisTile = Mathf.Clamp(Mathf.FloorToInt(difficulty), 1, 3);

        bool[] laneUsed = new bool[lanes.Length];

        // Spawn obstacles
        for (int i = 0; i < obstaclesThisTile; i++)
        {
            int laneIndex;

            // Pick a random unused lane
            do laneIndex = Random.Range(0, lanes.Length);
            while (laneUsed[laneIndex]);

            laneUsed[laneIndex] = true;

            Vector3 pos = transform.position + new Vector3(lanes[laneIndex], 0, 10f);
            Instantiate(obstaclePrefab, pos, Quaternion.identity, transform);
        }


        // -----------------------------
        //   COINS SPAWN LOGIC
        // -----------------------------
        for (int i = 0; i < lanes.Length; i++)
        {
            // Don’t spawn coins where obstacles already exist
            if (laneUsed[i]) continue;

            // Coins become rarer as difficulty increases
            float coinChance = Mathf.Lerp(0.8f, 0.3f, (difficulty - 1f) / 3f);

            if (Random.value < coinChance)
            {
                Vector3 coinPos = transform.position + new Vector3(lanes[i], 1f,
                    Random.Range(3f, 17f));

                Instantiate(coinPrefab, coinPos, Quaternion.identity, transform);
            }
        }
    }
}
