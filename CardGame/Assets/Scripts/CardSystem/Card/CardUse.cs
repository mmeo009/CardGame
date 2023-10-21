using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CardUse : MonoBehaviour
{
    public Entity_CardData.Param thisCard;          // �� ī���� �����͸� ����
    public PlayerData player;                       // �÷��̾��� �����͸� ����
	public void GetData(string id)
    {
        Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[id];        // ī�带 �����Ϳ��� ã�ƿ�
        if(foundCard != null)                   // ī���� ���̵� ������ ���
        {
            thisCard = foundCard;               // ������ �Ҵ�
		}
        else                                    // ���� ���� ���� ��� �α� ���
        {
            Debug.Log("�ش� ���̵��� ī�尡 ���ӿ� �������� �ʽ��ϴ�.");
        }
        player = PlayerData.Instance;           //  �÷��̾ �ҷ��ͼ� �Ҵ�
    }
    public void UsingCard()                     // ī�带 ����ϴ� �Լ�
    {
        if(player == null)                      // �÷��̾ �������� ���� ���
        {
            player = PlayerData.Instance;       // �÷��̾� �ҷ��ͼ� �Ҵ�
        }
        if (thisCard != null)                   // ���̵� �����Ұ��
        {
            int cardType = (int)Char.GetNumericValue(thisCard.id[2]);       // ī���� Ÿ��(����, ���, ��ų, �����̻�)
            char cardLevel = thisCard.id[thisCard.id.Length - 1];           // ī���� ��ȭ ����
            int cardPlusData = thisCard.stat_05;                            // ī���� ��� ���� ������(cc��, ����Ȯ�� ��)
            int cardCost = thisCard.cardCost;                               // ī���� ���� ��
            int cardMethod = thisCard.usingMethod;                          // ī���� ��� ���
            string cardCC01 = thisCard.cc_1;                                // ī���� �����̻� 1��
            string cardCC02 = thisCard.cc_2;                                // ī���� �����̻� 2��
            int myMana = player.player.currentMana;                         // �÷��̾��� ���� ����
            if (cardLevel == 'A' || cardLevel == 'B' || cardLevel == 'C' || cardLevel == 'N')
            {
                if (cardType == 1)
                {
                    if(cardMethod == 0)             // �ƹ��͵� �ƴ� ����
                    {
                        Debug.Log("�ƹ� �ɷµ� ����");
                    }
                    if(cardMethod == 1)             // ������ ����
                    {
                        if(myMana >= cardCost)
                        {
                            player.CalculatePorbability();              // ���߷� ��� �Լ�
                            player.GainingOrLosingValue("currentMana", -cardCost);            // ���� ����
                            int num = UnityEngine.Random.Range(0, 100);
                            if(num <= player.player.hitProbability)    // ���� Ȯ�� 
                            {
                                if(thisCard.adPower != 0)           // ad���� ī���� ���
                                {
                                    MonsterData.Instance.GetDamage(player.player.adDamage + thisCard.adPower);
                                }
                                else if(thisCard.apPower != 0)      // ap���� ī���� ���
                                {
                                    MonsterData.Instance.GetDamage(player.player.apDamage + thisCard.apPower);
                                }
                                Debug.Log(num +"/"+ player.player.hitProbability);      // ���߷� ���� ���
                            }
                            else
                            {
                                Debug.Log("������");       // �������� ��� ���
                            }
                            Destroy(gameObject);        // ī�� ������Ʈ�� ����
                        }
                        else
                        {
                            Debug.Log("������ ��� �� �� ����");        // ������ ���� ��� ���
                        }
                    }
                    if(cardMethod == 2)             // ������ ����
                    {
                        if (myMana >= cardCost)
                        {
                            player.CalculatePorbability();      // ���߷� ��� �Լ�
                            player.GainingOrLosingValue("currentMana", -cardCost);            // ���� ����
                            int num = UnityEngine.Random.Range(0, 100);     
                            if (num <= player.player.hitProbability/2)      // ����Ȯ�� ( ���Ÿ� ����� ���ݰ����� ���)
                            {
                                if (thisCard.adPower != 0)      // ad���� ī���� ���
                                {
                                    MonsterData.Instance.GetDamage(player.player.adDamage + thisCard.adPower);
                                }
                                else if (thisCard.apPower != 0)     // ap���� ī���� ���
                                {
                                    MonsterData.Instance.GetDamage(player.player.apDamage + thisCard.apPower);
                                }
                                Debug.Log(num + "/" + player.player.hitProbability / 2);        // ���߷� ���� ���
                            }
                            else
                            {
                                Debug.Log("������");       // �������� ��� ���
                            }
                            Destroy(gameObject);        // ī�� ������Ʈ ����
                        }
                        else
                        {
                            Debug.Log("������ ��� �� �� ����");        // ������ ���� ��� ���
                        }
                    }
                    if(cardMethod == 3)             // ������ ���ƿ��� ����
                    {
                        if (myMana >= cardCost)
                        {
                            player.CalculatePorbability();      // ���߷� ��� �Լ�
                            player.GainingOrLosingValue("currentMana", -cardCost);            // ���� ����
                            int num = UnityEngine.Random.Range(0, 100);
                            if (num <= player.player.hitProbability)        // ����Ȯ�� ( ������ ���ƿ��� �����̱⿡ �״�� ���)
                            {
                                if (thisCard.adPower != 0)      // ad������ ���
                                {
                                    MonsterData.Instance.GetDamage(player.player.adDamage + thisCard.adPower);      // ���� ü�� ����
                                    if (num/2 <= player.player.hitProbability)      // ���� �ڽſ��� ���ƿ��� Ȯ�� ���
                                    {
                                        player.GainingOrLosingValue("currentHealth", (player.player.adDamage + thisCard.adPower)/2);        //�� ü�� ����
                                    }
                                    else
                                    {
                                        Debug.Log("������ ������");       // ������ �������� ��� ���
                                    }
                                }
                                else if (thisCard.apPower != 0)     //ap������ ���
                                {
                                    MonsterData.Instance.GetDamage(player.player.apDamage + thisCard.apPower);
                                    if (num / 2 <= player.player.hitProbability)
                                    {
                                        player.GainingOrLosingValue("currentHealth", (player.player.apDamage + thisCard.apPower) / 2);
                                    }
                                    else
                                    {
                                        Debug.Log("������ ������");
                                    }
                                }
                                Debug.Log(num + "/" + player.player.hitProbability);        // ���߷� ���� ���
                            }
                            else
                            {
                                Debug.Log("������");       // �������� ��� ���
                            }
                            Destroy(gameObject);        // ī�� ������Ʈ ����
                        }
                        else
                        {
                            Debug.Log("������ ��� �� �� ����");        // ������ ���� ��� ���
                        }
                    }
                }
                else if (cardType == 2)     // ���ī�� ����
                {
                    if (cardMethod == 0)        // ��� ī�� �� �ƹ� �ɷ��� ���� ī��
                    {
                        Debug.Log("�ƹ� �ɷµ� ����");
                    }
                    else if (cardMethod == 1)       // ���� ���� ī��
                    {
                        if (myMana >= cardCost)
                        {
                            player.GainingOrLosingValue("currentMana", -cardCost);            // ���� ����
                            player.GainingOrLosingValue("shield", (player.player.apPower* thisCard.adPower));       // �÷��̾� ���� ����
                            Destroy(gameObject);        //ī�� ������Ʈ ����
                        }
                        else
                        {
                            Debug.Log("������ ��� �� �� ����");        // ������ ���� ��� ���
                        }
                    }
                    else if(cardMethod == 2)        // ü�� ȸ�� ī��
                    {
                        if (myMana >= cardCost)
                        {
                            player.GainingOrLosingValue("currentMana", -cardCost);            // ���� ����
                            player.GainingOrLosingValue("currentHealth", (player.player.apPower * thisCard.apPower));       // �÷��̾� ü�� ����
                            Destroy(gameObject);        // ī�� ������Ʈ ����
                        }
                        else
                        {
                            Debug.Log("������ ��� �� �� ����");        // ������ ���� ��� ���
                        }
                    }
                    else if (cardMethod == 3)       // ü���� ȸ���ϰ� ���� ü���� �������� ���� ���� �������� ��ȯ�ϴ� ī��
                    {
                        if (myMana >= cardCost)
                        {
                            player.GainingOrLosingValue("currentMana", -cardCost);            // ���� ����
                            player.GainingOrLosingValue("currentHealth", (player.player.apPower * thisCard.apPower), true);     // �÷��̾� ü������ �� �����ϴٸ� ���� ����
                            Destroy(gameObject);        // ī�� ������Ʈ ����
                        }
                        else
                        {
                            Debug.Log("������ ��� �� �� ����");        // ������ ���� ��� ���
                        }
                    }
                    else if (cardMethod == 4)       // õõ�� ������� ü���� ȸ���ϴ� ī��
                    {
                        if (myMana >= cardCost)
                        {
                            player.GainingOrLosingValue("currentMana", -cardCost);            // ���� ����
                            player.GainingOrLosingValue("currentHealth", (player.player.apPower * thisCard.fixedPower));        // �÷��̾��� ü���� ȸ��
                            player.GainingOrLosingValue("temporary", 2, false, (player.player.apPower * thisCard.fixedPower));      // õõ�� ������� ü�� �����̻��� �ο�
                            Destroy(gameObject);        // ī�� ������Ʈ ����
                        }
                        else
                        {
                            Debug.Log("������ ��� �� �� ����");        // ������ ���� ��� ���
                        }
                    }
                    else if(cardMethod == 5)        // ������ �Ǵ� ī��
                    {
                        if (myMana >= cardCost)
                        {
                            player.GainingOrLosingValue("currentMana", -cardCost);            // ���� ����
                            player.GainingOrLosingValue("god", 0, true);        // ���� �����̻��� �ο�
                            Destroy(gameObject);        // ī�� ������Ʈ ����
                        }
                        else
                        {
                            Debug.Log("������ ��� �� �� ����");        // ������ ���� ��� ���
                        }
                    }

                }
                else if (cardType == 3)     // ��ų ī��
                {
                    if(cardMethod == 0)
                    {

                    }
                }
                else if (cardType == 4)     // �����̻� ī��
                {
                    if(cardMethod == 0)     // ��� ������ �����̻� ī�� (ȭ��)
                    {
                        if(myMana >= cardCost)
                        {
                            player.GainingOrLosingValue("currentMana", -cardCost);            // ���� ����
                            Destroy(gameObject);        // ī�� ������Ʈ ����
                        }
                        else
                        {
                            Debug.Log("������ ��� �� �� ����");        // ������ ���� ��� ���
                        }
                    }
                }
            }
            else if(cardLevel == 'I')       //���Ѵ�� ��ȭ�� ������ ī��
            {
                if(cardType == 1)       // ���Ѵ�� ��ȭ�� ������ ���� ī��
                {
                    if (myMana >= cardCost)
                    {
                        int thisCardLevel = this.GetComponent<CardDataLoad>().thisCardLevel;
                        player.CalculatePorbability();
                        player.GainingOrLosingValue("currentMana", -cardCost);            // ���� ����
                        for(int i  = 0; i < thisCardLevel; i++)
                        {
                            int num = UnityEngine.Random.Range(0, 100);
                            if (num <= player.player.hitProbability / 2)
                            {
                                if (thisCard.adPower != 0)
                                {
                                    MonsterData.Instance.GetDamage(player.player.adDamage + thisCard.adPower);
                                }
                                else if (thisCard.apPower != 0)
                                {
                                    MonsterData.Instance.GetDamage(player.player.apDamage + thisCard.apPower);
                                }
                                Debug.Log(num + "/" + player.player.hitProbability / 2);
                            }
                            else
                            {
                                Debug.Log("������");       // ������ ���
                            }
                        }
                        Destroy(gameObject);        // ī�� ������Ʈ ����
                    }
                    else
                    {
                        Debug.Log("������ ��� �� �� ����");
                    }
                }
                else if(cardType == 2)      // ���Ѵ�� ��ȭ�� ������ ��� ī��
                {
                    player.GainingOrLosingValue("currentMana", -cardCost);            // ���� ����
                    for (int i = 0; i<player.player.currentMana - cardCost; i++)
                    {
                        player.GainingOrLosingValue("currentMana", -1);            // ���� ����
                        player.GainingOrLosingValue("currentHealth", (player.player.apPower * thisCard.apPower), true);
                    }
                }
            }
            else
            {
                Debug.Log("���ƿ�");       // ���� �� ���� ������ ī��
            }
        }
        else
        {
            GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
        }
        
    }
}
