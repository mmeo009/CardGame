using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button: MonoBehaviour
{
    public void Press(string sceneName)
    {
        if(sceneName == "end")
        {
            GameManager.Instance.EndClick();
        }
        else
        {
            GameManager.Instance.MoveScene(sceneName);
        }
    }
    public void EndTurn()
    {
        TurnManager.Instance.PlayerTurnEnd();
    }

}
