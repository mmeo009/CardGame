using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : GenericSingleton<PlayerData>
{
    public Player player = new Player();
    public GameObject[] cards;
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
    }

    public void CalculatingDamage()
    {
        player.adDamage = player.adPower * player.baPower;
        player.apDamage = player.apPower * player.baPower;
        player.fixedDamage = player.fixedPower;
    }

    public void GainingOrLosingValue(string value, int amount, bool overHealing = false)
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
                    player.currentHealth += amount;
                    if(player.currentMana > player.maxMana)
                    {
                        player.currentMana = player.maxMana;
                    }
                    break;
                case ("maxHealth"):
                    player.maxHealth += amount;
                    break;
                case ("maxMana"):
                    player.maxMana += amount;
                    break;
            }
        }
        CalculatingDamage();
        ValuesAreChanged();
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
}
