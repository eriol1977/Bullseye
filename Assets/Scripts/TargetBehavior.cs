using UnityEngine;
using System;
using System.Collections;

public class TargetBehavior : AbstractTarget
{
	public float explodingAngle = 90.0f;
	
	// Update is called once per frame
	void Update ()
	{
		if (Status == STATUS.HIT) {
			float angle = Mathf.Abs (Quaternion.Angle (Quaternion.Euler (new Vector3 (0, 0, 0)), transform.rotation)); 
			if (angle >= explodingAngle) {
				OnTargetDown (null);
				Status = STATUS.EXPLODING;
			}
		}
	}
}
