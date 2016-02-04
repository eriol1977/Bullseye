using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class DataControl : MonoBehaviour
{
	private Dictionary<int,Level> levels;

	public Level GetLevel (int number)
	{
		return levels [number];
	}

	public string GetLevelScene (int number)
	{
		return levels [number].Scene;
	}

	// SINGLETON CODE

	private static DataControl instance = null;

	public static DataControl Instance {
		get {
			return instance;
		}
	}

	void Awake ()
	{
		//Make this active and only instance (since it's already part of persistent game object, we don't have to repeat
		//all the DontDestroyOnLoad part present in FlowControl)
		instance = this;
		levels = Levels.LoadDictionary (Path.Combine (Application.dataPath, "Levels/levels.xml"));
	}
}
