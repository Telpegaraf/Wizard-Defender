using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform GoblinPrefab;
    public Transform spawnPoint;

    public float timeBetweenEaves = 5f;
    private float countDown = 2f;
    private int waveNumber = 1;

    private void Update()
    {
        if (countDown <= 0)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenEaves;
        }

        countDown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        waveNumber++;
    }

    private void SpawnEnemy()
    {
        Instantiate(GoblinPrefab, spawnPoint.transform.position, Quaternion.identity);
    }
}
