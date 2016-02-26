using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UIControl : MonoBehaviour
{
	// SINGLETON CODE

	private static UIControl instance = null;

	public static UIControl Instance {
		get {
			return instance;
		}
	}

	void Awake ()
	{
		//Make this active and only instance
		instance = this;
	}

	public Text scoreLabel;

	public Text levelLabel;

	public Text ballsLabel;

	public Text targetsLabel;

	public void UpdateScoreLabel ()
	{
		if (scoreLabel != null)
			scoreLabel.text = ScoreControl.Instance.Score.ToString ();
	}

	public void UpdateLevelLabel ()
	{
		if (levelLabel != null)
			levelLabel.text = "Level " + LevelControl.Instance.Level.ToString ();
	}

	public void UpdateBallsLabel ()
	{
		if (ballsLabel != null)
			ballsLabel.text = "Balls: " + (LevelControl.Instance.BallsInitial - LevelControl.Instance.BallsThrown).ToString ();
	}

	public void UpdateTargetsLabel ()
	{
		if (targetsLabel != null)
			targetsLabel.text = "Targets: " + LevelControl.Instance.Targets.ToString ();
	}
}
