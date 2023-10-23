using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class MergeController : MonoBehaviour
{
    public string gridACardId = null;
    public string gridBCardId = null;
    public string _id = null;
    public int gridALevel;
    public int gridBLevel;
    public int level;
    string baseCardId = null;
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioClip merge;
    public void MergeCards()
    {
        audio = GetOrAddComponent<AudioSource>(this.gameObject);

        if (gridACardId != null && gridBCardId != null)
        {
            if (gridACardId == gridBCardId)
            {
                if (gridACardId != "104027A")
                {
                    int idLength = gridACardId.Length;
                    if (idLength > 0)
                    {
                        char lastId = gridACardId[idLength - 1];
                        if (lastId == 'A')
                        {
                            string __id = gridACardId.Replace("A", "B");
                            Entity_CardData.Param cardFromData = Managers.Data.cardDatabase.param.Find(entry => entry.id == __id);
                            if (cardFromData == null)
                            {
                                _id = __id.Replace("B", "N");
                            }
                            else
                            {
                                _id = __id;
                            }
                        }
                        else if (lastId == 'B')
                        {
                            _id = gridACardId.Replace("B", "C");
                        }
                        else if (lastId == 'N')
                        {
                            _id = gridACardId.Replace("N", "I");
                            level = 1;
                            baseCardId = gridACardId.Replace("N", "A");
                        }
                        else if (lastId == 'J')
                        {
                            _id = gridACardId.Replace("J", "T");
                        }
                        else if (lastId == 'T')
                        {
                            _id = gridACardId.Replace("T", "H");
                        }
                        else if (lastId == 'I')
                        {
                            if (gridALevel == gridBLevel)
                            {
                                _id = gridACardId;
                                level = gridALevel + 1;
                            }
                            else
                            {
                                Debug.Log("�� ī���� ��ȭ�ܰ谡 �޶�!");
                                _id = null;
                            }
                        }
                        else
                        {
                            Debug.Log($"ID : {gridACardId}�� ��ȭ�� �� ����");
                        }
                    }
                    if (_id != null)
                    {
                        gridACardId = null;
                        gridBCardId = null;
                        DrawCard.Instance.CreateCardFromNothing(_id, level);
                        if (baseCardId != null)
                        {
                            if (merge != null)
                            {
                                audio.clip = merge;
                                audio.Play();
                            }
                            else
                            {
                                AudioClip _merge = Resources.Load<AudioClip>("SoundEffects/Card_Merge");
                                merge = _merge;
                                audio.clip = merge;
                                audio.Play();
                            }
                            Managers.Deck.AddCardToDeckById(baseCardId, 4);
                            baseCardId = null;
                        }
                        _id = null;
                        level = 0;
                    }
                }
            }
            else
            {
                Debug.Log("ī���� ���̵� ��ġ���� �ʾ�");
            }
        }
        else
        {
            Debug.Log("ī���� ���̵� �������� �ʾ�");
        }
    }
}
