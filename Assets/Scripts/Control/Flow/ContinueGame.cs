using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContinueGame : MonoBehaviour
{
	void Start ()
	{
		// disables Continue button if this is the last existing level
		if (FlowControl.Instance.Level == DataControl.Instance.GetLastLevelNumber ()) {
			GameObject btn = GameObject.Find ("ContinueButton");
			if (btn)
				btn.GetComponent<Button> ().interactable = false;
		}
	}

	public void Reload ()
	{
		FlowControl.Instance.ReloadLevel ();
	}

	public void LoadNextLevel ()
	{
		FlowControl.Instance.LoadNextLevel ();
	}

	public void ChooseLevel ()
	{
		FlowControl.Instance.ChooseLevel ();
	}

	public void Quit ()
	{
		Application.Quit ();
	}
}
