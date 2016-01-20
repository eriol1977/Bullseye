using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
	public float mouseSensitivity = 2.0f;

	float rotLeftRight = 0;
	public float leftRightRange = 60.0f;

	float rotUpDown = 0;
	public float upDownRange = 60.0f;

	CharacterController cc;

	// Use this for initialization
	void Awake ()
	{
		cc = GetComponent<CharacterController> ();
	}
	
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
