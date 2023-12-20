using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
	[SerializeField] int2 _speedRange;
	Rigidbody2D _body;
	int _size;
	public int Size => _size;

	public void Init(int size, Vector2 position)
	{
		_size              = size;
		_body              = GetComponent<Rigidbody2D>();
		transform.position = position;
		_body.velocity     = Vector2.zero;

		var direction = new Vector2(UnityEngine.Random.value, UnityEngine.Random.value).normalized;

		_body.AddForce(UnityEngine.Random.Range(_speedRange.x, _speedRange.y) * direction, ForceMode2D.Impulse);

		transform.localScale = new(_size - 0.5f, _size - 0.5f);
	}
}
