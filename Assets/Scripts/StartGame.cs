using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartGame : MonoBehaviour
{
	private GameObject modalPanel;

	void Start ()
	{
		modalPanel = GameObject.Find ("ModalPanel");
		modalPanel.SetActive (false);

		if (FlowControl.Instance.Status == FlowControl.STATUS.START_SCREEN) {
			LoadPlayerButtons ();
		}

		// disables Continue button if this is the last existing level
		if (FlowControl.Instance.Level == DataControl.Instance.GetLastLevelNumber ()) {
			GameObject btn = GameObject.Find ("ContinueButton");
			if (btn)
				btn.GetComponent<Button> ().interactable = false;
		}
	}

	void LoadPlayerButtons ()
	{
		List<string> playerNames = DataControl.Instance.GetPlayerNames ();
		string buttonName;
		string deleteButtonName;
		GameObject button;
		GameObject deleteButton;
		for (int i = 0; i < 3; i++) {
			buttonName = "Player" + (i + 1) + "Button";
			deleteButtonName = "Delete" + (i + 1) + "Button";
			button = GameObject.Find (buttonName);
			deleteButton = GameObject.Find (deleteButtonName);
			if (i < playerNames.Count) {
				button.GetComponentInChildren<Text> ().text = playerNames [i];
				button.GetComponent<Button> ().onClick.AddListener (delegate () {
					Go (buttonName); // FIXME
				});
				deleteButton.GetComponent<Button> ().onClick.AddListener (delegate () {
					ShowDeleteConfirmationDialog (buttonName); // FIXME
				});
			} else {
				button.GetComponentInChildren<Text> ().text = "[New]";
				button.GetComponent<Button> ().onClick.AddListener (delegate () {
					ShowNewPlayerDialog ();
				});
				deleteButton.SetActive (false);
			}
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

	public void ShowNewPlayerDialog ()
	{
		modalPanel.SetActive (true);
		modalPanel.GetComponentInChildren<InputField> ().text = "";
	}

	public void ShowDeleteConfirmationDialog ()
	{
		// TODO
	}

	public void ConfirmNewPlayer ()
	{
		string playerName = modalPanel.GetComponentInChildren<InputField> ().text;
		DataControl.Instance.CreatePlayer (playerName);
		FlowControl.Instance.Player = playerName;
		LoadPlayerButtons ();
		modalPanel.SetActive (false);
	}

	public void CancelNewPlayer ()
	{
		modalPanel.SetActive (false);
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
