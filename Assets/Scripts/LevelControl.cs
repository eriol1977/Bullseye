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
		ballsInitial = 0;
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

	private int ballsInitial;

	private int ballsThrown;

	private int ballsDestroyed;

	public event EventHandler BallsChanged;

	public int BallsInitial {
		get {
			return ballsInitial;
		}
		set {
			ballsInitial = value;
			ballsThrown = 0;
			ballsDestroyed = 0;

			//Fire the event - notifying all subscribers
			if (BallsChanged != null)
				BallsChanged (this, null);
		}
	}

	public int BallsThrown {
		get {
			return ballsThrown;
		}
		set {
			ballsThrown = value;
		}
	}

	public int BallsDestroyed {
		get {
			return ballsDestroyed;
		}
		set {
			ballsDestroyed = value;
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
