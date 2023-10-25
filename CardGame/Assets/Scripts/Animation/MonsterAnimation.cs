using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    public Entity_MonsteraData.Param thisMonster;
    public Dictionary<string, GameObject> eyes = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();
    public Animator anim;
    public void FindMyEyes()        // ���� �ִϸ����� ã�ƿ��� �Լ�
    {
        int childs = gameObject.transform.childCount;       // ������ �ڽ� ������Ʈ�� ���ڸ� �����ϴ� ����
        if(childs > 0 )
        {
            Dictionary<string, Transform> _childs = new Dictionary<string, Transform>();        // ������ �����۰� ���� �����ϴ� Dictionary
            for(int i = 0; i< childs; i++)
            {       // �ڽ� �� ��ŭ �ݺ��Ͽ� Dictionary�� ����
                _childs.Add(gameObject.transform.GetChild(i).name, gameObject.transform.GetChild(i).transform);     
            }
            if (_childs["Face"] != null)    // ���� ���� �� ���
            {
                foreach (Transform _face in _childs["Face"])
                {
                    eyes.Add(_face.name, _face.gameObject);      // �� ������Ʈ�� ã�ƿ� ������Ʈ�� �̸��� Key������ Dictionary�� ������
                    Debug.Log(_face.name);
                }
            }
            if(_childs["Item"] != null)     //�������� ���� �� ���
            {
                foreach (Transform _item in _childs["Item"])
                {
                    items.Add(_item.name, _item.gameObject);      // ������ ������Ʈ�� ã�ƿ� ������Ʈ�� �̸��� Key������ Dictionary�� ������
                    Debug.Log(_item.name);
                }
            }
        }

        Animator _anim = gameObject.GetComponent<Animator>();           // �ִϸ����͸� ã�ƿ�
        if(_anim != null)       // ���࿡ �ִϸ����Ͱ� �����Ѵٸ�
        {
            anim = _anim;       // �ִϸ����͸� �ҷ���
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
