using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager
{
    public enum Stage
    {
        MAIN,
        BATTLE,
        STORE,
        DEMON,
        TARVERN,
        FORGE,
        MIDDLEBOSS,
        BOSS
    }
    public Stage stage = Stage.MAIN;
    public int stageNum;
    public int battleStageCount;
    public int stronger;
    public void SelectLevel()
    {
        if(stageNum == 0)
        {
            stage = Stage.BATTLE;
            battleStageCount++;
        }
        else if((stageNum < 5 && battleStageCount > 3)||(stageNum != 5 && stageNum < 11 && battleStageCount > 6))
        {
            int st = UnityEngine.Random.Range(0, 4);
            switch (st)
            {
                case 0:
                    stage = Stage.STORE;
                    break;
                case 1:
                    stage = Stage.FORGE;
                    break;
                case 2:
                    stage = Stage.TARVERN;
                    break;
                case 3:
                    stage = Stage.DEMON;
                    break;
            }
        }
        else if((stageNum < 5 && battleStageCount < 3) || (stageNum != 5 && stageNum < 11 && battleStageCount < 6))
        {
            int st = UnityEngine.Random.Range(0, 5);
            switch (st)
            {
                case 0:
                    stage = Stage.STORE;
                    break;
                case 1:
                    stage = Stage.FORGE;
                    break;
                case 2:
                    stage = Stage.TARVERN;
                    break;
                case 3:
                    stage = Stage.DEMON;
                    break;
                case 4:
                    stage = Stage.BATTLE;
                    battleStageCount++;
                    break;
            }
        }
        else if((stageNum < 5 && battleStageCount < 2) || (stageNum != 5 && stageNum < 11 && battleStageCount < 4))
        {
            stage = Stage.BATTLE;
            battleStageCount++;
        }
        else if(stageNum == 5)
        {
            stage = Stage.MIDDLEBOSS;
        }
        else if(stageNum == 11)
        {
            stage = Stage.BOSS;
            stageNum = -1;
            battleStageCount = 0;
            stronger++;
        }
        stageNum++;
        Debug.Log(stage);
        Debug.Log(stageNum);
    }
}
