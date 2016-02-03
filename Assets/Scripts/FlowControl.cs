﻿using UnityEngine;
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

	public void Proceed ()
	{
		if (Status == STATUS.PLAYING) {
			EventControl.Instance.InitEvents ();

			lc = LevelControl.Instance;
			lc.Level = level;

			// TODO lettura da XML
			if (level == 1) {
				lc.Balls = 15; 
				lc.Targets = 9;
			} else if (level == 2) {
				lc.Balls = 12; 
				lc.Targets = 9;
			}

			sc = ScoreControl.Instance;
			sc.Score = 0;
		}
	}

	public void LoadLevel (int level)
	{
		this.level = level;
		Status = STATUS.PLAYING;
	}

	public void OnStartGame ()
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
		Status = STATUS.LEVEL_WON;
	}

	// STATUS CHANGE CODE

	private STATUS status;

	private enum STATUS
	{
		START_SCREEN,
		LEVEL_CHOICE,
		PLAYING,
		PAUSED,
		LEVEL_WON,
		LEVEL_FAILED
	}

	private STATUS Status {
		get { return status; }
		set {
			status = value;
			switch (status) {
			case STATUS.LEVEL_CHOICE:
				SceneManager.LoadScene ("LevelChoice");
				break;
			case STATUS.PLAYING:
				// TODO lettura da xml in base a level
				if (level == 1)
					SceneManager.LoadScene ("01");
				else
					SceneManager.LoadScene ("01");
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
