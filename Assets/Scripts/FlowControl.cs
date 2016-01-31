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
			lc = GameObject.Find ("TransientGameControl").GetComponent<LevelControl> ();
			lc.BallsFinished += OnLevelFailed;

			sc = GameObject.Find ("TransientGameControl").GetComponent<ScoreControl> ();

			uic = GameObject.Find ("TransientGameControl").GetComponent<UIControl> ();
			uic.Init (sc, lc);

			lc.Level = level;
			lc.Balls = 15; // TODO lettura da XML
			sc.Score = 0;
		}
	}

	public void OnStartGame ()
	{
		// TODO dovrebbe mostrare la schermata di scelta livello
		Status = STATUS.PLAYING;
	}

	private void OnLevelFailed (object sender, EventArgs e)
	{
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
