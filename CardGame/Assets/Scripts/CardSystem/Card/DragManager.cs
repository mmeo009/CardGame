using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public CardUse thisCard;
    public bool isEnlargedCardCreated = false;

    private GameObject enlargedCardPrefab;



    void SendRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

    }

    private void CreateEnlargedCard()
    {
        Vector3 pos = GameObject.FindGameObjectWithTag("EnlargedPosition").transform.position;
        string id = thisCard.GetComponent<CardDataLoad>().thisCardId;
        int level = thisCard.GetComponent<CardDataLoad>().thisCardLevel;
        if (enlargedCardPrefab == null)
        {
            enlargedCardPrefab = Resources.Load<GameObject>("Prefabs/EnlargedCard");
            GameObject newCard = Instantiate(enlargedCardPrefab, pos, Quaternion.identity);
            newCard.transform.localScale = Vector3.one;
            newCard.GetComponent<RectTransform>().sizeDelta = new Vector3(180, 320);
            newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
            newCard.GetComponent<CardDataLoad>().thisCardLevel = level;
            newCard.GetComponent<CardDataLoad>().LoadCardData(id);
            newCard.transform.SetParent(GameObject.Find("Canvas").transform);

        }
        else
        {
            GameObject newCard = Instantiate(enlargedCardPrefab, pos, Quaternion.identity);
            newCard.transform.localScale = Vector3.one;
            newCard.GetComponent<RectTransform>().sizeDelta = new Vector3(180, 320);
            newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
            newCard.GetComponent<CardDataLoad>().thisCardLevel = level;
            newCard.GetComponent<CardDataLoad>().LoadCardData(id);
            newCard.transform.SetParent(GameObject.Find("Canvas").transform);

        }
        isEnlargedCardCreated = true;
    }
}
