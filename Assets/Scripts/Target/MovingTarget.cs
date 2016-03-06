using UnityEngine;
using System;
using System.Collections;

public class MovingTarget : AbstractTarget
{
	public Vector3 p0 = new Vector3 (-7, 4, 15);
	public Vector3 p1 = new Vector3 (7, 4, 15);
	public float timeDuration = 3;
	private Vector3 p01;
	private float timeStart;
	private bool movingLeftToRight = true;

	protected override void Start ()
	{
		timeStart = Time.time;
		base.Start ();
	}

	// Update is called once per frame
	void Update ()
	{               
		if (Status == STATUS.HIT) {
			Status = STATUS.EXPLODING;
		}

		float u = (Time.time - timeStart) / timeDuration;             
		if (u >= 1) {
			u = 1;
			movingLeftToRight = !movingLeftToRight;
			timeStart = Time.time;
		}             
		// This is the standard linear interpolation function             
		p01 = movingLeftToRight ? ((1 - u) * p0 + u * p1) : ((1 - u) * p1 + u * p0);
		transform.position = p01;
	}

	protected override void OnCollisionEnter (Collision other)
	{
		if (Status == STATUS.NORMAL && (other.gameObject.tag == "Ball" || other.gameObject.tag == "Target")) {
			// Events setup (I put it here, because it caused random errors in Start method, maybe because of concurrency of many targets starting up at the same time...)
			EventControl.Instance.InitTargetEvents (this);
			OnTargetDown (null);
			Status = STATUS.HIT;
		}
	}
}
