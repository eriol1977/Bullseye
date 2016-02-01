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

	public void Init ()
	{
		ScoreControl.Instance.ScoreChanged += HandleScoreChanged;
		LevelControl.Instance.LevelChanged += HandleLevelChanged;
		LevelControl.Instance.BallsChanged += HandleBallsChanged;
	}

	public void HandleScoreChanged (object sender, EventArgs e)
	{
		UpdateScoreLabel ();
	}

	public void HandleLevelChanged (object sender, EventArgs e)
	{
		UpdateLevelLabel ();
	}

	public void HandleBallsChanged (object sender, EventArgs e)
	{
		UpdateBallsLabel ();
	}

	public void HandleTargetsChanged (object sender, EventArgs e)
	{
		UpdateTargetsLabel ();
	}

	private void UpdateScoreLabel ()
	{
		if (scoreLabel != null)
			scoreLabel.text = ScoreControl.Instance.Score.ToString ();
	}

	private void UpdateLevelLabel ()
	{
		if (levelLabel != null)
			levelLabel.text = "Level " + LevelControl.Instance.Level.ToString ();
	}

	private void UpdateBallsLabel ()
	{
		if (ballsLabel != null)
			ballsLabel.text = "Balls: " + LevelControl.Instance.Balls.ToString ();
	}

	private void UpdateTargetsLabel ()
	{
		if (targetsLabel != null)
			targetsLabel.text = "Targets: " + LevelControl.Instance.Targets.ToString ();
	}
}
