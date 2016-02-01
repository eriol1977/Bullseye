using UnityEngine;

using System;
using System.Collections;

public class LevelControl : MonoBehaviour
{
	// SINGLETON CODE

	private static LevelControl instance = null;

	public static LevelControl Instance {
		get {
			return instance;
		}
	}

	void Awake ()
	{
		//Make this active and only instance
		instance = this;
		level = 0;
		balls = 0;
	}
		
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

	private int totBalls = -1;

	public event EventHandler BallsChanged;

	public event EventHandler BallsFinished;

	public void OnBallThrown (object sender, EventArgs e)
	{
		Balls--;
	}

	public void OnBallDestroyed (object sender, EventArgs e)
	{
		totBalls--;
		if (totBalls == 0) {
			BallsFinished (this, null);
		}
	}

	public int Balls {
		get {
			return balls;
		}
		set {
			balls = value;

			//Init balls' total
			if (totBalls == -1)
				totBalls = value;

			//Fire the event - notifying all subscribers
			if (BallsChanged != null)
				BallsChanged (this, null);
		}
	}

	// TARGETS CODE

	private int targets;

	public event EventHandler TargetsFinished;

	public void OnTargetDestroyed (object sender, EventArgs e)
	{
		Targets--;
		if (Targets == 0) {
			TargetsFinished (this, null);
		}
	}

	public int Targets { 
		get {
			return targets;
		}
		set {
			targets = value;
		}
	}
}
