using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ShootingController : MonoBehaviour
{
	public GameObject ballPrefab;

	private readonly float minImpulse = 3.0f;

	private readonly float maxImpulse = 15.0f;

	private readonly float impulseStep = 0.1f;

	private float ballImpulse;

	// so that the ball is not launched horizontally, almost directed to the ground
	private Vector3 verticalImpulseOffset;

	// factor applied to the ball impulse to determine a stronger vertical component
	private readonly float verticalImpulseFactor = 1.3f;

	// so that the ball starts a little higher than the middle of the screen
	private readonly Vector3 heightOffset = new Vector3 (0, 0.4f, 0);

	private bool isThrowing = false;

	public delegate void InitPowerSliderHandler (float min, float max);

	public event InitPowerSliderHandler InitPowerSlider;

	public delegate void UpdatePowerSliderHandler (float value);

	public event UpdatePowerSliderHandler UpdatePowerSlider;

	private bool powerSliderInitialized = false;

	// Update is called once per frame
	void Update ()
	{
		if (!powerSliderInitialized) {
			InitPowerSlider (minImpulse, maxImpulse);
			powerSliderInitialized = true;
		}

		if (LevelControl.Instance.CanShoot) {

			if (Input.GetButtonDown ("Fire1")) {
				isThrowing = true;
				ballImpulse = minImpulse;
				UpdatePowerSlider (ballImpulse);
			}

			if (isThrowing && Input.GetButton ("Fire1")) {
				if (ballImpulse <= maxImpulse)
					ballImpulse += impulseStep;
				if (ballImpulse > maxImpulse)
					ballImpulse = maxImpulse;
				UpdatePowerSlider (ballImpulse);
			}
				
			if (isThrowing && Input.GetButtonUp ("Fire1")) {
				Camera cam = Camera.main;
				GameObject ball = (GameObject)Instantiate (ballPrefab, cam.transform.position + cam.transform.forward + heightOffset, cam.transform.rotation);
				verticalImpulseOffset = new Vector3 (0, ballImpulse / verticalImpulseFactor, 0);
				ball.GetComponent <Rigidbody> ().AddForce ((cam.transform.forward * ballImpulse) + verticalImpulseOffset, ForceMode.Impulse);
				isThrowing = false;
				ballImpulse = minImpulse;
				UpdatePowerSlider (ballImpulse);
			}
		}
	}
}
