using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] LevelLibrary _levelLibrary;
	[SerializeField] ObjectPool _asteroidPool;
	[SerializeField] GameObject _asteroidPrefab;

	[SerializeField] PlayerUI _playerUI;

	int _currentLevel = 0;

	public int _score = 0;

	void Start()
	{
		_asteroidPool.Init(_asteroidPrefab);
		StartLevel(_currentLevel);
        _playerUI.SetScoreText(_score);
    }

	void StartLevel(int level)
	{
		var levelInfo = _levelLibrary.library[level];

        var asteroidCount = levelInfo.asteroidNum;
		var size = levelInfo.asteroidScore.Count;


        for (int i = 0; i < asteroidCount; i++)
		{
            float offset = Random.Range(0f, 1f);
            Vector2 viewportSpawnPosition = Vector2.zero;

            int edge = Random.Range(0, 4);
            if (edge == 0)
                viewportSpawnPosition = new(offset, 0);
            else if (edge == 1)
                viewportSpawnPosition = new(offset, 1);
            else if (edge == 2)
                viewportSpawnPosition = new(0, offset);
            else if (edge == 3)
                viewportSpawnPosition = new(1, offset);

            Vector2 spawnPos = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
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
}
