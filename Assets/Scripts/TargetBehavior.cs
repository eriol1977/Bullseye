using UnityEngine;
using System;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{
	public float explodingAngle = 90.0f;

	public int scoreValue = 100;

	public event EventHandler TargetDown;

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
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Status == STATUS.HIT) {
			float angle = Mathf.Abs (Quaternion.Angle (Quaternion.Euler (new Vector3 (0, 0, 0)), transform.rotation)); 
			if (angle >= explodingAngle) {
				if (TargetDown != null)
					TargetDown (this, null);
				Status = STATUS.EXPLODING;
			}
		}
	}

	void OnCollisionEnter (Collision other)
	{
		if (Status == STATUS.NORMAL && (other.gameObject.tag == "Ball" || other.gameObject.tag == "Target")) {
			// Events setup (I put it here, because it caused random errors in Start method, maybe because of concurrency of many targets starting up at the same time...)
			EventControl.Instance.InitTargetEvents (this);
			Status = STATUS.HIT;
		}
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
