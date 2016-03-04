using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShootingController : MonoBehaviour
{
	public GameObject ballPrefab;

	public float minImpulse = 2.0f;

	public float maxImpulse = 15.0f;

	public float impulseStep = 0.2f;

	public float ballImpulse;

	// so that the ball is not launched horizontally, almost directed to the ground
	private Vector3 verticalImpulseOffset;

	// so that the ball starts a little higher than the middle of the screen
	public Vector3 heightOffset = new Vector3 (0, 0.4f, 0);

	private bool isThrowing = false;

	// TODO use events!
	private Slider powerSlider;

	public Image fill;

	private Color minImpulseColor = Color.yellow;

	private Color maxImpulseColor = Color.red;

	public Gradient impulseGradient;

	void Start ()
	{
		powerSlider = GameObject.Find ("PowerSlider").GetComponent<Slider> ();
		if (powerSlider == null) {
			// TODO use exceptions
			Debug.Log ("No PowerSlider found in the scene.");
		} else {
			powerSlider.minValue = minImpulse;
			powerSlider.maxValue = maxImpulse;
		}
	}

	// Update is called once per frame
	void Update ()
	{
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
				verticalImpulseOffset = new Vector3 (0, ballImpulse / 1.25f, 0);
				ball.GetComponent <Rigidbody> ().AddForce ((cam.transform.forward * ballImpulse) + verticalImpulseOffset, ForceMode.Impulse);
				isThrowing = false;
				ballImpulse = minImpulse;
				UpdatePowerSlider (ballImpulse);
			}
		}
	}

	private void UpdatePowerSlider (float value)
	{
		powerSlider.value = value;
		fill.color = Color.Lerp (minImpulseColor, maxImpulseColor, value / maxImpulse);
		// TODO
		//fill.color = impulseGradient.Evaluate (value / ((value - minImpulse) / (maxImpulse - minImpulse)));
	}
}
