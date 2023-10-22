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
    public GameObject shieldText;
    public GameObject hpText;
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
            float hpfill = (float)player.currentHealth / (float)player.maxHealth;
            Debug.Log(hpfill);
            hp.GetComponent<Image>().fillAmount = hpfill;
        }
        else
        {
            GameObject _hp = GameObject.Find("PlayerHP");
            hp = _hp;
            float hpfill = (float)player.currentHealth / (float)player.maxHealth;
            Debug.Log(hpfill);
            hp.GetComponent<Image>().fillAmount = hpfill;
        }
        if(hpText != null)
        {
            hpText.GetComponent<TMP_Text>().text = player.currentHealth + "/" + player.maxHealth;
        }
        else
        {
            GameObject _hpT = GameObject.Find("HPText");
            hpText = _hpT;
            hpText.GetComponent<TMP_Text>().text = player.currentHealth + "/" + player.maxHealth;
        }
        if (shieldText != null)
        {
            if (player.shield > 0)
            {
                shieldText.GetComponent<TMP_Text>().text = player.shield.ToString();
            }
            else
            {
                shieldText.GetComponent<TMP_Text>().text = "---";
            }

        }
        else
        {
            GameObject _shieldT = GameObject.Find("ShieldText");
            shieldText = _shieldT;
            if (player.shield > 0)
            {
                shieldText.GetComponent<TMP_Text>().text = player.shield.ToString();
            }
            else
            {
                shieldText.GetComponent<TMP_Text>().text = "---";
            }
        }
    }

    public void CalculatingDamage()
    {
        player.adDamage = player.adPower * player.baPower;
        player.apDamage = player.apPower * player.baPower;
        player.fixedDamage = player.fixedPower;
    }

    public void GainingOrLosingValue(string value, int amount = 0, bool overHealing = false, int ccDamage = 2)
    {
        if (amount < 0)
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
                    if(player.currentHealth <=0)
                    {
                        PlayerDie();
                    }
                    break;
                case ("currentMana"):
                    player.currentMana += amount;
                    break;
                case ("shield"):
                    player.shield += amount;
                    break;
                case ("poison"):
                    Player.CC poison = player.playerCc.Find(cc => cc.ccName == "poison");
                    if (poison != null)
                    {
                        if (poison.remainingTurn <= 0)
                        {
                            player.playerCc.Remove(poison);
                        }
                        player.currentHealth -= poison.damage;
                        poison.remainingTurn += amount;
                    }
                    break;
                case ("temporary"):
                    Player.CC temporary = player.playerCc.Find(cc => cc.ccName == "temporary");
                    if (temporary != null)
                    {
                        if (temporary.remainingTurn <= 0)
                        {
                            player.playerCc.Remove(temporary);
                        }
                        player.currentHealth -= temporary.damage / 2;
                        temporary.remainingTurn += amount;
                    }
                    break;
                case ("blind"):
                    Player.CC blind = player.playerCc.Find(cc => cc.ccName == "blind");
                    if (blind != null)
                    {
                        if(blind.remainingTurn <= 0)
                        {
                            player.playerCc.Remove(blind);
                        }
                        blind.remainingTurn += amount;
                    }
                    break;
                case ("fury"):
                    Player.CC fury = player.playerCc.Find(cc => cc.ccName == "fury");
                    if (fury != null)
                    {
                        if (fury.remainingTurn <= 0)
                        {
                            player.playerCc.Remove(fury);
                        }
                        fury.remainingTurn += amount;
                    }
                    break;
                case ("sloth"):
                    Player.CC sloth = player.playerCc.Find(cc => cc.ccName == "sloth");
                    if (sloth != null)
                    {
                        if (sloth.remainingTurn <= 0)
                        {
                            player.playerCc.Remove(sloth);
                        }
                        sloth.remainingTurn += amount;
                    }
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
                    int toShield = player.currentHealth + amount - player.maxHealth;
                    if (overHealing == true)
                    {
                        player.currentHealth = player.maxHealth;
                        player.shield += toShield;
                    }
                    else
                    {
                        player.currentHealth += amount;
                    }
                    if (player.currentHealth > player.maxHealth)
                    {
                        player.currentHealth = player.maxHealth;
                    }
                    break;
                case ("currentMana"):
                    if(amount == 0 && overHealing == true)
                    {
                        player.currentMana = player.maxMana;
                    }
                    else
                    {
                        player.currentMana += amount;
                        if (player.currentMana > player.maxMana && overHealing == false)
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
                    if (amount == 0 && overHealing == true)
                    {
                        player.shield = 0;
                    }
                    else
                    {
                        player.shield += amount;
                    }
                    break;
                case ("blind"):
                    Player.CC blind = player.playerCc.Find(cc => cc.ccName == "blind");
                    if (blind == null)
                    {
                        Player.CC newCc = new Player.CC
                        {
                            ccName = "blind",
                            remainingTurn = amount,
                            damage = 0
                        };
                        player.playerCc.Add(newCc);
                    }
                    else
                    {
                        blind.remainingTurn += amount;
                    }
                    break;
                case ("fury"):
                    Player.CC fury = player.playerCc.Find(cc => cc.ccName == "fury");
                    if (fury == null)
                    {
                        Player.CC newCc = new Player.CC
                        {
                            ccName = "fury",
                            remainingTurn = amount,
                            damage = 0
                        };
                        player.playerCc.Add(newCc);
                    }
                    else
                    {
                        fury.remainingTurn += amount;
                    }
                    break;
                case ("sloth"):
                    Player.CC sloth = player.playerCc.Find(cc => cc.ccName == "sloth");
                    if (sloth == null)
                    {
                        Player.CC newCc = new Player.CC
                        {
                            ccName = "sloth",
                            remainingTurn = amount,
                            damage = 0
                        };
                        player.playerCc.Add(newCc);
                    }
                    else
                    {
                        sloth.remainingTurn += amount;
                    }
                    break;
                case ("poison"):
                    Player.CC poison = player.playerCc.Find(cc => cc.ccName == "poison");
                    if(poison == null)
                    {
                        Player.CC newCc = new Player.CC
                        {
                            ccName = "poison",
                            remainingTurn = amount,
                            damage = ccDamage
                        };
                        player.playerCc.Add(newCc);
                    }
                    else
                    {
                        poison.remainingTurn += amount/2;
                        if(poison.damage != ccDamage)
                        {
                            poison.damage = ccDamage;
                        }
                    }
                    break;
                case ("temporary"):
                    Player.CC temporary = player.playerCc.Find(cc => cc.ccName == "temporary");
                    if(temporary == null)
                    {
                        Player.CC newCc = new Player.CC
                        {
                            ccName = "temporary",
                            remainingTurn = amount,
                            damage = ccDamage
                        };
                        player.playerCc.Add(newCc);
                    }
                    else
                    {
                        if(temporary.remainingTurn == 2)
                        {
                            temporary.damage += ccDamage;
                        }
                        else if(temporary.remainingTurn == 1)
                        {
                            temporary.remainingTurn = 2;
                            player.currentHealth -= temporary.damage / 2;
                            temporary.damage = ccDamage;
                        }
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
    public void CCChange()
    {
        GainingOrLosingValue("poison", -1);
        GainingOrLosingValue("temporary", -1);
        GainingOrLosingValue("blind", -1);
        GainingOrLosingValue("fury", -1);
        GainingOrLosingValue("sloth", -1);
        GainingOrLosingValue("god", 0, false);
    }

    public void CalculatePorbability()
    {
        Player.CC blind = player.playerCc.Find(cc => cc.ccName == "blind");
        Player.CC fury = player.playerCc.Find(cc => cc.ccName == "fury");
        Player.CC sloth = player.playerCc.Find(cc => cc.ccName == "sloth");
        if (blind != null && fury == null)
        {
            player.hitProbability = 50;
        }
        else if(blind != null && fury != null)
        {
            player.hitProbability = 10;
        }
        else if(blind == null && fury != null)
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
    public int adDamage;                // 물리 공격
    public int apDamage;                // 마법 공격
    public int fixedDamage;             // 고정 공격
    public int shield;                  // 방어력
    public int hitProbability = 100;    // 명중률
    public bool god = false;            // 무적
    public List<CC> playerCc = new List<CC>();
    [System.Serializable]
    public class CC
    {
        public string ccName;
        public int remainingTurn;
        public int damage = 0;
    }
}
