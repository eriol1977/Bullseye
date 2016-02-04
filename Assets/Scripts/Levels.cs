using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot ("Game")]
public class Levels
{
	[XmlArray ("Levels")]
	[XmlArrayItem ("Level")]
	public List<Level> levels = new List<Level> ();

	public static Levels Load (string path)
	{
		var serializer = new XmlSerializer (typeof(Levels));
		using (var stream = new FileStream (path, FileMode.Open)) {
			return serializer.Deserialize (stream) as Levels;
		}
	}

	public static Dictionary<int,Level> LoadDictionary (string path)
	{
		Levels levs = Load (path);
		Dictionary<int,Level> dic = new Dictionary<int, Level> (levs.levels.Capacity);
		foreach (Level l in levs.levels) {
			dic.Add (l.Number, l);
		}
		return dic;
	}
}