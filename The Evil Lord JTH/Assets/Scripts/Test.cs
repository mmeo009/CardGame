using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        Managers.Resource.LoadAllAsync<Object>("Data", (key, count, totalCount) =>
        {
            Debug.Log("key : " + key + " Count : " + count + " totalCount : " + totalCount);
            if (count == totalCount)
            {
                Managers.Data.Init();
                Debug.Log(Managers.Data.cardsDictionary["101001A"].cardName);
            }
        });
    }

}
