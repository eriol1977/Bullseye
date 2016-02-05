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
}