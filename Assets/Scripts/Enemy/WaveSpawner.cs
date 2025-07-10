using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Thêm namespace cho UI

public class WaveSpawner : MonoBehaviour
{
	[System.Serializable]
	public class Enemy
	{
		public GameObject enemyPrefab;
		public int cost;
		public int minWave; // Minimum wave this enemy can appear in
	}

	public List<Enemy> enemies = new List<Enemy>();
	public List<Transform> spawnLocations; // List of spawn points
	public int currentWave = 0; // Start at 0, increment to 1 for first wave
	public int waveValue;
	public List<GameObject> enemiesToSpawn = new List<GameObject>();
	public List<GameObject> activeEnemies = new List<GameObject>(); // Track active enemies

	public int waveDuration = 30; // Duration of each wave (seconds)
	private float waveTimer;
	private float spawnInterval;
	private float spawnTimer;
	public float waveInterval = 5f; // Delay between waves
	public float uiDisplayTime = 3f; // Thời gian hiển thị UI (giây)

	private bool isWaveActive = false;

	[SerializeField] private TMP_Text waveText; // Gán Text UI từ Inspector

	void Start()
	{
		StartNewWave();
	}

	void FixedUpdate()
	{
		if (!isWaveActive) return;

		if (spawnTimer <= 0 && enemiesToSpawn.Count > 0)
		{
			Transform randomSpawn = spawnLocations[Random.Range(0, spawnLocations.Count)];
			GameObject spawnedEnemy = Instantiate(enemiesToSpawn[0], randomSpawn.position, Quaternion.identity);
			enemiesToSpawn.RemoveAt(0);
			activeEnemies.Add(spawnedEnemy);
			spawnTimer = spawnInterval;
		}
		else if (enemiesToSpawn.Count == 0 && activeEnemies.Count == 0 && isWaveActive)
		{
			StartCoroutine(StartNextWaveWithDelay());
		}
		else
		{
			spawnTimer -= Time.fixedDeltaTime;
			waveTimer -= Time.fixedDeltaTime;
		}

		activeEnemies.RemoveAll(enemy => enemy == null);
	}

	public void StartNewWave()
	{
		currentWave++;
		waveValue = currentWave * 10;
		GenerateEnemies();

		spawnInterval = waveDuration / (float)enemiesToSpawn.Count;
		waveTimer = waveDuration;
		spawnTimer = 0;
		isWaveActive = true;

		Debug.Log($"Starting Wave {currentWave} with {enemiesToSpawn.Count} enemies.");
	}

	IEnumerator StartNextWaveWithDelay()
	{
		isWaveActive = false;
		ShowWaveUI(); // Hiển thị UI khi wave kết thúc
		yield return new WaitForSeconds(uiDisplayTime); // Chờ thời gian hiển thị UI
		HideWaveUI(); // Ẩn UI
		Debug.Log($"Wave {currentWave} completed! Waiting {waveInterval} seconds for next wave.");
		yield return new WaitForSeconds(waveInterval - uiDisplayTime); // Chờ phần còn lại của waveInterval
		StartNewWave();
	}

	private void ShowWaveUI()
	{
		if (waveText != null)
		{
			waveText.text = $"Wave {currentWave} Completed!\nNext Wave: {currentWave + 1}";
			waveText.gameObject.SetActive(true); // Hiển thị text
		}
	}

	private void HideWaveUI()
	{
		if (waveText != null)
		{
			waveText.gameObject.SetActive(false); // Ẩn text
		}
	}

	public void GenerateEnemies()
	{
		List<GameObject> generatedEnemies = new List<GameObject>();
		int remainingValue = waveValue;

		List<Enemy> availableEnemies = enemies.FindAll(enemy => enemy.minWave <= currentWave);

		while (remainingValue > 0 && availableEnemies.Count > 0)
		{
			int randEnemyId = Random.Range(0, availableEnemies.Count);
			Enemy selectedEnemy = availableEnemies[randEnemyId];
			int randEnemyCost = selectedEnemy.cost;

			if (remainingValue >= randEnemyCost)
			{
				generatedEnemies.Add(selectedEnemy.enemyPrefab);
				remainingValue -= randEnemyCost;
			}
			else
			{
				availableEnemies.RemoveAt(randEnemyId);
			}
		}

		enemiesToSpawn.Clear();
		enemiesToSpawn = generatedEnemies;

		for (int i = 0; i < enemiesToSpawn.Count - 1; i++)
		{
			int randIndex = Random.Range(i, enemiesToSpawn.Count);
			GameObject temp = enemiesToSpawn[i];
			enemiesToSpawn[i] = enemiesToSpawn[randIndex];
			enemiesToSpawn[randIndex] = temp;
		}
	}
}