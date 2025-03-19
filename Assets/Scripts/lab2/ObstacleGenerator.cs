using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstaclePrefab; // Префаб перешкоди
    public float spawnInterval = 1f; // Інтервал між появою перешкод
    public float spawnRangeX = 4.5f; // Діапазон появи перешкод по осі X (половина ширини лінії)
    public float spawnRangeY = 3f; // Діапазон появи перешкод по осі Y (висота)
    public float spawnStartZ = -83f; // Початок лінії по осі Z
    public float spawnEndZ = 17f; // Кінець лінії по осі Z

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            float randomY = Random.Range(0, spawnRangeY);
            float randomZ = Random.Range(spawnStartZ, spawnEndZ);

            Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}