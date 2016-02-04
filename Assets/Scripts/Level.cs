using System.Xml;
using System.Xml.Serialization;

public class Level
{
	[XmlAttribute ("number")]
	public int Number;

	[XmlAttribute ("scene")]
	public string Scene;

	public int Balls;

	public int Targets;
}