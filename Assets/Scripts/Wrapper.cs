using UnityEngine;

public class Wrapper : MonoBehaviour
{
	void Update()
	{
		var viewportPos = Camera.main.WorldToViewportPoint(transform.position);

		if (viewportPos.x < 0)
			viewportPos.x += 1;
		else if (viewportPos.x > 1)
			viewportPos.x -= 1;
		else if (viewportPos.y < 0)
			viewportPos.y += 1;
		else if (viewportPos.y > 1)
			viewportPos.y -= 1;

		transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
	}
}
