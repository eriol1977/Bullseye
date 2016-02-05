using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{
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
}
