using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    public enum Type
    {
        HIT,
        HEAL,
        BREAK,
        GAIN
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(Type type, bool breakAfterHit = false)
    {
        if(type == Type.HEAL)
        {
            anim.SetTrigger("Heal");
        }
        else if(type == Type.HIT)
        {
            anim.SetTrigger("Hit");
        }
        else if(type == Type.GAIN)
        {
            anim.SetTrigger("Gain");
            if (breakAfterHit == true)
            {
                anim.SetBool("BreakAfterHit", true);
                Invoke("MakeFalse", 0.5f);
            }
        }
        else if(type == Type.BREAK)
        {
            anim.SetTrigger("Break");
            if(breakAfterHit == true)
            {
                anim.SetBool("BreakAfterHit", true);
                Invoke("MakeFalse", 0.5f);
            }
        }
    }
    public void MakeFalse()
    {
        anim.SetBool("BreakAfterHit", false);
    }
}
