using UnityEngine;
using System.Collections;

public class LevelButtonBehavior : MonoBehaviour
{
	public int level;

	public void LoadLevel ()
	{
		FlowControl.Instance.LoadLevel (level);
	}
}
