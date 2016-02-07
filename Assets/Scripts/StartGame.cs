using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGame : MonoBehaviour
{
	void Start ()
	{
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
