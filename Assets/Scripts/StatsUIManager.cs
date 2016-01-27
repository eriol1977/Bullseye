using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class StatsUIManager : MonoBehaviour
{
	public Text scoreLabel;


	// Use this for initialization
	void Start ()
	{
		GameManager.Instance.ScoreChanged += HandleScoreChanged;
	}

	public void HandleScoreChanged (object sender, EventArgs e)
	{
		UpdateScoreLabel ();
	}

	private void UpdateScoreLabel ()
	{
		if (scoreLabel != null)
			scoreLabel.text = GameManager.Instance.Score.ToString ();
	}
}
