using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDataLoad : MonoBehaviour
{
    [SerializeField]
    public List<ObjectNameAndParent> thisCardinfo = new List<ObjectNameAndParent>();
    public CardUse cardUse;
    public string thisCardId;
    public int thisCardLevel;
    public SlotIndex mySlot;
    public bool dragOrder = false;
    public bool isHolding = false;

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
    public void InDragging()
    {
        if(dragOrder == false)
        {
            foreach (ObjectNameAndParent objectName in thisCardinfo)
            {
                SpriteRenderer sp = objectName.thisObject.GetComponent<SpriteRenderer>();
                RectTransform rt = objectName.thisObject.GetComponent<RectTransform>();
                if (sp != null)
                {
                    sp.sortingOrder += 14;
                }
                else if (objectName.thisObject.GetComponent<TextMeshPro>())
                {
                    Vector3 currentPosition = rt.localPosition;
                    currentPosition.z -= 10;
                    rt.localPosition = currentPosition;
                }
            }
            IsHolding(false);
            dragOrder = true;
        }
    }
    public void EndDragging()
    {
        if(dragOrder == true)
        {
            foreach (ObjectNameAndParent objectName in thisCardinfo)
            {
                SpriteRenderer sp = objectName.thisObject.GetComponent<SpriteRenderer>();
                RectTransform rt = objectName.thisObject.GetComponent<RectTransform>();
                if (sp != null)
                {
                    sp.sortingOrder -= 14;
                }
                else if (objectName.thisObject.GetComponent<TextMeshPro>())
                {
                    Vector3 currentPosition = rt.localPosition;
                    currentPosition.z += 10;
                    rt.localPosition = currentPosition;
                }
            }
            dragOrder = false;
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
            front.thisObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            isHolding = true;
        }
        else
        {
            front.thisObject.GetComponent<SpriteRenderer>().color = Color.white;
            isHolding = false;
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
            Debug.Log($"ID {cardId}를 가진 카드를 찾을 수 없습니다.");
        }
    }

    public void LoadCardData(string id)
    {
        foreach(ObjectNameAndParent go in thisCardinfo)
        {
            if(go.name == "Normal" || go.name == "Epic" || go.name == "Rare" || go.name == "Legendary" || go.name == "FP" || go.name == "AD" || go.name == "AP"
                || go.name == "DF" || go.name == "SP" || go.name == "HP" || go.name == "CC")
            {
                go.thisObject.SetActive(false);
            }
        }
        Entity_CardData.Param cardData = Managers.Data.cardsDictionary[id];
        if (cardData != null)
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
                cardImage.thisObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Illustration/Card/{id}");
            }

            // 카드 이름 및 텍스트 불러오기
            ObjectNameAndParent cardName = thisCardinfo.Find(name => name.name == "CardName");
            ObjectNameAndParent cardText = thisCardinfo.Find(name => name.name == "CardText");
            if(cardName != null)
            {
                cardName.thisObject.GetComponent<TextMeshPro>().text = cardData.cardName.Replace("[ra]", $"{thisCardLevel}");
            }
            if(cardText != null)
            {
                string _text = cardData.text.Replace("[ad]", $"<color=red>{PlayerData.Instance.player.adDamage + cardData.adPower}</color>")
                    .Replace("[ap]", $"<color=red>{PlayerData.Instance.player.apDamage + cardData.apPower}</color>")
                    .Replace("[fd]", $"<color=red>{PlayerData.Instance.player.fixedDamage + cardData.fixedPower}</color>")
                    .Replace("[had]", $"<color=red>{(PlayerData.Instance.player.adDamage + cardData.adPower) / 2}</color>")
                    .Replace("[hap]", $"<color=red>{(PlayerData.Instance.player.apDamage + cardData.adPower) / 2}</color>")
                    .Replace("[hfd]", $"<color=red>{(PlayerData.Instance.player.fixedDamage + cardData.adPower) / 2}</color>")
                    .Replace("[ra]", $"<color=red>{thisCardLevel}</color>")
                    .Replace("[fdd]", $"<color=red>{(cardData.fixedPower + PlayerData.Instance.player.apDamage) / 2}</color>")
                    .Replace("[adp]", $"<color=red>{PlayerData.Instance.player.apDamage * cardData.adPower}</color>")
                    .Replace("[app]", $"<color=red>{PlayerData.Instance.player.apDamage * cardData.apPower}</color>")
                    .Replace("[fdp]", $"<color=red>{PlayerData.Instance.player.apDamage * cardData.fixedPower}</color>")
                    ;

                cardText.thisObject.GetComponent<TextMeshPro>().text = _text;
            }

            // 카드 강화 단계 불러오기
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

            // 카드 코스트 불러오기
            ObjectNameAndParent manaText = thisCardinfo.Find(name => name.name == "ManaText");
            if(manaText != null)
            {
                manaText.thisObject.GetComponent<TMP_Text>().text = cardData.cardCost.ToString();
            }

            // 카드 스텟 불러오기
            ObjectNameAndParent typeText = thisCardinfo.Find(name => name.name == "TypeText");
            ObjectNameAndParent BG = thisCardinfo.Find(name => name.name == "BG");
            switch (cardData.cardType)
            {
                case 0:
                    ObjectNameAndParent SPN = thisCardinfo.Find(name => name.name == "SP");

                    if (SPN != null && typeText != null)
                    {
                        SPN.thisObject.SetActive(true);
                        typeText.thisObject.GetComponent<TextMeshPro>().text = "∞";
                    }
                    else
                    {
                        Debug.Log("해당 이미지를 찾을 수 없습니다.");
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
                        BG.thisObject.GetComponent<SpriteRenderer>().color = Color.red;
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
                        BG.thisObject.GetComponent<SpriteRenderer>().color = Color.cyan;
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
                        BG.thisObject.GetComponent<SpriteRenderer>().color = Color.magenta;

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
                        BG.thisObject.GetComponent<SpriteRenderer>().color = Color.red;

                    }
                    else if(cardData.id[2] == '2')
                    {
                        ObjectNameAndParent DFI = thisCardinfo.Find(name => name.name == "DF");
                        if (DFI != null && typeText != null)
                        {
                            DFI.thisObject.SetActive(true);
                            typeText.thisObject.GetComponent<TextMeshPro>().text = $"{PlayerData.Instance.player.apDamage * cardData.adPower}x{thisCardLevel}";
                        }
                        BG.thisObject.GetComponent<SpriteRenderer>().color = Color.cyan;

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
                        BG.thisObject.GetComponent<SpriteRenderer>().color = Color.white;

                    }
                    break;
            }

            if (this.gameObject.GetComponent<CardDataLoad>() != null)
            {
                this.gameObject.GetComponent<CardDataLoad>().thisCardId = id;
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
