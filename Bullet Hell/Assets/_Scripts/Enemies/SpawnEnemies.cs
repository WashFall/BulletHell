using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    private bool canSpawn = true;
    private float spawnRate = 1;
    private float cameraWidth;

    private void Awake()
    {
        cameraWidth = Camera.main.aspect * (Camera.main.orthographicSize) + 1;
    }

    void Update()
    {
        if (canSpawn)
        {
            canSpawn = false;
            StartCoroutine(nameof(spawnEnemy));
            spawnRate = 1 - GameManager.Instance.timer / 100;
        }
    }

    private IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(spawnRate);

        Vector2 enemySpawnPoint = (Vector2)transform.position + (Random.insideUnitCircle.normalized * cameraWidth);
        GameObject enemy = Instantiate(enemyPrefab, enemySpawnPoint, Quaternion.identity);
        enemy.GetComponent<MoveToPlayerObject>().playerObject = player;
        canSpawn = true;
    }
}
