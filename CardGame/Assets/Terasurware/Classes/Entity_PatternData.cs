using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_PatternData : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public string patternId;
		public string monsterId;
		public int type;
		public int ad;
		public int ap;
		public int heal;
		public string cc_1;
		public string cc_2;
		public int stat_001;
		public int stat_002;
		public int stat_003;
		public int stat_004;
		public int stat_005;
		public int stat_006;
	}
}