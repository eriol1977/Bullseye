using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot ("SaveGames")]
public class Players
{
	[XmlArray ("Players")]
	[XmlArrayItem ("Player")]
	public List<Player> players = new List<Player> ();

	public static Players Load (string path)
	{
		var serializer = new XmlSerializer (typeof(Players));
		using (var stream = new FileStream (path, FileMode.Open)) {
			return serializer.Deserialize (stream) as Players;
		}
	}

	public static Dictionary<string,Player> LoadDictionary (string path)
	{
		Players pls = Load (path);
		Dictionary<string,Player> dic = new Dictionary<string,Player> (pls.players.Capacity);
		foreach (Player p in pls.players) {
			dic.Add (p.Name, p);
		}
		return dic;
	}
}