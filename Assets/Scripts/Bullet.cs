using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
	[SerializeField] float _lifeTime;
	[SerializeField] float _speed;

	Rigidbody2D _body;
	ObjectPool _pool;

	float _timer;

	public void Init(ObjectPool pool, Vector2 position, Vector2 direction, float shipVelocity)
	{
		_pool              = pool;
		_body              = GetComponent<Rigidbody2D>();
		transform.position = position;
		_body.velocity     = direction * shipVelocity;

		_timer = 0;

        _body.AddForce(_speed * direction, ForceMode2D.Impulse);
	}

    void Update()
    {
		_timer += Time.deltaTime;

		if (_timer >= _lifeTime)
			DisableBullet();
    }

    void DisableBullet()
	{
		if (_pool != null)
			_pool.RemoveObject(this.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Asteroid asteroid))
		{
			var levelManager = FindAnyObjectByType<LevelManager>();

			levelManager.AsteroidDestroyed(asteroid);

			DisableBullet();
        }
	}
}
