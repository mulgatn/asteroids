using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject _asteroidPrefab;
    [SerializeField] float _spawnDistance;
    [SerializeField] 


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnAsteroid();
    }

    void SpawnAsteroid()
    {
        Vector2 spawnPoint = Random.insideUnitCircle.normalized * _spawnDistance;
        Instantiate(_asteroidPrefab, spawnPoint, Quaternion.identity);
    }
}
