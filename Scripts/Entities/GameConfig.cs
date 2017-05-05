using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Omni.Entities
{
	[Serializable]
	public class GameConfig : GenericObjectUser
	{
		public GameConfig()
		{
		}

		public List<AssetList> AssetList { get; set; }
	}
}