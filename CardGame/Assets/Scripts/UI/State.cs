using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    public string name;
    public Image image;
    public void LoadImage(string ccName)
    {
        string nameUpper = ccName.ToUpper();
        if (image == null)
        {
            image = gameObject.GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>($"Illustration/CC/{nameUpper}");
        }
        else
        {
            image.sprite = Resources.Load<Sprite>($"Illustration/CC/{nameUpper}");
        }
        name = ccName;
    }
}
