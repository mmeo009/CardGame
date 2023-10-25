using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    public Entity_MonsteraData.Param thisMonster;
    public Dictionary<string, GameObject> eyes = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();
    public Animator anim;
    public void FindMyEyes()        // 눈과 애니메이터 찾아오는 함수
    {
        int childs = gameObject.transform.childCount;       // 몬스터의 자식 오브젝트의 숫자를 저장하는 변수
        if(childs > 0 )
        {
            Dictionary<string, Transform> _childs = new Dictionary<string, Transform>();        // 몬스터의 아이템과 얼굴을 저장하는 Dictionary
            for(int i = 0; i< childs; i++)
            {       // 자식 수 만큼 반복하여 Dictionary에 저장
                _childs.Add(gameObject.transform.GetChild(i).name, gameObject.transform.GetChild(i).transform);     
            }
            if (_childs["Face"] != null)    // 얼굴이 존재 할 경우
            {
                foreach (Transform _face in _childs["Face"])
                {
                    eyes.Add(_face.name, _face.gameObject);      // 얼굴 오브젝트를 찾아와 오브젝트의 이름을 Key값으로 Dictionary에 저장함
                    Debug.Log(_face.name);
                }
            }
            if(_childs["Item"] != null)     //아이템이 존재 할 경우
            {
                foreach (Transform _item in _childs["Item"])
                {
                    items.Add(_item.name, _item.gameObject);      // 아이템 오브젝트를 찾아와 오브젝트의 이름을 Key값으로 Dictionary에 저장함
                    Debug.Log(_item.name);
                }
            }
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
            if(items != null) 
            {
                items["Normal"].SetActive(false);
                items["Sad"].SetActive(true);
            }
            Invoke("SetDefaultEye", 1.0f);
        }
        else
        {
            eyes["Normal"].SetActive(false);
            eyes["Sad"].SetActive(true);
            if (items != null)
            {
                items["Normal"].SetActive(false);
                items["Sad"].SetActive(true);
            }
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
        if(items != null)
        {
            items["Sad"].SetActive(false);
            items["Mad"].SetActive(false);
            items["Normal"].SetActive(true);
        }
    }
}
