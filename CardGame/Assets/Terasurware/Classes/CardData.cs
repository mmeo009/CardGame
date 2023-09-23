using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardData : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public string id;
		public string name;
		public string method;
		public double type;
		public string element;
		public double attack_type;
		public double cost;
		public string rarity;
		public string text;
		public string comment;
		public string cc_1;
		public string cc_2;
		public double stat_01;
		public double stat_02;
		public double stat_03;
		public double stat_04;
		public string image;
	}
}

