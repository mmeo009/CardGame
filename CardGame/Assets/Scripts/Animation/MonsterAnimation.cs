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
        int childs = gameObject.transform.childCount;
        List<Transform> _eyes = new List<Transform>();
        List<Transform> _items = new List<Transform>();
        for (int i = 0; i < childs; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            if (child.name == "Face")
            {
                _eyes.Add(child);
                foreach (Transform eye in child)
                {
                    eyes.Add(eye.name, eye.gameObject);
                }
            }
            else if (child.name == "Item")
            {
                _items.Add(child);
                foreach (Transform item in child)
                {
                    items.Add(item.name, item.gameObject);
                }
            }
        }

        Animator _anim = gameObject.GetComponent<Animator>();
        if (_anim != null)
        {
            anim = _anim;
        }
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
/*        if(items != null && items["Normal"] != null)
        {
            items["Sad"].SetActive(false);
            items["Mad"].SetActive(false);
            items["Normal"].SetActive(true);
        }*/
    }
}
