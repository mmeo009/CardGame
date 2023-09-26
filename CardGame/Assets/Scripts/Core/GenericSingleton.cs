using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)                   //�ν��Ͻ��� �������
            {
                _instance = FindObjectOfType<T>();  //Ŭ���� Ÿ���� ã�´�.
                if (_instance == null)
                {
                    GameObject obj = new GameObject();  //������Ʈ ����
                    obj.name = typeof(T).Name;          //�̸� ����
                    _instance = obj.AddComponent<T>();  //������Ʈ Add
                }
            }
            return _instance;
        }
    }
    private void Awake()                                   //Awake �������� �ν��Ͻ� �˻�
    {
        if (_instance == null)                              //�ν��Ͻ��� �������
        {
            _instance = this as T;                         //���� �ν��Ͻ��� Static�� �Է�
            DontDestroyOnLoad(gameObject);                 //DontDestroyOnLoad �ı����� �ʴ� ������Ʈ�� ����
        }
        else if (_instance != this)
        {
            Destroy(gameObject);                           //������ �ν��Ͻ��� �ִ� ��� �ı� ��Ų��.
        }
    }
}
