using System.Xml;
using System.Xml.Serialization;

public class Player
{
	[XmlAttribute ("name")]
	public string Name;

	[XmlAttribute ("lastLevel")]
	public int LastLevel;

	public Scores scores;

	public int GetScore (int level)
	{
		return scores.GetScore (level);
	}
}