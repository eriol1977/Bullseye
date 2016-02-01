using UnityEngine;
using System;
using System.Collections;

public class Ball : MonoBehaviour
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
		BallThrown += LevelControl.Instance.OnBallThrown;
		BallDestroyed += LevelControl.Instance.OnBallDestroyed;
	}

	void Start ()
	{
		throwSound.Play ();
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
		if (other.gameObject.tag.Equals ("Target")) {
			Destroy (gameObject, 1.0f);
		}
	}

	void OnDestroy ()
	{
		BallDestroyed (this, null);
	}
}
