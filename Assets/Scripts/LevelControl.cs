using UnityEngine;

using System;
using System.Collections;

public class LevelControl : MonoBehaviour
{
	// LEVEL CODE

	private int level;

	public event EventHandler LevelChanged;

	public int Level {
		get {
			return level;
		}
		set {
			level = value;

			//Fire the event - notifying all subscribers
			if (LevelChanged != null)
				LevelChanged (this, null);
		}
	}

	// BALLS CODE

	private int balls;

	public event EventHandler BallsChanged;

	public event EventHandler BallsFinished;

	public int Balls {
		get {
			return balls;
		}
		set {
			balls = value;

			//Fire the event - notifying all subscribers
			if (BallsChanged != null)
				BallsChanged (this, null);

			if (balls == 0) {
				BallsFinished (this, null);
			}
		}
	}

}
