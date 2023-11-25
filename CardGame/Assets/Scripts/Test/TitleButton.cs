using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public void Press(string sceneName)
    {
        Managers.Game.MoveScene(sceneName);
    }
}
