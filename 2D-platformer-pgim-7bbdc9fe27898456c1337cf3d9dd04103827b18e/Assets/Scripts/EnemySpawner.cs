using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab; // Prefab musuh
    [SerializeField] Transform[] spawnPoints; // Posisi spawn musuh
    [SerializeField] float spawnInterval = 3f; // Waktu antar spawn (detik)

    void Start()
    {
        if (spawnPoints.Length == 0) 
        {
            Debug.LogError("Spawn Points kosong! Tambahkan spawn points di Inspector.");
        }
        else 
        {
            StartCoroutine(SpawnEnemies()); // Mulai spawn musuh secara berkala
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true) 
        {
            SpawnEnemyAtRandomPoint();
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

 void SpawnEnemyAtRandomPoint()
{
    if (enemyPrefab == null)
    {
        Debug.LogError("Enemy prefab tidak diassign di Inspector!");
        return;
    }

    if (spawnPoints.Length == 0)
    {
        Debug.LogWarning("Tidak ada spawn point yang diassign!");
        return;
    }

    // Pilih spawn point secara acak
    int randomIndex = Random.Range(0, spawnPoints.Length);
    Transform spawnPoint = spawnPoints[randomIndex];

    // Spawn musuh di posisi spawn point
    Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
}

}
