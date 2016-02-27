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

	private Boolean levelWon = false;

	private object lastTarget = null;

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

	public void InitTargetEvents (AbstractTarget target)
	{
		target.TargetDown += OnTargetDown;
		target.TargetDestroyed += OnTargetDestroyed;
	}

	public void OnLevelChanged (object sender, EventArgs e)
	{
		uic.UpdateLevelLabel ();
	}

	public void OnBallThrown (object sender, EventArgs e)
	{
		lc.BallsThrown++;
		if (lc.BallsThrown == lc.BallsInitial)
			lc.CanShoot = false;
		uic.UpdateBallsLabel ();
	}

	public void OnBallsChanged (object sender, EventArgs e)
	{
		uic.UpdateBallsLabel ();
	}

	public void OnBallDestroyed (object sender, EventArgs e)
	{
		lc.BallsDestroyed++;
		if (lc.BallsDestroyed == lc.BallsInitial) {
			if (levelWon)
				fc.OnTargetsFinished ();
			else
				fc.OnBallsFinished ();
		}
	}

	public void OnTargetsChanged (object sender, EventArgs e)
	{
		uic.UpdateTargetsLabel ();
	}

	public void OnTargetDown (object sender, EventArgs e)
	{
		var t = (AbstractTarget)sender;
		sc.Score += t.ScoreValue ();
		uic.UpdateTargetsLabel ();
		if (t.IsMandatory ()) {
			lc.Targets--;
			if (lc.Targets == 0) {
				levelWon = true;
				lastTarget = sender;
				lc.CanShoot = false;
			}
		}
	}

	public void OnTargetDestroyed (object sender, EventArgs e)
	{
		var t = (AbstractTarget)sender;
		if (t.IsMandatory () && levelWon && (sender == lastTarget))
			fc.OnTargetsFinished ();
	}

	public void OnScoreChanged (object sender, EventArgs e)
	{
		uic.UpdateScoreLabel ();
	}
}
