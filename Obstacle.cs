using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs and Camera")]
    public GameObject obstacleTopPrefab;
    public GameObject obstacleBottomPrefab;
    public Transform cameraTransform;

    [Header("Spawn Settings")]
    public float spawnXInterval = 5f;
    public float minY = -2f;
    public float maxY = 2f;
    public float gapY = 8f;

    [Header("Y Difference Check")]
    public float minYDifference = 1f;
    public int maxSpawnAttempts = 5;
    private float lastSpawnX;
    private float lastBottomY;
    private List<GameObject> topObstacles = new List<GameObject>();
    private List<GameObject> bottomObstacles = new List<GameObject>();

    void Start()
    {
        lastSpawnX = cameraTransform.position.x;
        lastBottomY = Random.Range(minY, maxY);
    }
    void Update()
    {
        if (cameraTransform.position.x - lastSpawnX >= spawnXInterval)
        {
            SpawnObstaclePair();
            lastSpawnX = cameraTransform.position.x;
            if (topObstacles.Count > 3)
            {
                Destroy(topObstacles[0]);
                topObstacles.RemoveAt(0);
            }
            if (bottomObstacles.Count > 3)
            {
                Destroy(bottomObstacles[0]);
                bottomObstacles.RemoveAt(0);
            }
        }
    }

    void SpawnObstaclePair()
    {
        float obstacleX = cameraTransform.position.x + spawnXInterval;
        float bottomY = GenerateBottomY();
        float topY = bottomY + gapY;
        Vector2 bottomPos = new Vector2(obstacleX, bottomY);
        Vector2 topPos = new Vector2(obstacleX, topY);
        var bottom = Instantiate(obstacleBottomPrefab, bottomPos, Quaternion.identity);
        var top = Instantiate(obstacleTopPrefab, topPos, Quaternion.identity);
        bottomObstacles.Add(bottom);
        topObstacles.Add(top);
    }
    float GenerateBottomY()
    {
        float y = Random.Range(minY, maxY);
        int attempts = 0;
        while (Mathf.Abs(y - lastBottomY) < minYDifference && attempts < maxSpawnAttempts)
        {
            y = Random.Range(minY, maxY);
            attempts++;
        }
        lastBottomY = y;
        return y;
    }
}
