using UnityEngine;
using System;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{
	public float explodingAngle = 90.0f;

	public int scoreValue = 100;

	public event EventHandler TargetDestroyed;

	private enum STATUS
	{
		NORMAL,
		HIT,
		EXPLODING}
	;

	private STATUS _status;

	private STATUS Status {
		get { return _status; }
		set {
			_status = value;
			if (value == STATUS.EXPLODING)
				Explode ();
		}
	}

	// Use this for initialization
	void Start ()
	{
		Status = STATUS.NORMAL;

		// Events setup
		EventControl.Instance.InitTargetEvents (this);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Status == STATUS.HIT) {
			float angle = Mathf.Abs (Quaternion.Angle (Quaternion.Euler (new Vector3 (0, 0, 0)), transform.rotation)); 
			if (angle >= explodingAngle) {
				Status = STATUS.EXPLODING;
			}
		}
	}

	void OnCollisionEnter (Collision other)
	{
		if (Status == STATUS.NORMAL && (other.gameObject.tag == "Ball" || other.gameObject.tag == "Target"))
			Status = STATUS.HIT;
	}

	private void Explode ()
	{
		// hide object
		gameObject.GetComponent<MeshRenderer> ().enabled = false;
		// stop object movement and rotation, to avoid affecting the explosion
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		// plays the explosion and the related sound effect
		var exp = GetComponent<ParticleSystem> ();
		exp.Play ();
		gameObject.GetComponent<AudioSource> ().Play ();
		// destroys the object
		Destroy (gameObject, 0.7f); // this gives enough time to the sound to play
	}

	public int ScoreValue {
		get {
			return scoreValue;
		}
	}

	void OnDestroy ()
	{
		if (TargetDestroyed != null)
			TargetDestroyed (this, null);
	}
}
