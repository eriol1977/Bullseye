using UnityEngine;
using System;
using System.Collections;

public class BallBehavior : MonoBehaviour
{

	public float lifespan = 4.0f;

	private AudioSource throwSound;
	private AudioSource bounceSound;

	public event EventHandler BallThrown;
	public event EventHandler BallDestroyed;

	void Awake ()
	{
		// loads the appropriate sound effects
		foreach (AudioSource a in GetComponents<AudioSource> ()) {
			if (a.clip.name.Equals ("throw"))
				throwSound = a;
			else if (a.clip.name.Equals ("bounce"))
				bounceSound = a;
		}

		// Events setup
		EventControl.Instance.InitBallEvents (this);
	}

	void Start ()
	{
		throwSound.Play ();
		if (BallThrown != null)
			BallThrown (this, null);
	}

	// Update is called once per frame
	void Update ()
	{
		lifespan -= Time.deltaTime;
		if (lifespan <= 0) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter (Collision other)
	{
		bounceSound.Play ();
		//Destroy (gameObject, 2.0f);
	}

	void OnDestroy ()
	{
		if (BallDestroyed != null)
			BallDestroyed (this, null);
	}
}
