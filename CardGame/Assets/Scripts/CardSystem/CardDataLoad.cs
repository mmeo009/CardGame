using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDataLoad : MonoBehaviour
{
    [SerializeField]
    private List<ObjectNameAndParent> thisCardinfo = new List<ObjectNameAndParent>();
    [SerializeField]
    private string thisCardId;

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

    private void LoadCardData(string id)
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
                cardText.thisObject.GetComponent<TMP_Text>().text = cardData.text;
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
