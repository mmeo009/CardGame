using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class CardData_importer : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Excel/CardData.xlsx";
    private static readonly string[] sheetNames = { "CardData", };
    
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets)
        {
            if (!filePath.Equals(asset))
                continue;

            using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}

                foreach (string sheetName in sheetNames)
                {
                    var exportPath = "Assets/Excel/" + sheetName + ".asset";
                    
                    // check scriptable object
                    var data = (Entity_CardData)AssetDatabase.LoadAssetAtPath(exportPath, typeof(Entity_CardData));
                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<Entity_CardData>();
                        AssetDatabase.CreateAsset((ScriptableObject)data, exportPath);
                        data.hideFlags = HideFlags.NotEditable;
                    }
                    data.param.Clear();

					// check sheet
                    var sheet = book.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        Debug.LogError("[QuestData] sheet not found:" + sheetName);
                        continue;
                    }

                	// add infomation
                    for (int i=1; i<= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        ICell cell = null;
                        
                        var p = new Entity_CardData.Param();
			
					cell = row.GetCell(0); p.id = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.itemCode = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.cardName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.cardType = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.usingMethod = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.cardCost = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.element = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(7); p.rarity = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.text = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(9); p.comment = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(10); p.adPower = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(11); p.apPower = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(12); p.fixedPower = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(13); p.cc_1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(14); p.cc_2 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(15); p.stat_01 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(16); p.stat_02 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(17); p.stat_03 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(18); p.stat_04 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(19); p.stat_05 = (int)(cell == null ? 0 : cell.NumericCellValue);

                        data.param.Add(p);
                    }
                    
                    // save scriptable object
                    ScriptableObject obj = AssetDatabase.LoadAssetAtPath(exportPath, typeof(ScriptableObject)) as ScriptableObject;
                    EditorUtility.SetDirty(obj);
                }
            }

        }
    }
}
