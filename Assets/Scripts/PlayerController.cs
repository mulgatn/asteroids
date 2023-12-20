using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] float _acceleration;
	[SerializeField] float _maxVelocity;
	[SerializeField] float _rotationSpeed;
	[SerializeField] float _fireRate;
	[SerializeField] float _invincibleDuration;

	[Header("Shooting")]
	[SerializeField] GameObject _bulletPrefab;
	[SerializeField] GameObject _bulletSpawn;
	[SerializeField] ObjectPool _bulletPool;
	public ObjectPool BulletPool => _bulletPool;

	Rigidbody2D _body;
	SpriteRenderer _renderer;

	bool _accelerating;

	float _fireTimer;
	float _invincibleTimer;

	bool IsInvincible => _invincibleTimer < _invincibleDuration;

	void Start()
	{
		_body = GetComponent<Rigidbody2D>();
		_renderer = GetComponent<SpriteRenderer>();
		_bulletPool.Init(_bulletPrefab);
		Respawn();
	}

	void Update()
	{
		if (_invincibleTimer < _invincibleDuration)
		{
			_invincibleTimer += Time.deltaTime;
			if (_invincibleTimer >= _invincibleDuration)
				_renderer.color = Color.white;
		}

		_accelerating = Input.GetKey(KeyCode.UpArrow);
		HandleRotation();

		if (Input.GetKeyDown(KeyCode.Space) && _fireTimer >= _fireRate)
			Fire();

		_fireTimer += Time.deltaTime;
	}

	void FixedUpdate()
	{
		if (_accelerating)
		{
			_body.AddForce(_acceleration * transform.up);
			_body.velocity = Vector2.ClampMagnitude(_body.velocity, _maxVelocity);
		}
	}

	void Fire()
	{
		var bullet    = _bulletPool.RequestObject().GetComponent<Bullet>();
		var shipSpeed = Mathf.Clamp(Vector2.Dot(_body.velocity, transform.up), 0f, _body.velocity.magnitude);

		bullet.Init(_bulletPool, _bulletSpawn.transform.position, transform.up, shipSpeed);

		_fireTimer = 0;
	}

	void HandleRotation()
	{
		var rotationMultiplier = Input.GetKey(KeyCode.LeftArrow) ? 1f :
									Input.GetKey(KeyCode.RightArrow) ? -1f : 0f;

		transform.Rotate(_rotationSpeed * Time.deltaTime * transform.forward * rotationMultiplier);
	}

	public void Respawn()
	{
		_fireTimer         = 0f;
		_invincibleTimer   = 0;
		_body.velocity     = Vector2.zero;
		transform.position = Vector2.zero;
		_renderer.color    = new(_renderer.color.r, _renderer.color.g, _renderer.color.b, .5f);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (IsInvincible)
			return;

		if (collision.GetComponent<Asteroid>())
		{
			var levelManager = FindAnyObjectByType<LevelManager>();

			levelManager.PlayerDeath(this);
		}
	}
}
