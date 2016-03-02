using UnityEngine;
using System.Collections;

public class MoveOnTrails : MonoBehaviour
{
	public float speed = 10.0f;

	public float bouncingReduction = 0.2f;

	private bool bouncing = false;

	void Update ()
	{
		Vector3 p = transform.position;
		transform.position = new Vector3 (p.x, p.y, p.z + (speed * Time.deltaTime));
		if (bouncing) {
			speed += bouncingReduction;
			if (speed >= 0)
				StopMotion ();
		}
	}

	public void Bounce ()
	{
		speed = -speed;
		bouncing = true;
	}

	public void StopMotion ()
	{
		speed = 0;
	}
}
