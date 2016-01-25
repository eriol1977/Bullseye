using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;

	public static GameManager Instance {
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
		}
	}

	void Start ()
	{
		Score = 0;
		Level = 1;
	}

	// SCORE

	private int score;

	public Text scoreLabel;

	public int Score {
		get {
			return score;
		}
		set {
			score = value;
			UpdateScoreLabel ();
		}
	}

	private void UpdateScoreLabel ()
	{
		scoreLabel.text = Score.ToString ();
	}

	// LEVEL

	private int level;

	public Text levelLabel;

	public int Level {
		get {
			return level;
		}
		set {
			level = value;
			UpdateLevelLabel ();
		}
	}

	private void UpdateLevelLabel ()
	{
		levelLabel.text = "Level " + level.ToString ();
	}
}
