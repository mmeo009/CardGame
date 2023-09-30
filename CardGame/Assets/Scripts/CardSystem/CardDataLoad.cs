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
            // 희귀도 불러오기
            switch (cardData.rarity)
            {
                // normal 티어
                case 1:
                    ObjectNameAndParent normal = thisCardinfo.Find(name => name.name == "Normal");
                    if (normal != null)
                    {
                        normal.thisObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("티어 이미지를 찾을 수 없습니다.");
                    }
                    break;
                    // rare 티어
                case 2:
                    ObjectNameAndParent rare = thisCardinfo.Find(name => name.name == "Rare");
                    if (rare != null)
                    {
                        rare.thisObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("티어 이미지를 찾을 수 없습니다.");
                    }
                    break;
                    // epic 티어
                case 3:
                    ObjectNameAndParent epic = thisCardinfo.Find(name => name.name == "Epic");
                    if (epic != null)
                    {
                        epic.thisObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("티어 이미지를 찾을 수 없습니다.");
                    }
                    break;
                    // legendary 티어
                case 4:
                    ObjectNameAndParent legendary = thisCardinfo.Find(name => name.name == "Legendary");
                    if (legendary != null)
                    {
                        legendary.thisObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("티어 이미지를 찾을 수 없습니다.");
                    }
                    break;
            }

            // 카드 이미지 불러오기
            ObjectNameAndParent cardImage = thisCardinfo.Find(name => name.name == "CardImage");
            if (cardImage != null)
            {
                cardImage.thisObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Illustration/{id}");
            }

            // 카드 이름 및 텍스트 불러오기
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
            Debug.Log($"ID {cardData}를 가진 카드를 찾을 수 없습니다.");
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
