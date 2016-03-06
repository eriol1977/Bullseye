using System.Xml;
using System.Xml.Serialization;

public class Score
{
	[XmlAttribute ("level")]
	public int Level;

	[XmlAttribute ("value")]
	public int Value;
}