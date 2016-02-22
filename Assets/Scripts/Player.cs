using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Player
{
	[XmlAttribute ("name")]
	public string Name;

	[XmlAttribute ("lastLevel")]
	public int LastLevel;

	[XmlArray ("Scores")]
	[XmlArrayItem ("Score")]
	public List<Score> scores = new List<Score> ();

	public int GetScore (int level)
	{
		foreach (Score s in scores)
			if (s.Level == level)
				return s.Value;
		return 0;
	}

	public void AddScore (int level, int value)
	{
		Score s = new Score ();
		s.Level = level;
		s.Value = value;
		scores.Add (s);
	}
}