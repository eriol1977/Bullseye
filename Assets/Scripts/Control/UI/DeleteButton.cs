using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class DeleteButton : MonoBehaviour
{
	private string playerName;

	public event EventHandler PlayerDeleted;

	void Awake ()
	{
		gameObject.GetComponent<Button> ().onClick.RemoveAllListeners ();
		gameObject.GetComponent<Button> ().onClick.AddListener (ShowDeleteConfirmationDialog);
	}

	public string PlayerName {
		get {
			return playerName;
		}
		set {
			playerName = value;
		}
	}

	public void ShowDeleteConfirmationDialog ()
	{
		if (PlayerDeleted != null)
			PlayerDeleted (this, null);
	}

}
