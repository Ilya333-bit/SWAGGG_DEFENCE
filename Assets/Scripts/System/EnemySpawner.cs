using System.Collections;
using TMPro;
using UnityEngine;

public class AdvancedEnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName; // Название волны
        public GameObject[] enemyPrefabs; // Префабы врагов для волны
        public int baseCount; // Базовое количество врагов в волне
        public float spawnRate; // Скорость спавна врагов в волне
        public bool isBossWave; // Является ли волна босс-волной
    }

    public Wave[] baseWaves; // Базовые конфигурации волн
    public Transform[] spawnPoints; // Точки спавна
    public float timeBetweenWaves = 10f; // Время между волнами
    public TextMeshProUGUI waveCountdownText; // UI элемент для отображения обратного отсчета
    public TextMeshProUGUI waveInfoText; // UI элемент для отображения информации о волне

    private int currentWaveIndex = 0; // Текущий индекс базовой волны
    private float countdown = 0f; // Обратный отсчет до следующей волны
    private int waveNumber = 0; // Номер текущей волны

    public delegate void OnWaveStart(Wave wave);
    public event OnWaveStart WaveStarted;

    public delegate void OnWaveEnd(Wave wave);
    public event OnWaveEnd WaveEnded;

    void Start()
    {
        countdown = timeBetweenWaves;
        UpdateWaveInfo();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            waveCountdownText.text = Mathf.Round(countdown).ToString();
        }
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            countdown = timeBetweenWaves;

            Wave wave = GenerateWave();
            WaveStarted?.Invoke(wave);
            StartCoroutine(SpawnWave(wave));

            waveNumber++;
            UpdateWaveInfo();
        }
    }

    Wave GenerateWave()
    {
        Wave baseWave = baseWaves[currentWaveIndex % baseWaves.Length];
        Wave newWave = new Wave();
        newWave.waveName = "Wave " + (waveNumber + 1);
        newWave.enemyPrefabs = baseWave.enemyPrefabs;
        newWave.baseCount = Mathf.RoundToInt(baseWave.baseCount * Mathf.Pow(1.1f, waveNumber)); // Увеличение количества врагов
        newWave.spawnRate = baseWave.spawnRate * Mathf.Pow(1.05f, waveNumber); // Увеличение скорости спавна
        if (waveNumber != 0)
        {
            newWave.isBossWave = waveNumber % 10 == 0; // Каждая 10-я волна - босс
        }

        if (newWave.isBossWave)
        {
            newWave.waveName = "Boss Wave " + (waveNumber / 10);
            newWave.enemyPrefabs = new GameObject[] { baseWave.enemyPrefabs[Random.Range(0, baseWave.enemyPrefabs.Length)] };
            newWave.baseCount = 1; // Один босс
            newWave.spawnRate = 0.5f; // Замедленная скорость спавна
        }

        return newWave;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.baseCount; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        WaveEnded?.Invoke(wave);
    }

    void SpawnEnemy(Wave wave)
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, wave.enemyPrefabs.Length);
        GameObject enemyPrefab = wave.enemyPrefabs[enemyIndex];
        Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
    }

    void UpdateWaveInfo()
    {
        if (waveNumber < baseWaves.Length)
        {
            Wave nextWave = GenerateWave();
            waveInfoText.text = "Next Wave: " + nextWave.waveName + "\nEnemies: " + nextWave.baseCount;
        }
        else
        {
            waveInfoText.text = "Final Wave Completed!";
        }
    }
}
