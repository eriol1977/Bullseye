using UnityEngine;
using System;
using System.Collections;

public class EventControl : MonoBehaviour
{
	// SINGLETON CODE

	private static EventControl instance = null;

	public static EventControl Instance {
		get {
			return instance;
		}
	}

	void Awake ()
	{
		//Make this active and only instance
		instance = this;
	}

	private FlowControl fc;

	private ScoreControl sc;

	private LevelControl lc;

	private UIControl uic;

	public void InitEvents ()
	{
		fc = FlowControl.Instance;
		lc = LevelControl.Instance;
		sc = ScoreControl.Instance;
		uic = UIControl.Instance;

		lc.BallsChanged += OnBallsChanged;
		lc.LevelChanged += OnLevelChanged;
		lc.TargetsChanged += OnTargetsChanged;
		sc.ScoreChanged += OnScoreChanged;
	}

	public void InitBallEvents (BallBehavior ball)
	{
		ball.BallThrown += OnBallThrown;
		ball.BallDestroyed += OnBallDestroyed;
	}

	public void InitTargetEvents (TargetBehavior target)
	{
		target.TargetDestroyed += OnTargetDestroyed;
	}

	public void OnLevelChanged (object sender, EventArgs e)
	{
		uic.UpdateLevelLabel ();
	}

	public void OnBallThrown (object sender, EventArgs e)
	{
		lc.Balls--;
		lc.CanShoot = false;
	}

	public void OnBallsChanged (object sender, EventArgs e)
	{
		uic.UpdateBallsLabel ();
	}

	public void OnBallDestroyed (object sender, EventArgs e)
	{
		if (lc.Balls == 0) {
			fc.OnBallsFinished ();
		} else {
			lc.CanShoot = true;
		}
	}

	public void OnTargetsChanged (object sender, EventArgs e)
	{
		uic.UpdateTargetsLabel ();
	}

	public void OnTargetDestroyed (object sender, EventArgs e)
	{
		sc.Score += ((TargetBehavior)sender).ScoreValue;
		uic.UpdateTargetsLabel ();
		lc.Targets--;
		if (lc.Targets == 0) {
			fc.OnTargetsFinished ();
		}
	}

	public void OnScoreChanged (object sender, EventArgs e)
	{
		uic.UpdateScoreLabel ();
	}
}
