using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
	[SerializeField] ObjectPool _asteroidPool;
	[SerializeField] GameObject _asteroidPrefab;

	void Start()
	{
		_asteroidPool.Init(_asteroidPrefab);    
	}
}
