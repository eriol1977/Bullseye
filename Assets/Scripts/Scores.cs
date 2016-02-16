using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Scores
{
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