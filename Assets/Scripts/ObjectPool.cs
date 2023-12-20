using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	GameObject _prefab;
	Queue<GameObject> _pool;
	List<GameObject> _activeObjects;
	public int ActiveObjectNum => _activeObjects.Count;

	public void Init(GameObject prefab)
	{
		_prefab        = prefab;
		_pool          = new();
		_activeObjects = new();
	}

	public GameObject RequestObject()
	{
		if (_pool.Count > 0)
		{
			var newObject = _pool.Dequeue();
			newObject.SetActive(true);
			_activeObjects.Add(newObject);
			return newObject;
		}
		else
		{
			var newObject = Instantiate(_prefab);
			newObject.transform.parent = transform;
			_activeObjects.Add(newObject);
			return newObject;
		}
	}

	public void RemoveObject(GameObject gameObject)
	{
		gameObject.SetActive(false);
		_activeObjects.Remove(gameObject);
		_pool.Enqueue(gameObject);
	}
}
