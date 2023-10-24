using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    public Entity_MonsteraData.Param thisMonster;
    public Dictionary<string, GameObject> eyes = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> item = new Dictionary<string, GameObject>();
    public Animator anim;
    public void FindMyEyes()        // ���� �ִϸ����� ã�ƿ��� �Լ�
    {
        Transform face = gameObject.transform.GetChild(0).transform;        // �� ������Ʈ�� ã�ƿ�
        foreach(Transform _face in face)
        {
            eyes.Add(_face.name,_face.gameObject);      // �� ������Ʈ�� ã�ƿ� ������Ʈ�� �̸��� Key������ Dictionary�� ������
            Debug.Log(_face.name);
        }
        Transform item = gameObject.transform.GetChild(1).transform;        // ������ ������Ʈ�� ã�ƿ�
        foreach (Transform _item in item)
        {
            eyes.Add(_item.name, _item.gameObject);      // ������ ������Ʈ�� ã�ƿ� ������Ʈ�� �̸��� Key������ Dictionary�� ������
            Debug.Log(_item.name);
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
