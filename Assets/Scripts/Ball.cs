using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

	public float lifespan = 4.0f;

	// Use this for initialization
	void Start ()
	{
	
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
		if (other.gameObject.tag.Equals ("Target")) {
			Destroy (gameObject);
		}
	}
}
