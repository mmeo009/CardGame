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
        audioSource.Play(); // 배경음악 재생
    }

    void Update()
    {
        // 배경음악 제어 로직 추가
    }
}
