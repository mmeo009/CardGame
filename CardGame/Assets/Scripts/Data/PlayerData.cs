using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerData : GenericSingleton<PlayerData>
{
    public Player player = new Player();
    public GameObject[] cards;
    public GameObject mana;
    public GameObject hp;
    private void Awake()
    {
        if (player.currentHealth == 0)
        {
            player.currentHealth = player.maxHealth;
        }
        if(player.currentMana == 0)
        {
            player.currentMana = player.maxMana;
        }
        ShowMyInfo();
    }
    public void ShowMyInfo()
    {
        if (mana != null)
        {
            mana.transform.GetChild(mana.transform.childCount - 1).GetComponent<TMP_Text>().text = player.currentMana + "/" + player.maxMana;
        }
        else
        {
            GameObject _mana = GameObject.Find("RemainingMana");
            mana = _mana;
            mana.transform.GetChild(mana.transform.childCount - 1).GetComponent<TMP_Text>().text = player.currentMana + "/" + player.maxMana;
        }

        if (hp != null)
        {
            hp.GetComponent<Image>();
        }
        else
        {
            GameObject _hp = GameObject.Find("PlayerHP");
            hp = _hp;
        }
    }

    public void CalculatingDamage()
    {
        player.adDamage = player.adPower * player.baPower;
        player.apDamage = player.apPower * player.baPower;
        player.fixedDamage = player.fixedPower;
    }

    public void GainingOrLosingValue(string value, int amount = 0, bool overHealing = false)
    {
        if (amount < 0)
        {
            switch (value)
            {
                case ("adPower"):
                    player.adPower -= amount;
                    break;
                case ("apPower"):
                    player.apPower -= amount;
                    break;
                case ("baPower"):
                    player.baPower -= amount;
                    break;
                case ("fixedPower"):
                    player.fixedPower -= amount;
                    break;
                case ("currentHealth"):
                    player.currentHealth -= amount;
                    if(player.currentHealth <=0)
                    {
                        PlayerDie();
                    }
                    break;
                case ("currentMana"):
                    player.currentHealth -= amount;
                    break;
                case ("shield"):
                    player.shield -= amount;
                    break;
            }
        }
        else if(amount >= 0)
        {
            switch (value)
            {
                case ("adPower"):
                    player.adPower += amount;
                    break;
                case ("apPower"):
                    player.apPower += amount;
                    break;
                case ("baPower"):
                    player.baPower += amount;
                    break;
                case ("fixedPower"):
                    player.fixedPower += amount;
                    break;
                case ("currentHealth"):
                    player.currentHealth += amount;
                    if (overHealing == true)
                    {
                            int toShield = player.currentHealth - player.maxHealth;
                            player.shield += toShield;
                    }
                    if (player.currentHealth > player.maxHealth)
                    {
                        player.currentHealth = player.maxHealth;
                    }
                    break;
                case ("currentMana"):
                    if(amount == 0)
                    {
                        player.currentMana = player.maxMana;
                    }
                    else
                    {
                        player.currentMana += amount;
                        if (player.currentMana > player.maxMana)
                        {
                            player.currentMana = player.maxMana;
                        }
                    }
                    break;
                case ("maxHealth"):
                    player.maxHealth += amount;
                    break;
                case ("maxMana"):
                    player.maxMana += amount;
                    break;
                case ("shield"):
                    player.shield += amount;
                    break;
                case ("blind"):
                    if(overHealing == true)
                    {
                        player.blind = true;
                    }
                    else
                    {
                        player.blind = false;
                    }
                    break;
                case ("fury"):
                    if (overHealing == true)
                    {
                        player.fury = true;
                    }
                    else
                    {
                        player.fury = false;
                    }
                    break;
                case ("sloth"):
                    if (overHealing == true)
                    {
                        player.sloth = true;
                    }
                    else
                    {
                        player.sloth = false;
                    }
                    break;
                case ("poison"):
                    if (overHealing == true)
                    {
                        player.poison = true;
                    }
                    else
                    {
                        player.poison = false;
                    }
                    break;
                case ("god"):
                    if (overHealing == true)
                    {
                        player.god = true;
                    }
                    else
                    {
                        player.god = false;
                    }
                    break;
                case ("temporary"):
                    if(player.temporary >= 0)
                    {
                        player.temporary = 2;
                    }
                    else if(player.temporary < 0)
                    {
                        player.temporary = 0;
                    }
                    else
                    {
                        player.temporary-=1;
                    }
                    break;
            }
        }
        CalculatingDamage();
        ValuesAreChanged();
        ShowMyInfo();
    }
    public void ValuesAreChanged()
    {
        if(cards == null)
        {
            GameObject[] _cards = GameObject.FindGameObjectsWithTag("Card");
            GameObject _enlargedCard = GameObject.FindWithTag("EnlargedCard");
            cards = _cards;
            cards[_cards.Length - 1] = _enlargedCard;

            for(int i = 0; i < cards.Length; i++)
            {
                string id = cards[i].GetComponent<CardDataLoad>().thisCardId;
                cards[i].GetComponent<CardDataLoad>().LoadCardData(id);
            }
        }
    }

    public void CalculatePorbability()
    {
        if (player.blind == true && player.fury == false)
        {
            player.hitProbability = 50;
        }
        else if (player.blind == true && player.fury == true)
        {
            player.hitProbability = 10;
        }
        else if(player.blind == false && player.fury == true)
        {
            player.hitProbability = 25;
        }
        else
        {
            player.hitProbability = 100;
        }
    }

    public void PlayerDie()
    {

    }

}
[System.Serializable]
public class Player
{
    public int adPower = 1;             // 물리 공격력
    public int apPower = 1;             // 마법 공격력
    public int fixedPower = 1;          // 고정 공격력
    public int baPower = 1;             // 기본 공격력
    public int plusPower = 0;           // 추가 공격력
    public int maxHealth = 30;          // 최대 체력
    public int currentHealth;           // 현재 체력
    public int maxMana = 3;             // 최대 마나
    public int currentMana;             // 현재 마나
    public int adDamage;
    public int apDamage;
    public int fixedDamage;
    public int shield;
    public int hitProbability = 100;
    public bool blind = false;
    public bool fury = false;
    public bool sloth = false;
    public bool poison = false;
    public bool god = false;
    public int temporary = 0;
}
