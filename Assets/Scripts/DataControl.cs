using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class DataControl : MonoBehaviour
{
	private readonly string LEVELS_FILENAME = "Levels/levels.xml";

	private readonly string SAVEGAMES_FILENAME = "Levels/save_games.xml";

	private Dictionary<int,Level> levels;

	private Dictionary<string,Player> players;

	public Level GetLevel (int number)
	{
		return levels [number];
	}

	public string GetLevelScene (int number)
	{
		return levels [number].Scene;
	}

	public int GetLastLevelNumber ()
	{
		return levels.Count;
	}

	public Player GetPlayer (string name)
	{
		return players [name];
	}

	public List<string> GetPlayerNames ()
	{
		return new List<string> (players.Keys);
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
		levels = Levels.LoadDictionary (Path.Combine (Application.dataPath, LEVELS_FILENAME));
		players = Players.LoadDictionary (Path.Combine (Application.dataPath, SAVEGAMES_FILENAME));
	}
}
