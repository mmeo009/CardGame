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
        BOSS
    }
    public Stage stage = Stage.MAIN;
    public int stageNum;
    public int battleStageCount;
    public int strongDegree = 1;
    public int battleStageType = 1;
    public int level = 2;
    public void SelectLevel()
    {
        if(stageNum == 0)
        {
            //battleStageType = UnityEngine.Random.Range(1, 4);
            stage = Stage.BATTLE;
            battleStageCount++;
            if (level == 1)
            {
                level = 2;
            }
            else
            {
                level = 1;
            }
        }
        else if(stageNum < 5 && battleStageCount > 2)
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
        else if(stageNum < 3 && battleStageCount <= 2)
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
        else if (stageNum < 5 && battleStageCount < 3)
        {
            stage = Stage.BATTLE;
            battleStageCount++;
        }
        else if(stageNum == 5)
        {
            stage = Stage.BOSS;
            stageNum = -1;
            battleStageCount = 0;
            strongDegree++;
        }
        stageNum++;
        Debug.Log("Stage : " + stage
                  + " StageNum : " + stageNum
                  + " battleStageType : " + battleStageType
                  + " battleStageCount : " + battleStageCount
                  + " Level : " + level);

        if(stage == Stage.BOSS || stage == Stage.BATTLE)
        {
            GameManager.Instance.MoveScene("Battle Scene");
        }
    }
    public void BattleStage()
    {
        if(stage == Stage.BOSS)
        {
            MonsterData.Instance.LoadMonsterData(level, battleStageType, strongDegree, true);
        }
        else
        {
            MonsterData.Instance.LoadMonsterData(level, battleStageType, strongDegree);
        }
    }
}
