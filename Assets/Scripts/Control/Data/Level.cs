using System.Xml;
using System.Xml.Serialization;

public class Level
{
	[XmlAttribute ("number")]
	public int Number;

	[XmlAttribute ("scene")]
	public string Scene;

	[XmlAttribute ("kind")]
	public string Kind;

	public int Balls;

	public int Targets;
}