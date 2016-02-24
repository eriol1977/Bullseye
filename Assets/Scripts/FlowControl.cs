using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;


public class FlowControl : MonoBehaviour
{
	private int level;

	public int Level {
		get {
			return level;
		}
	}

	private Player player;

	public Player Player {
		get {
			return player;
		}
		set {
			player = value;
		}
	}

	// SINGLETON CODE

	private static FlowControl instance = null;

	public static FlowControl Instance {
		get {
			return instance;
		}
	}

	void Awake ()
	{
		//Check if existing instance of class exists in scene
		//If so, then destroy this instance
		if (instance) {
			DestroyImmediate (gameObject);
		} else {
			//Make this active and only instance
			instance = this;
			//Make game manager persistent
			DontDestroyOnLoad (gameObject);

			status = STATUS.START_SCREEN;
			level = 0;
		}
	}

	private ScoreControl sc;

	private LevelControl lc;

	public void Proceed ()
	{
		if (Status == STATUS.PLAYING) {
			EventControl.Instance.InitEvents ();

			lc = LevelControl.Instance;
			lc.Level = level;

			Level lev = DataControl.Instance.GetLevel (level);
			lc.Balls = lev.Balls; 
			lc.Targets = lev.Targets;

			sc = ScoreControl.Instance;
			sc.Score = 0;

			Cursor.visible = false;
		} else {
			Cursor.visible = true;
		}
	}

	public void LoadLevel (int level)
	{
		this.level = level;
		Status = STATUS.PLAYING;
	}

	public void ReloadLevel ()
	{
		Status = STATUS.PLAYING;
	}

	public void LoadNextLevel ()
	{
		level++;
		Status = STATUS.PLAYING;
	}

	public void ChooseLevel ()
	{
		Status = STATUS.LEVEL_CHOICE;
	}

	// FIXME the result is Failed if I use the last ball to hit the last target, even if I hit it, because
	// the BallDestroyed comes first to LevelControl
	public void OnBallsFinished (object sender, EventArgs e)
	{
		Status = STATUS.LEVEL_FAILED;
	}

	public void OnTargetsFinished (object sender, EventArgs e)
	{
		System.Threading.Thread.Sleep (100);

		var score = ScoreControl.Instance.Score;
		var playerScore = Player.GetScore (level);
		if (playerScore == 0)
			Player.AddScore (level, score);
		else if (score > playerScore)
			Player.ReplaceScore (level, score);
		if (level == Player.LastLevel)
			Player.LastLevel++;
		DataControl.Instance.SavePlayer (Player);

		Status = STATUS.LEVEL_WON;
	}

	// STATUS CHANGE CODE

	private STATUS status;

	public enum STATUS
	{
		START_SCREEN,
		LEVEL_CHOICE,
		PLAYING,
		PAUSED,
		LEVEL_WON,
		LEVEL_FAILED
	}

	public STATUS Status {
		get { return status; }
		set {
			status = value;
			switch (status) {
			case STATUS.LEVEL_CHOICE:
				SceneManager.LoadScene ("LevelChoice");
				break;
			case STATUS.PLAYING:
				SceneManager.LoadScene (DataControl.Instance.GetLevelScene (level));
				break;
			case STATUS.LEVEL_WON:
				SceneManager.LoadScene ("LevelWon");
				break;
			case STATUS.LEVEL_FAILED:
				SceneManager.LoadScene ("LevelFailed");
				break;
			}
		}
	}
}
