using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	[SerializeField] LevelLibrary _levelLibrary;

	[SerializeField] ObjectPool _asteroidPool;
	[SerializeField] GameObject _asteroidPrefab;

	[SerializeField] PlayerUI _playerUI;

	int _currentLevel = 0;

	int _score;
	int _lives;

	void Start()
	{
		_asteroidPool.Init(_asteroidPrefab);
		StartLevel(_currentLevel);

		_score = 0;
		_lives = 3;

		_playerUI.SetScoreText(_score);
		_playerUI.SetLivesText(_lives);
	}

	void StartLevel(int level)
	{
		var levelInfo = _levelLibrary.library[level];

		var asteroidCount = levelInfo.asteroidNum;
		var size = levelInfo.asteroidScore.Count;

		for (int i = 0; i < asteroidCount; i++)
		{
			float offset = Random.Range(0f, 1f);
			Vector2 viewportSpawnPos = Vector2.zero;

			int edge = Random.Range(0, 4);
			if (edge == 0)
				viewportSpawnPos = new(offset, 0);
			else if (edge == 1)
				viewportSpawnPos = new(offset, 1);
			else if (edge == 2)
				viewportSpawnPos = new(0, offset);
			else if (edge == 3)
				viewportSpawnPos = new(1, offset);

			Vector2 spawnPos = Camera.main.ViewportToWorldPoint(viewportSpawnPos);
			SpawnAsteroid(size, spawnPos);
		}
	}

	public void AsteroidDestroyed(Asteroid asteroid)
	{
		if (asteroid.Size != 1)
		{
			for (int i = 0; i < 2; i++)
				SpawnAsteroid(asteroid.Size - 1, asteroid.transform.position);
		}

		_score += _levelLibrary.library[_currentLevel].asteroidScore[asteroid.Size - 1];
		_playerUI.SetScoreText(_score);

		_asteroidPool.RemoveObject(asteroid.gameObject);

		if (_asteroidPool.ActiveObjectNum == 0)
		{
			_currentLevel = Mathf.Min(_currentLevel + 1, _levelLibrary.library.Count - 1);
			StartLevel(_currentLevel);
		}
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
			StartLevel(0);
	}

	void SpawnAsteroid(int size, Vector2 pos)
	{
		var asteroid = _asteroidPool.RequestObject().GetComponent<Asteroid>();

		asteroid.Init(size, pos);
	}

	public void PlayerDeath(PlayerController player)
	{
		_lives--;
		_playerUI.SetLivesText(_lives);
		if (_lives == 0)
		{
			PlayerPrefs.SetInt("Score", _score);
			SceneManager.LoadScene(2);
		}
		else
		{
			player.Respawn();
		}
	}
}
