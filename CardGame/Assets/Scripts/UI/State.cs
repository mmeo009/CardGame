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
                    stateText.text = $"������ ������ �� {poisonCC.damage}��ŭ �������� �Խ��ϴ�.";
                }
            }
            else if (name == "temporary")
            {
                Player.CC temporaryCC = player.player.playerCc.Find(cc => cc.ccName == "temporary");
                if (temporaryCC != null)
                {
                    stateText.text = $"������ ������ �� {temporaryCC.damage}��ŭ �������� �Խ��ϴ�.";
                }
            }
            else if (name == "blind")
            {
                stateText.text = "�÷��̾��� ���߷��� 50% �����մϴ�.";
            }
            else if (name == "fury")
            {
                stateText.text = "�÷��̾��� ���ݷ��� 3 ������ ���߷��� 75% �����մϴ�.";
            }
            else if (name == "sloth")
            {
                stateText.text = "���� ī�带 ����ϸ� ������ �������ϴ�.";
            }
        }
        else
        {
            Debug.LogError("TMP_Text�� stateText�� �����");
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        player.stateInfo.SetActive(false);
    }
}
