using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UIControl : MonoBehaviour
{

	public Text scoreLabel;

	public Text levelLabel;

	public Text ballsLabel;

	private ScoreControl sc;

	private LevelControl lc;

	public void Init (ScoreControl sc, LevelControl lc)
	{
		this.sc = sc;
		this.lc = lc;

		this.sc.ScoreChanged += HandleScoreChanged;
		this.lc.LevelChanged += HandleLevelChanged;
		this.lc.BallsChanged += HandleBallsChanged;
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

	private void UpdateScoreLabel ()
	{
		if (scoreLabel != null)
			scoreLabel.text = sc.Score.ToString ();
	}

	private void UpdateLevelLabel ()
	{
		if (levelLabel != null)
			levelLabel.text = "Level " + lc.Level.ToString ();
	}

	private void UpdateBallsLabel ()
	{
		if (ballsLabel != null)
			ballsLabel.text = "Balls: " + lc.Balls.ToString ();
	}
}
