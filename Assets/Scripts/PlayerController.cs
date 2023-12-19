using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] float _acceleration;
	[SerializeField] float _maxVelocity;
	[SerializeField] float _rotationSpeed;
	[SerializeField] float _fireRate;


	[Header("Shooting")]
	[SerializeField] GameObject _bulletPrefab;
	[SerializeField] GameObject _bulletSpawn;
	[SerializeField] ObjectPool _bulletPool;
	public ObjectPool BulletPool => _bulletPool;

	Rigidbody2D _body;

	bool _accelerating;

	float _fireTimer;

	void Start()
	{
		_body = GetComponent<Rigidbody2D>();
		_fireTimer = 0f;
		_bulletPool.Init(_bulletPrefab);
	}

	void Update()
	{
		_accelerating = Input.GetKey(KeyCode.UpArrow);
		HandleRotation();

		if (Input.GetKeyDown(KeyCode.Space) && _fireTimer >= _fireRate)
			Fire();

		_fireTimer += Time.deltaTime;
	}

	void Fire()
	{
		var bullet = _bulletPool.RequestObject().GetComponent<Bullet>();
		var shipSpeed = Mathf.Clamp(Vector2.Dot(_body.velocity, transform.up), 0f, _body.velocity.magnitude);

		bullet.Init(_bulletPool, _bulletSpawn.transform.position, transform.up, shipSpeed);

		_fireTimer = 0;
	}

	void FixedUpdate()
	{
		if (_accelerating)
		{
			_body.AddForce(_acceleration * transform.up);
			_body.velocity = Vector2.ClampMagnitude(_body.velocity, _maxVelocity);
		}    
	}

	void HandleRotation()
	{
		var rotationMultiplier = Input.GetKey(KeyCode.LeftArrow) ? 1f :
									Input.GetKey(KeyCode.RightArrow) ? -1f : 0f;

		transform.Rotate(_rotationSpeed * Time.deltaTime * transform.forward * rotationMultiplier);
	}
}