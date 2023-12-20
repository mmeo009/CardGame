using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class State : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string name;
    public string text;
    public Image image;
    public PlayerData player;
    public void LoadImage(string ccName)
    {
        player = PlayerData.Instance;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        player.stateInfo.SetActive(true);
        TMP_Text stateText = GameObject.Find("StateText").GetComponent<TMP_Text>();

        if (stateText != null)
        {
            if (name == "poison")
            {
                Player.CC poisonCC = player.player.playerCc.Find(cc => cc.ccName == "poison");
                if (poisonCC != null)
                {
                    stateText.text = $"매턴이 시작할 때 {poisonCC.damage}만큼 데미지를 입습니다.";
                }
            }
            else if (name == "temporary")
            {
                Player.CC temporaryCC = player.player.playerCc.Find(cc => cc.ccName == "temporary");
                if (temporaryCC != null)
                {
                    stateText.text = $"매턴이 시작할 때 {temporaryCC.damage}만큼 데미지를 입습니다.";
                }
            }
            else if (name == "blind")
            {
                stateText.text = "플레이어의 명중률이 50% 감소합니다.";
            }
            else if (name == "fury")
            {
                stateText.text = "플레이어의 공격력이 3 오르고 명중률이 75% 감소합니다.";
            }
            else if (name == "sloth")
            {
                stateText.text = "공격 카드를 사용하면 공격이 빗나갑니다.";
            }
        }
        else
        {
            Debug.LogError("TMP_Text가 stateText에 없어요");
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        player.stateInfo.SetActive(false);
    }
}
