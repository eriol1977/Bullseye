using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

	private STATUS status;

	private enum STATUS
	{
		PLAYING,
		PAUSED,
		GAMEOVER
	}

	private STATUS Status {
		get { return status; }
		set {
			status = value;
			if (value == STATUS.PLAYING) {
				Replay ();	
			} else if (value == STATUS.GAMEOVER) {
				GameOver ();
			}
		}
	}

	void Start ()
	{
		Status = STATUS.PLAYING;
	}

	void Update ()
	{
		if (Status == STATUS.GAMEOVER) {
			if (Input.GetKey (KeyCode.R)) {
				Status = STATUS.PLAYING;
			}
		}
	}

	private void GameOver ()
	{
		GetComponent <MovementController> ().enabled = false;
		GetComponent <ShootingController> ().enabled = false;
		SceneManager.LoadScene ("GameOver");
	}

	private void Replay ()
	{
		Score = 0;
		Level = 1;
		Balls = 15;
		GetComponent <MovementController> ().enabled = true;
		GetComponent <ShootingController> ().enabled = true;
		SceneManager.LoadScene ("01");
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
		if (scoreLabel != null)
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
		if (levelLabel != null)
			levelLabel.text = "Level " + level.ToString ();
	}

	// BALLS

	private int balls;

	public Text ballsLabel;

	public int Balls {
		get {
			return balls;
		}
		set {
			balls = value;
			UpdateBallsLabel ();
			if (balls == 0) {
				Status = STATUS.GAMEOVER;
			}
		}
	}

	private void UpdateBallsLabel ()
	{
		if (ballsLabel != null)
			ballsLabel.text = "Balls: " + balls.ToString ();
	}
}
