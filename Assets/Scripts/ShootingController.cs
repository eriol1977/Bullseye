using UnityEngine;
using System.Collections;

public class ShootingController : MonoBehaviour
{
	public GameObject ballPrefab;
	public float ballImpulse = 10.0f;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Camera cam = Camera.main;
		if (Input.GetButtonDown ("Fire1")) {
			GameObject bullet = (GameObject)Instantiate (ballPrefab, cam.transform.position + cam.transform.forward, cam.transform.rotation);
			bullet.GetComponent <Rigidbody> ().AddForce (cam.transform.forward * ballImpulse, ForceMode.Impulse);
		}
	}
}
