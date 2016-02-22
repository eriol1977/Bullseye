using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class StartGame : MonoBehaviour
{
	private GameObject modalPanel;
	private GameObject newPlayerDialog;
	private GameObject deletePlayerDialog;
	private List<GameObject> playerButtons = new List<GameObject> (3);
	private List<GameObject> deleteButtons = new List<GameObject> (3);

	void Start ()
	{
		StoreUIElements ();

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

	void StoreUIElements ()
	{
		modalPanel = GameObject.Find ("ModalPanel");
		newPlayerDialog = GameObject.Find ("NewPlayerDialog");
		deletePlayerDialog = GameObject.Find ("DeletePlayerDialog");
		newPlayerDialog.SetActive (false);
		deletePlayerDialog.SetActive (false);
		modalPanel.SetActive (false);

		string buttonName;
		string deleteButtonName;
		GameObject delButton;
		for (int i = 1; i <= 3; i++) {
			buttonName = "Player" + i + "Button";
			deleteButtonName = "Delete" + i + "Button";
			playerButtons.Add (GameObject.Find (buttonName));
			delButton = GameObject.Find (deleteButtonName);
			delButton.GetComponent<DeleteButton> ().PlayerDeleted += HandlePlayerDeleted;
			deleteButtons.Add (delButton);
		}
	}

	void LoadPlayerButtons ()
	{
		List<string> playerNames = DataControl.Instance.GetPlayerNames ();
		GameObject button;
		GameObject deleteButton;
		string playerName;
		for (int i = 0; i < 3; i++) {
			button = playerButtons [i];
			deleteButton = deleteButtons [i];
			if (i < playerNames.Count) {
				playerName = playerNames [i];
				button.GetComponentInChildren<Text> ().text = playerName;
				button.GetComponent<Button> ().onClick.RemoveAllListeners ();
				button.GetComponent<Button> ().onClick.AddListener (delegate () {
					Go (playerName);
				});
				deleteButton.SetActive (true);
				deleteButton.GetComponent<DeleteButton> ().PlayerName = playerName;
			} else {
				button.GetComponentInChildren<Text> ().text = "[New]";
				button.GetComponent<Button> ().onClick.RemoveAllListeners ();
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
		newPlayerDialog.SetActive (true);
		newPlayerDialog.GetComponentInChildren<InputField> ().text = "";
	}

	public void HandlePlayerDeleted (object sender, EventArgs e)
	{
		string playerName = ((DeleteButton)sender).PlayerName;
		ShowDeleteConfirmationDialog (playerName);
	}

	public void ShowDeleteConfirmationDialog (string playerName)
	{
		modalPanel.SetActive (true);
		deletePlayerDialog.SetActive (true);
		deletePlayerDialog.transform.Find ("Confirm").gameObject.GetComponent<Button> ().onClick.AddListener (delegate {
			ConfirmDeletePlayer (playerName);		
		});
	}

	public void ConfirmNewPlayer ()
	{
		string playerName = modalPanel.GetComponentInChildren<InputField> ().text.Trim ();
		if (!playerName.Equals ("")) {
			DataControl.Instance.CreatePlayer (playerName);
			FlowControl.Instance.Player = playerName;
			LoadPlayerButtons ();
			newPlayerDialog.SetActive (false);
			modalPanel.SetActive (false);
		}
	}

	public void CancelNewPlayer ()
	{
		newPlayerDialog.SetActive (false);
		modalPanel.SetActive (false);
	}

	public void ConfirmDeletePlayer (string playerName)
	{
		DataControl.Instance.DeletePlayer (playerName);
		FlowControl.Instance.Player = "";
		LoadPlayerButtons ();
		deletePlayerDialog.SetActive (false);
		modalPanel.SetActive (false);
	}

	public void CancelDeletePlayer ()
	{
		deletePlayerDialog.SetActive (false);
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
