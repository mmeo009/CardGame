using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    public Entity_MonsteraData.Param thisMonster;
    public Dictionary<string, GameObject> eyes = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();
    public Animator anim;
    public void FindMyEyes()
    {
        List<Transform> _items = new List<Transform>();
        foreach(Transform face in transform)
        {
            if(face.name == "Buff" || face.name == "Surprised" || face.name == "Sad" || face.name == "Normal")
            {
                eyes.Add(face.name, face.gameObject);
            }
        }    
        Animator _anim = gameObject.GetComponent<Animator>();
        if (_anim != null)
        {
            anim = _anim;
        }
        SetDefaultEye();
    }

    public void GetDamaged()
    {
        if (eyes == null)
        {
            if(thisMonster.id != "501102A")
            {
                FindMyEyes();
                eyes["Normal"].SetActive(false);
                eyes["Sad"].SetActive(true);
/*                if (items != null)
                {
                    items["Normal"].SetActive(false);
                    items["Sad"].SetActive(true);
                }*/
                Invoke("SetDefaultEye", 1.0f);
            }
        }
        else
        {

            eyes["Normal"].SetActive(false);
            eyes["Sad"].SetActive(true);
/*            if (items != null)
            {
                items["Normal"].SetActive(false);
                items["Sad"].SetActive(true);
            }*/
            Invoke("SetDefaultEye", 1.0f);
        }
        if(anim != null)
        {
            anim.SetBool("Sad", true);
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
            if(eyes["Surprised"] != null)
            {
                eyes["Surprised"].SetActive(false);
            }
            if (eyes["Sad"] != null)
            {
                eyes["Sad"].SetActive(false);
            }
            if (eyes["Buff"] != null)
            {
                eyes["Buff"].SetActive(false);
            }
            if (eyes["Normal"] != null)
            {
                eyes["Normal"].SetActive(true);
            }
        }
        else
        {
            if (eyes["Surprised"] != null)
            {
                eyes["Surprised"].SetActive(false);
            }
            if (eyes["Sad"] != null)
            {
                eyes["Sad"].SetActive(false);
            }
            if (eyes["Buff"] != null)
            {
                eyes["Buff"].SetActive(false);
            }
            if (eyes["Normal"] != null)
            {
                eyes["Normal"].SetActive(true);
            }
        }
/*        if(items != null && items["Normal"] != null)
        {
            items["Sad"].SetActive(false);
            items["Mad"].SetActive(false);
            items["Normal"].SetActive(true);
        }*/
    }
}
