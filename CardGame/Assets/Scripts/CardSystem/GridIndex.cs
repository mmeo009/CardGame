using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridIndex : MonoBehaviour
{
    public int GridNum;
    public bool isEmpty = true;
    public GameObject myCard;

    public void ISEmpty()
    {
       int count = this.gameObject.transform.childCount;
        if(count < 1)
        {
            Debug.Log($"{GridNum}번 그리드 비었음");
            myCard = null;
            isEmpty = true;
        }
        if(count == 1)
        {
            Debug.Log($"{GridNum}번 그리드 차있음");
            myCard = this.gameObject.transform.GetChild(0).gameObject;
            isEmpty = false;
        }
    }
}
