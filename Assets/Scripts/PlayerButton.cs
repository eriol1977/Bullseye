using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerButton : MonoBehaviour
{
	private string playerName;

	public event EventHandler PlayerSelected;

	void Awake ()
	{
		gameObject.GetComponent<Button> ().onClick.RemoveAllListeners ();
		gameObject.GetComponent<Button> ().onClick.AddListener (Play);
	}

	public string PlayerName {
		get {
			return playerName;
		}
		set {
			playerName = value;
		}
	}

	public void Play ()
	{
		if (PlayerSelected != null)
			PlayerSelected (this, null);
	}

}
