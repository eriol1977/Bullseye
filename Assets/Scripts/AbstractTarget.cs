using UnityEngine;
using System;
using System.Collections;

public abstract class AbstractTarget : MonoBehaviour
{
	public event EventHandler TargetDown;

	public event EventHandler TargetDestroyed;

	public abstract bool IsMandatory ();

	public abstract int ScoreValue ();

	protected enum STATUS
	{
		NORMAL,
		HIT,
		EXPLODING}
	;

	protected STATUS _status;

	protected STATUS Status {
		get { return _status; }
		set {
			_status = value;
			if (value == STATUS.EXPLODING)
				Explode ();
		}
	}

	protected virtual void Start ()
	{
		Status = STATUS.NORMAL;
	}

	protected virtual void OnTargetDown (EventArgs e)
	{
		EventHandler invoker = TargetDown;
		if (invoker != null)
			invoker (this, e);
	}

	protected virtual void OnTargetDestroyed (EventArgs e)
	{
		EventHandler invoker = TargetDestroyed;
		if (invoker != null)
			invoker (this, e);
	}

	protected virtual void OnCollisionEnter (Collision other)
	{
		if (Status == STATUS.NORMAL && (other.gameObject.tag == "Ball" || other.gameObject.tag == "Target")) {
			// Events setup (I put it here, because it caused random errors in Start method, maybe because of concurrency of many targets starting up at the same time...)
			EventControl.Instance.InitTargetEvents (this);
			Status = STATUS.HIT;
		}
	}

	protected void Explode ()
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

	void OnDestroy ()
	{
		OnTargetDestroyed (null);
	}
}
