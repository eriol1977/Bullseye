using UnityEngine;
using System.Collections;

public class MoveOnTrails : MonoBehaviour
{
	public float speed = 10.0f;

	void Update ()
	{
		Vector3 p = transform.position;
		transform.position = new Vector3 (p.x, p.y, p.z + (speed * Time.deltaTime));
	}
}
