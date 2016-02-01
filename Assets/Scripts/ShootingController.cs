using UnityEngine;
using System.Collections;

public class ShootingController : MonoBehaviour
{
	public GameObject ballPrefab;
	public float ballImpulse = 10.0f;
	public Vector3 heightOffset = new Vector3 (0, 0.3f, 0);
	// so that the ball starts a little higher than the middle of the screen
	public Vector3 verticalImpulseOffset = new Vector3 (0, 8.0f, 0);
	// so that the ball is not launched horizontally, almost directed to the ground

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Camera cam = Camera.main;
		if (Input.GetButtonDown ("Fire1")) {
			GameObject ball = (GameObject)Instantiate (ballPrefab, cam.transform.position + cam.transform.forward + heightOffset, cam.transform.rotation);
			ball.GetComponent <Rigidbody> ().AddForce ((cam.transform.forward * ballImpulse) + verticalImpulseOffset, ForceMode.Impulse);
		}
	}
}
