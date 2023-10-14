using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_MonsteraData : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public string id;
		public string monsterName;
		public int baseHp;
		public int baseAd;
		public int baseAp;
		public int pattern;
		public int statPlus;
	}
}