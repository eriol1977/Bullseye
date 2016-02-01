using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class FlowControl : MonoBehaviour
{
	private int level;

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

	private UIControl uic;

	public void Proceed ()
	{
		if (Status == STATUS.PLAYING) {
			UIControl.Instance.Init ();

			LevelControl lc = LevelControl.Instance;
			lc.BallsFinished += OnBallsFinished;
			lc.TargetsFinished += OnTargetsFinished;
			lc.Level = level;

			// TODO lettura da XML
			lc.Balls = 15; 
			lc.Targets = 9;

			ScoreControl.Instance.Score = 0;
		}
	}

	public void OnStartGame ()
	{
		// TODO dovrebbe mostrare la schermata di scelta livello
		Status = STATUS.PLAYING;
	}

	private void OnBallsFinished (object sender, EventArgs e)
	{
		// FIXME load level failed screen
		Status = STATUS.GAMEOVER;
	}

	private void OnTargetsFinished (object sender, EventArgs e)
	{
		// FIXME load level won screen
		Status = STATUS.GAMEOVER;
	}

	// STATUS CHANGE CODE

	private STATUS status;

	private enum STATUS
	{
		START_SCREEN,
		LEVEL_CHOICE,
		PLAYING,
		PAUSED,
		LEVEL_RESULT,
		GAMEOVER
	}

	private STATUS Status {
		get { return status; }
		set {
			status = value;
			switch (status) {
			case STATUS.PLAYING:
				level++;
				// TODO lettura da xml in base a level
				SceneManager.LoadScene ("01");
				break;
			case STATUS.GAMEOVER:
				level = 0;
				SceneManager.LoadScene ("GameOver");
				break;
			}
		}
	}
}
