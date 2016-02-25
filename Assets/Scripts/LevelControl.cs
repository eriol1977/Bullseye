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

	public event EventHandler BallsChanged;

	public int Balls {
		get {
			return balls;
		}
		set {
			balls = value;

			//Fire the event - notifying all subscribers
			if (BallsChanged != null)
				BallsChanged (this, null);
		}
	}

	public bool CanShoot = true;

	// TARGETS CODE

	private int targets;

	public event EventHandler TargetsChanged;

	public int Targets { 
		get {
			return targets;
		}
		set {
			targets = value;

			//Fire the event - notifying all subscribers
			if (TargetsChanged != null)
				TargetsChanged (this, null);
		}
	}
}
