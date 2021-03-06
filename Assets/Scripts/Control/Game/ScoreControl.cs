﻿using UnityEngine;
using System;
using System.Collections;

public class ScoreControl : MonoBehaviour
{
	// SINGLETON CODE

	private static ScoreControl instance = null;

	public static ScoreControl Instance {
		get {
			return instance;
		}
	}

	void Awake ()
	{
		//Make this active and only instance
		instance = this;
		score = 0;
	}


	// SCORE
	private int score;

	public event EventHandler ScoreChanged;

	public int Score {
		get {
			return score;
		}
		set {
			score = value;

			//Fire the event - notifying all subscribers
			if (ScoreChanged != null)
				ScoreChanged (this, null);
		}
	}


}
