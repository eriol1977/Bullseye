using UnityEngine;
using System;
using System.Collections;

public class ScoreControl : MonoBehaviour
{

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
