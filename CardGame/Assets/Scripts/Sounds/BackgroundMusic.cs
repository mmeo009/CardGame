using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetOrAddComponent<AudioSource>(this.gameObject);
        audioSource.Play(); // ������� ���
    }

    void Update()
    {
        // ������� ���� ���� �߰�
    }
}
