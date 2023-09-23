using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class Card_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Excel/Card.xlsx";
	private static readonly string exportPath = "Assets/Excel/Card.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			CardData data = (CardData)AssetDatabase.LoadAssetAtPath (exportPath, typeof(CardData));
			if (data == null) {
				data = ScriptableObject.CreateInstance<CardData> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					CardData.Sheet s = new CardData.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						CardData.Param p = new CardData.Param ();
						
					cell = row.GetCell(0); p.id = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.method = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.type = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.element = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(5); p.attack_type = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.cost = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.rarity = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(8); p.text = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(9); p.comment = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(10); p.cc_1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(11); p.cc_2 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(12); p.stat_01 = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(13); p.stat_02 = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(14); p.stat_03 = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(15); p.stat_04 = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(16); p.image = (cell == null ? "" : cell.StringCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
