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
		InitImpulseGradient ();
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

	//////////////////// POWER SLIDER

	public Slider powerSlider;

	public Image fill;

	private Gradient impulseGradient;

	private readonly Color[] impulseGradientColors = { Color.green, Color.yellow, new Color (254, 161, 0), Color.red };

	private readonly float[] impulseGradientTimes = { 0.0f, 0.33f, 0.66f, 1.0f };

	private void InitImpulseGradient ()
	{
		impulseGradient = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[impulseGradientColors.Length];
		GradientAlphaKey[] gak = new GradientAlphaKey[impulseGradientColors.Length];
		for (int i = 0; i < impulseGradientColors.Length; i++) {
			gck [i].color = impulseGradientColors [i];
			gak [i].alpha = 1.0f;
			gck [i].time = gak [i].time = impulseGradientTimes [i];
		}
		impulseGradient.SetKeys (gck, gak);
	}

	public void InitPowerSlider (float min, float max)
	{
		powerSlider.minValue = min;
		powerSlider.maxValue = max;
	}

	public void UpdatePowerSlider (float value)
	{
		powerSlider.value = value;
		fill.color = impulseGradient.Evaluate ((value - powerSlider.minValue) / (powerSlider.maxValue - powerSlider.minValue));
	}
}
