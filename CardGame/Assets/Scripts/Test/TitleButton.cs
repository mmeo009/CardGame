using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
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

}
