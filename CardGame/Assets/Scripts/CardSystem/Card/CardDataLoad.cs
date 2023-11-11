using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDataLoad : MonoBehaviour
{
    [SerializeField]
    private List<ObjectNameAndParent> thisCardinfo = new List<ObjectNameAndParent>();
    public CardUse cardUse;
    public string thisCardId;
    public int thisCardLevel;

    public void FindChilds(GameObject target)
    {
        cardUse = GetComponent<CardUse>();
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
            //Debug.Log(target.name);
            if(childObject.gameObject.transform.childCount > 0)
            {
                FindChilds(childObject);
            }
        }
    }
    public void LoadCardLevel()
    {
        char lastId = thisCardId[thisCardId.Length - 1];
        int level = 1;
        switch (lastId)
        {
            case 'A':
                level = 1;
                break;
            case 'B':
                level = 2;
                break;
            case 'C':
                level = 3;
                break;
            case 'J':
                level = 1;
                break;
            case 'T':
                level = 2;
                break;
            case 'H':
                level = 3;
                break;
            case 'N':
                level = 2;
                break;
        }
        thisCardLevel = level;
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
        if(DeckData.Instance != null)
        {
            if(DeckData.Instance.deck.Count != 0)
            {
                int card = Random.Range(0, DeckData.Instance.deck.Count);
                CardInformation oneCard = DeckData.Instance.deck[card];
                thisCardId = oneCard.id;
                thisCardLevel = oneCard.level;
                Managers.Deck.RemoveCardToDeckById(thisCardId, thisCardLevel, 1);
                cardUse.GetData(thisCardId);
                LoadCardData(thisCardId);
            }
        }
    }

    public void PickCardIdFromDataBase(string cardId, int level = 0)
    {
        Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[cardId];
        if (foundCard != null)
        {
            thisCardId = cardId;
            if(level == 0)
            {
                LoadCardLevel();
            }
            else
            {
                thisCardLevel = level;
            }
            LoadCardData(thisCardId);
            cardUse.GetData(thisCardId);
        }
        else
        {
            Debug.Log($"ID {cardId}�� ���� ī�带 ã�� �� �����ϴ�.");
        }
    }

    public void LoadCardData(string id)
    {
        Entity_CardData.Param cardData = Managers.Data.cardsDictionary[id];
        if (cardData != null)
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
                cardImage.thisObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Illustration/Card/{id}");
            }

            // ī�� �̸� �� �ؽ�Ʈ �ҷ�����
            ObjectNameAndParent cardName = thisCardinfo.Find(name => name.name == "CardName");
            ObjectNameAndParent cardText = thisCardinfo.Find(name => name.name == "CardText");
            if(cardName != null)
            {
                cardName.thisObject.GetComponent<TextMeshPro>().text = cardData.cardName.Replace("[ra]", $"{thisCardLevel}");
            }
            if(cardText != null)
            {
                string _text = cardData.text.Replace("[ad]", $"{PlayerData.Instance.player.adDamage + cardData.adPower}")
                    .Replace("[ap]", $"{PlayerData.Instance.player.apDamage + cardData.apPower}")
                    .Replace("[fd]", $"{PlayerData.Instance.player.fixedDamage + cardData.fixedPower}")
                    .Replace("[had]", $"{(PlayerData.Instance.player.adDamage + cardData.adPower) / 2}")
                    .Replace("[hap]", $"{(PlayerData.Instance.player.apDamage + cardData.adPower) / 2}")
                    .Replace("[hfd]", $"{(PlayerData.Instance.player.fixedDamage + cardData.adPower) / 2}")
                    .Replace("[ra]", $"{thisCardLevel}")
                    .Replace("[fdd]", $"{(cardData.fixedPower + PlayerData.Instance.player.apDamage) / 2}")
                    .Replace("[adp]", $"{PlayerData.Instance.player.apDamage * cardData.adPower}")
                    .Replace("[app]", $"{PlayerData.Instance.player.apDamage * cardData.apPower}")
                    .Replace("[fdp]", $"{PlayerData.Instance.player.apDamage * cardData.fixedPower}")
                    ;

                cardText.thisObject.GetComponent<TextMeshPro>().text = _text;
            }

            // ī�� ��ȭ �ܰ� �ҷ�����
            ObjectNameAndParent levelText = thisCardinfo.Find(name => name.name == "LevelText");
            if(levelText != null)
            {
                int levelNum = 0;
                char level = cardData.id[cardData.id.Length - 1];
                if (level == 'A' || level == 'J')
                {
                    levelNum = 1;
                }
                else if(level == 'B' || level == 'T')
                {
                    levelNum = 2;
                }
                else if(level == 'C' || level == 'H')
                {
                    levelNum = 3;
                }
                else if(level == 'I' || level == 'N')
                {
                    levelNum = thisCardLevel;
                }
                    levelText.thisObject.GetComponent<TextMeshPro>().text = levelNum.ToString();
            }

            // ī�� �ڽ�Ʈ �ҷ�����
            ObjectNameAndParent manaText = thisCardinfo.Find(name => name.name == "ManaText");
            if(manaText != null)
            {
                manaText.thisObject.GetComponent<TMP_Text>().text = cardData.cardCost.ToString();
            }

            // ī�� ���� �ҷ�����
            ObjectNameAndParent typeText = thisCardinfo.Find(name => name.name == "TypeText");
            switch (cardData.cardType)
            {
                case 0:
                    ObjectNameAndParent SPN = thisCardinfo.Find(name => name.name == "SP");

                    if (SPN != null && typeText != null)
                    {
                        SPN.thisObject.SetActive(true);
                        typeText.thisObject.GetComponent<TextMeshPro>().text = "��";
                    }
                    else
                    {
                        Debug.Log("�ش� �̹����� ã�� �� �����ϴ�.");
                    }
                    break;
                case 1:
                    ObjectNameAndParent AD = thisCardinfo.Find(name => name.name == "AD");
                    ObjectNameAndParent AP = thisCardinfo.Find(name => name.name == "AP");
                    ObjectNameAndParent FP = thisCardinfo.Find(name => name.name == "FP");
                    if (AD != null && AP != null && FP != null)
                    {
                        if(cardData.adPower != 0)
                        {
                            AD.thisObject.SetActive(true);
                            typeText.thisObject.GetComponent<TextMeshPro>().text = cardData.adPower.ToString();
                        }
                        else if(cardData.apPower != 0)
                        {
                            AP.thisObject.SetActive(true);
                            typeText.thisObject.GetComponent<TextMeshPro>().text = cardData.apPower.ToString();
                        }
                        else if(cardData.fixedPower != 0)
                        {
                            FP.thisObject.SetActive(true);
                            typeText.thisObject.GetComponent<TextMeshPro>().text = cardData.fixedPower.ToString();
                        }
                        else
                        {
                            AD.thisObject.SetActive(true);
                            typeText.thisObject.GetComponent<TextMeshPro>().text = cardData.adPower.ToString();
                        }
                    }
                    break;
                case 2:
                    ObjectNameAndParent DF = thisCardinfo.Find(name => name.name == "DF");
                    ObjectNameAndParent HP = thisCardinfo.Find(name => name.name == "HP");
                    if (DF != null && HP != null)
                    {
                        if(cardData.adPower != 0)
                        {
                            DF.thisObject.SetActive(true);
                            typeText.thisObject.GetComponent<TextMeshPro>().text = cardData.adPower.ToString();
                        }
                        else if(cardData.apPower != 0)
                        {
                            HP.thisObject.SetActive(true);
                            typeText.thisObject.GetComponent<TextMeshPro>().text = cardData.apPower.ToString();
                        }
                        else if(cardData.fixedPower != 0)
                        {
                            HP.thisObject.SetActive(true);
                            typeText.thisObject.GetComponent<TextMeshPro>().text = cardData.fixedPower.ToString();
                        }
                    }
                    break;
                case 3:
                    ObjectNameAndParent SP = thisCardinfo.Find(name => name.name == "SP");
                    if(SP != null && typeText != null)
                    {
                        SP.thisObject.SetActive(true);
                        if(cardData.adPower == 0 && cardData.apPower == 0)
                        {
                            typeText.thisObject.SetActive(false);
                        }
                        else
                        {
                            typeText.thisObject.GetComponent<TextMeshPro>().text += cardData.adPower.ToString();
                        }
                    }    
                    break;
                case 4:
                    if(cardData.id[2] == '1')
                    {
                        ObjectNameAndParent SPI = thisCardinfo.Find(name => name.name == "SP");
                        if (SPI != null && typeText != null)
                        {
                            SPI.thisObject.SetActive(true);
                            typeText.thisObject.GetComponent<TextMeshPro>().text = $"{PlayerData.Instance.player.adDamage + cardData.adPower}x{thisCardLevel}";
                        }
                    }
                    else if(cardData.id[2] == '2')
                    {
                        ObjectNameAndParent DFI = thisCardinfo.Find(name => name.name == "DF");
                        if (DFI != null && typeText != null)
                        {
                            DFI.thisObject.SetActive(true);
                            typeText.thisObject.GetComponent<TextMeshPro>().text = $"{PlayerData.Instance.player.apDamage * cardData.adPower}x{thisCardLevel}";
                        }
                    }
                    else if(cardData.id[2] == '4')
                    {
                        ObjectNameAndParent CC = thisCardinfo.Find(name => name.name == "CC");
                        if (CC != null && typeText != null)
                        {
                            CC.thisObject.SetActive(true);
                            if(cardData.adPower != 0)
                            {
                                typeText.thisObject.GetComponent<TextMeshPro>().text = $"{PlayerData.Instance.player.adDamage * cardData.adPower}";
                            }
                            else
                            {
                                typeText.thisObject.SetActive(false);
                            }
                            
                        }
                    }
                    break;
            }

            if (this.gameObject.GetComponent<CardController>() != null)
            {
                this.gameObject.GetComponent<CardController>().id = id;
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
