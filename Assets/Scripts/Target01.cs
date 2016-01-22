using UnityEngine;
using System.Collections;

public class Target01 : MonoBehaviour
{
	public GameObject explosionPrefab;

	public float lifeSpan = 3.0f;

	private enum STATUS
	{
		NORMAL,
		HIT,
		EXPLODING,
		EXPLODED}
	;

	private STATUS _status;

	private STATUS Status {
		get { return _status; }
		set {
			_status = value;
			if (value == STATUS.EXPLODED)
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
			if (angle >= 90.0f)
				Status = STATUS.EXPLODING;
		} else if (Status == STATUS.EXPLODING) {
			lifeSpan -= Time.deltaTime;
			if (lifeSpan <= 0) {
				Status = STATUS.EXPLODED;
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
		// BUG! it seems to be related to Unity v.5.3.1
		// http://answers.unity3d.com/questions/1114805/instantiating-prefab-gives-isfinite-error.html
		//Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
