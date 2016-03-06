using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
	public float mouseSensitivity = 2.0f;

	float rotLeftRight = 0;
	private float leftRightRange = 179.0f;

	float rotUpDown = 0;
	private float upDownRange = 60.0f;
	
	// Update is called once per frame
	void Update ()
	{
		rotLeftRight -= Input.GetAxis ("Mouse X") * mouseSensitivity;
		rotLeftRight = Mathf.Clamp (rotLeftRight, -leftRightRange, leftRightRange);

		rotUpDown -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		rotUpDown = Mathf.Clamp (rotUpDown, -upDownRange, upDownRange);

		Camera.main.transform.localRotation = Quaternion.Euler (rotUpDown, -rotLeftRight, 0);
	}
}
