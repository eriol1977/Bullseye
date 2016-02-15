using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartGame : MonoBehaviour
{
	void Start ()
	{
		if (FlowControl.Instance.Status == FlowControl.STATUS.START_SCREEN) {
			List<string> playerNames = DataControl.Instance.GetPlayerNames ();

			string buttonName;
			GameObject button;
			for (int i = 0; i < 3; i++) {
				buttonName = "Player" + (i + 1) + "Button";
				button = GameObject.Find (buttonName);
				if (i < playerNames.Count) {
					button.GetComponentInChildren<Text> ().text = playerNames [i];
					button.GetComponent<Button> ().onClick.AddListener (delegate() {
						Go (buttonName);
					});
				} else {
					button.GetComponentInChildren<Text> ().text = "[New]";
					button.GetComponent<Button> ().onClick.AddListener (delegate() {
						NewPlayer ();
					});
				}
			}
		}

		// disables Continue button if this is the last existing level
		if (FlowControl.Instance.Level == DataControl.Instance.GetLastLevelNumber ()) {
			GameObject btn = GameObject.Find ("ContinueButton");
			if (btn)
				btn.GetComponent<Button> ().interactable = false;
		}
	}

	public void Go ()
	{
		FlowControl.Instance.OnStartGame ();
	}

	public void Go (string playerName)
	{
		FlowControl.Instance.Player = playerName;
		FlowControl.Instance.OnStartGame ();
	}

	public void NewPlayer ()
	{
		// TODO modal dialog
		Debug.Log ("New player!");
	}

	public void Reload ()
	{
		FlowControl.Instance.ReloadLevel ();
	}

	public void LoadNextLevel ()
	{
		FlowControl.Instance.LoadNextLevel ();
	}

	public void Quit ()
	{
		Application.Quit ();
	}
}
