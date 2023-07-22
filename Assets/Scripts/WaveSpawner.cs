using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Transform shieldGoblinPrefab;
    [SerializeField] private Transform goblinPrefab;
    [SerializeField] private Transform birdPrefab;
    [SerializeField] private Transform dragonPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform airSpawnPoint;

    private float timeBetweenWaves = 5f;
    private float countDown = 2f;
    private int waveNumber = 1;
    private int difficultCounter = 0;

    private void Update()
    {
        if (countDown <= 0)
        {
            //StartCoroutine(SpawnWave());
            SpawnEnemy();
            countDown = timeBetweenWaves;
        }
        countDown -= Time.deltaTime;
    }

    //IEnumerator SpawnWave()
    //{
    //    SpawnEnemy();
    //    yield return new WaitForSeconds(0.5f);
        
    //    waveNumber++;
    //}

    

    private void SpawnEnemy()
    {

        if (waveNumber % 1 == 0)
        {
            StartCoroutine(SpawnCount(waveNumber / 1, goblinPrefab, spawnPoint.transform));
        }
        if (waveNumber % 2 == 0 && difficultCounter > 0)
        {
            StartCoroutine(SpawnCount(waveNumber / 2, shieldGoblinPrefab, spawnPoint.transform));
        }
        if (waveNumber % 3 == 0 && difficultCounter > 1)
        {
            StartCoroutine(SpawnCount(waveNumber / 3, birdPrefab, airSpawnPoint.transform));
        }
        if (waveNumber % 4 == 0 && difficultCounter > 2)
        {
            StartCoroutine(SpawnCount(waveNumber / 4, dragonPrefab, airSpawnPoint.transform));
        }
        ChangeDifficult();
    }

    IEnumerator SpawnCount(int number, Transform prefab, Transform _spawnPoint)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(prefab, _spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void ChangeDifficult()
    {
        waveNumber++;
        if (waveNumber >= 5)
        {
            difficultCounter++;
            waveNumber -= 4;
        }
    }
}
