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

		lc.LevelChanged += uic.HandleLevelChanged;
		lc.BallsChanged += uic.HandleBallsChanged;
		lc.TargetsChanged += uic.HandleTargetsChanged;
		sc.ScoreChanged += uic.HandleScoreChanged;

		lc.TargetsFinished += fc.OnTargetsFinished;
		lc.BallsFinished += fc.OnBallsFinished;
	}

	public void InitBallEvents (BallBehavior ball)
	{
		ball.BallThrown += lc.OnBallThrown;
		ball.BallDestroyed += lc.OnBallDestroyed;
	}

	public void InitTargetEvents (TargetBehavior target)
	{
		target.TargetDestroyed += sc.OnTargetDestroyed;
		target.TargetDestroyed += uic.HandleTargetsChanged;
		target.TargetDestroyed += lc.OnTargetDestroyed;
	}
}
