using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardAdd : MonoBehaviour
{

    private void Awake()
    {
        Managers.Data.GetResources();
        Managers.Data.DataIntoDictionary();
        DrawCard.Instance.TransformChack(); 
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Managers.Stage.SelectLevel();
        }
    }
}
