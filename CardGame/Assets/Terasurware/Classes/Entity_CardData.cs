using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_CardData : ScriptableObject
{

	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public string id;
		public string itemCode;
		public string cardName;
		public int cardType;
		public int usingMethod;
		public int cardCost;
		public string element;
		public int rarity;
		public string text;
		public string comment;
		public int adPower;
		public int apPower;
		public int fixedPower;
		public string cc_1;
		public string cc_2;
		public int stat_01;
		public int stat_02;
		public int stat_03;
		public int stat_04;
		public int stat_05;
	}
}