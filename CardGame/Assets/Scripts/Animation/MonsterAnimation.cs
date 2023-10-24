using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    public Entity_MonsteraData.Param thisMonster;
    public Dictionary<string, GameObject> eyes = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> item = new Dictionary<string, GameObject>();
    public Animator anim;
    public void FindMyEyes()        // 눈과 애니메이터 찾아오는 함수
    {
        Transform face = gameObject.transform.GetChild(0).transform;        // 얼굴 오브젝트를 찾아옴
        foreach(Transform _face in face)
        {
            eyes.Add(_face.name,_face.gameObject);      // 얼굴 오브젝트를 찾아와 오브젝트의 이름을 Key값으로 Dictionary에 저장함
            Debug.Log(_face.name);
        }
        Transform item = gameObject.transform.GetChild(1).transform;        // 아이템 오브젝트를 찾아옴
        foreach (Transform _item in item)
        {
            eyes.Add(_item.name, _item.gameObject);      // 아이템 오브젝트를 찾아와 오브젝트의 이름을 Key값으로 Dictionary에 저장함
            Debug.Log(_item.name);
        }

        Animator _anim = gameObject.GetComponent<Animator>();           // 애니메이터를 찾아옴
        if(_anim != null)       // 만약에 애니메이터가 존재한다면
        {
            anim = _anim;       // 애니메이터를 불러옴
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
    }
}
