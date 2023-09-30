using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDataLoad : MonoBehaviour
{
    [SerializeField]
    private List<ObjectNameAndParent> thisCardinfo = new List<ObjectNameAndParent>();
    public string thisCardId;

    public void FindChilds(GameObject target)
    {
        for (int i = 0; i < target.gameObject.transform.childCount; i++)
        {
            Transform childTransform = target.gameObject.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            string childName = childObject.name;
            ObjectNameAndParent info = new ObjectNameAndParent
            {
                name = childName,
                thisObject = childObject,
                parantName = target.name,
                parantObject = target.gameObject
            };
            thisCardinfo.Add(info);
            Debug.Log(target.name);
            if(childObject.gameObject.transform.childCount > 0)
            {
                FindChilds(childObject);
            }
        }
    }
    public void IsHolding(bool hold)
    {
        ObjectNameAndParent front = thisCardinfo.Find(name => name.name == "Front");
        if (hold == true)
        {
            front.thisObject.GetComponent<Image>().color = Color.cyan;
        }
        else
        {
            front.thisObject.GetComponent<Image>().color = Color.white;
        }
    }

    public void PickCardAndIdFromDeck()
    {
        if(CardData.Instance != null)
        {
            if(CardData.Instance.deck.Count != 0)
            {
                int card = Random.Range(0, CardData.Instance.deck.Count);
                CardDataEntry oneCard = CardData.Instance.deck[card];
                thisCardId = oneCard.id;
                Managers.Deck.RemoveCardToDeckById(thisCardId, 1);
                LoadCardData(thisCardId);
            }
        }
    }

    public void LoadCardData(string id)
    {
        Entity_CardData.Param cardData = CardData.Instance.cardDatabase.param.Find(card => card.id == id);
        if(cardData != null)
        {
            // ��͵� �ҷ�����
            switch (cardData.rarity)
            {
                // normal Ƽ��
                case 1:
                    ObjectNameAndParent normal = thisCardinfo.Find(name => name.name == "Normal");
                    if (normal != null)
                    {
                        normal.thisObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("Ƽ�� �̹����� ã�� �� �����ϴ�.");
                    }
                    break;
                    // rare Ƽ��
                case 2:
                    ObjectNameAndParent rare = thisCardinfo.Find(name => name.name == "Rare");
                    if (rare != null)
                    {
                        rare.thisObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("Ƽ�� �̹����� ã�� �� �����ϴ�.");
                    }
                    break;
                    // epic Ƽ��
                case 3:
                    ObjectNameAndParent epic = thisCardinfo.Find(name => name.name == "Epic");
                    if (epic != null)
                    {
                        epic.thisObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("Ƽ�� �̹����� ã�� �� �����ϴ�.");
                    }
                    break;
                    // legendary Ƽ��
                case 4:
                    ObjectNameAndParent legendary = thisCardinfo.Find(name => name.name == "Legendary");
                    if (legendary != null)
                    {
                        legendary.thisObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("Ƽ�� �̹����� ã�� �� �����ϴ�.");
                    }
                    break;
            }

            // ī�� �̹��� �ҷ�����
            ObjectNameAndParent cardImage = thisCardinfo.Find(name => name.name == "CardImage");
            if (cardImage != null)
            {
                cardImage.thisObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Illustration/{id}");
            }

            // ī�� �̸� �� �ؽ�Ʈ �ҷ�����
            ObjectNameAndParent cardName = thisCardinfo.Find(name => name.name == "CardName");
            ObjectNameAndParent cardText = thisCardinfo.Find(name => name.name == "CardText");
            if(cardName != null)
            {
                cardName.thisObject.GetComponent<TMP_Text>().text = cardData.cardName;
            }
            if(cardText != null)
            {
                string _text = cardData.text.Replace("[ad]", $"{PlayerData.Instance.player.adDamage + cardData.adPower}")
                    .Replace("[ap]", $"{PlayerData.Instance.player.apDamage + cardData.apPower}")
                    .Replace("[fd]", $"{PlayerData.Instance.player.fixedDamage + cardData.fixedPower}")
                    .Replace("[had]", $"{(PlayerData.Instance.player.adDamage + cardData.adPower) / 2}")
                    .Replace("[hap]", $"{(PlayerData.Instance.player.apDamage + cardData.adPower) / 2}")
                    .Replace("[hfd]", $"{(PlayerData.Instance.player.fixedDamage + cardData.adPower) / 2}");

                cardText.thisObject.GetComponent<TMP_Text>().text = _text;
            }
        }
        else
        {
            Debug.Log($"ID {cardData}�� ���� ī�带 ã�� �� �����ϴ�.");
        }

    }
}
[System.Serializable]
public class ObjectNameAndParent
{
    public string name;
    public GameObject thisObject;
    public string parantName;
    public GameObject parantObject;
}
