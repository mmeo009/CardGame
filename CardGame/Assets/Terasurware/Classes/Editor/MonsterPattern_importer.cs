using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class MonsterPattern_importer : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Excel/MonsterPattern.xlsx";
    private static readonly string[] sheetNames = { "PatternData", };
    
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
                    var exportPath = "Assets/Resources/" + sheetName + ".asset";
                    
                    // check scriptable object
                    var data = (Entity_PatternData)AssetDatabase.LoadAssetAtPath(exportPath, typeof(Entity_PatternData));
                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<Entity_PatternData>();
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
                        
                        var p = new Entity_PatternData.Param();
			
					cell = row.GetCell(0); p.patternId = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.monsterId = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.type = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.ad = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.ap = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.heal = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.cc_1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(7); p.cc_2 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(8); p.stat_001 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.stat_002 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.stat_003 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(11); p.stat_004 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(12); p.stat_005 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(13); p.stat_006 = (int)(cell == null ? 0 : cell.NumericCellValue);

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
