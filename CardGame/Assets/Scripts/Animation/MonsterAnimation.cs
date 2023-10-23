using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    public Entity_MonsteraData.Param thisMonster;
    public Dictionary<string, GameObject> eyes = new Dictionary<string, GameObject>();
    public void FindMyEyes()
    {
        Transform face = gameObject.transform.GetChild(0).transform;
        foreach(Transform _face in face)
        {
            eyes.Add(_face.name,_face.gameObject);
            Debug.Log(_face.name);
        }
    }

    public void GetDamaged()
    {
        if (eyes == null)
        {
            FindMyEyes();
            eyes["Normal"].SetActive(false);
            eyes["Sad"].SetActive(true);
            Invoke("SetDefaultEye", 1.0f);
        }
        else
        {
            eyes["Normal"].SetActive(false);
            eyes["Sad"].SetActive(true);
            Invoke("SetDefaultEye", 1.0f);
        }
    }

    public void BeneficialEffect()
    {

    }

    public void SetDefaultEye()
    {
        if (eyes == null)
        {
            FindMyEyes();
            eyes["CC"].SetActive(false);
            eyes["Sad"].SetActive(false);
            eyes["Mad"].SetActive(false);
            eyes["Normal"].SetActive(true);
        }
        else
        {
            eyes["CC"].SetActive(false);
            eyes["Sad"].SetActive(false);
            eyes["Mad"].SetActive(false);
            eyes["Normal"].SetActive(true);
        }
    }
}
