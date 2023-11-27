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
    public GameObject state;
    public PlayerAnimation playerAnimation;
    public bool isUsingCard = false;
    public void DataSet()
    {
        if (player.currentHealth == 0)
        {
            player.currentHealth = player.maxHealth;
        }
        if(player.currentMana == 0)
        {
            player.currentMana = player.maxMana;
        }
        if (playerAnimation == null)
        {
            playerAnimation = FindObjectOfType<PlayerAnimation>();
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
        if(state != null)
        {
            foreach(Transform _state in state.transform)
            {
                string _name = _state.GetComponent<State>().name;
                Player.CC _cc = player.playerCc.Find(cc => cc.ccName == _name);
                if(_cc == null)
                {
                    Destroy(_state.gameObject);
                }
            }
        }
        else
        {
            state = GameObject.FindWithTag("PlayerState");
            foreach (Transform _state in state.transform)
            {
                string _name = _state.GetComponent<State>().name;
                Player.CC _cc = player.playerCc.Find(cc => cc.ccName == _name);
                if (_cc == null)
                {
                    Destroy(_state.gameObject);
                }
            }
        }
    }

    public void CalculatingDamage()         // 공격력 계산 함수 
    {
        player.adDamage = player.adPower * player.baPower;
        player.apDamage = player.apPower * player.baPower;
        player.fixedDamage = player.fixedPower;
    }

    public void GainingOrLosingValue(string value, int amount = 0, bool overHealing = false, int ccDamage = 2)
    {       // (바꿀 값, 바꿀 양, 과다치유 Or 상태이상 추가 제거, 상태이상 공격력)
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
                    if(player.shield > 0)
                    {
                        if(player.shield >= -amount)
                        {
                            player.shield += amount;
                            playerAnimation.PlayAnimation(PlayerAnimation.Type.HIT);
                        }
                        else
                        {
                            int _shield = player.shield += amount;
                            player.shield = 0;
                            player.currentHealth += _shield;
                            playerAnimation.PlayAnimation(PlayerAnimation.Type.BREAK, true);
                        }
                    }
                    else
                    {
                        player.currentHealth += amount;
                        playerAnimation.PlayAnimation(PlayerAnimation.Type.HIT);
                    }
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
                    playerAnimation.PlayAnimation(PlayerAnimation.Type.BREAK);
                    break;
                case ("poison"):
                    Player.CC poison = player.playerCc.Find(cc => cc.ccName == "poison");
                    if (poison != null)
                    {
                        player.currentHealth -= poison.damage;
                        poison.remainingTurn += amount;
                        if (poison.remainingTurn <= 0)
                        {
                            player.playerCc.Remove(poison);
                        }
                    }
                    break;
                case ("temporary"):
                    Player.CC temporary = player.playerCc.Find(cc => cc.ccName == "temporary");
                    if (temporary != null)
                    {
                        player.currentHealth -= temporary.damage / 2;
                        temporary.remainingTurn += amount;
                        if (temporary.remainingTurn <= 0)
                        {
                            player.playerCc.Remove(temporary);
                        }
                    }
                    break;
                case ("blind"):
                    Player.CC blind = player.playerCc.Find(cc => cc.ccName == "blind");
                    if (blind != null)
                    {
                        blind.remainingTurn += amount;
                        if (blind.remainingTurn <= 0)
                        {
                            player.playerCc.Remove(blind);
                        }
                    }
                    break;
                case ("fury"):
                    Player.CC fury = player.playerCc.Find(cc => cc.ccName == "fury");
                    if (fury != null)
                    {
                        fury.remainingTurn += amount;
                        if (fury.remainingTurn <= 0)
                        {
                            player.adPower -= 3;
                            player.playerCc.Remove(fury);
                        }
                    }
                    break;
                case ("sloth"):
                    Player.CC sloth = player.playerCc.Find(cc => cc.ccName == "sloth");
                    if (sloth != null)
                    {
                        sloth.remainingTurn += amount;
                        if (sloth.remainingTurn <= 0)
                        {
                            player.playerCc.Remove(sloth);
                        }
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
                        playerAnimation.PlayAnimation(PlayerAnimation.Type.GAIN, true);
                    }
                    else
                    {
                        player.currentHealth += amount;
                        playerAnimation.PlayAnimation(PlayerAnimation.Type.HEAL);
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
                        if(player.shield > 0)
                        {
                            playerAnimation.PlayAnimation(PlayerAnimation.Type.BREAK);
                        }
                        player.shield = 0;
                    }
                    else
                    {
                        player.shield += amount;
                        playerAnimation.PlayAnimation(PlayerAnimation.Type.GAIN);
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
                        GameObject _blind = Instantiate(Resources.Load<GameObject>("Prefabs/State"));
                        _blind.transform.parent = state.transform;
                        _blind.transform.localScale = Vector3.one;
                        _blind.GetComponent<State>().LoadImage(value);
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
                        player.adPower += 3;
                        GameObject _fury = Instantiate(Resources.Load<GameObject>("Prefabs/State"));
                        _fury.transform.parent = state.transform;
                        _fury.transform.localScale = Vector3.one;
                        _fury.GetComponent<State>().LoadImage(value);
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
                        GameObject _sloth = Instantiate(Resources.Load<GameObject>("Prefabs/State"));
                        _sloth.transform.parent = state.transform;
                        _sloth.transform.localScale = Vector3.one;
                        _sloth.GetComponent<State>().LoadImage(value);
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
                        GameObject _poison = Instantiate(Resources.Load<GameObject>("Prefabs/State"));
                        _poison.transform.parent = state.transform;
                        _poison.transform.localScale = Vector3.one;
                        _poison.GetComponent<State>().LoadImage(value);
                    }
                    else
                    {
                        poison.remainingTurn += amount/2;
                        if(poison.damage < ccDamage)
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
                        GameObject _temporary = Instantiate(Resources.Load<GameObject>("Prefabs/State"));
                        _temporary.transform.parent = state.transform;
                        _temporary.transform.localScale = Vector3.one;
                        _temporary.GetComponent<State>().LoadImage(value);
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
        CalculatePorbability();
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
        GainingOrLosingValue("currentMana", 0, true);
        GainingOrLosingValue("poison", -1);
        GainingOrLosingValue("temporary", -1);
        GainingOrLosingValue("blind", -1);
        GainingOrLosingValue("fury", -1);
        GainingOrLosingValue("sloth", -1);
        GainingOrLosingValue("god", 0, false);
        GainingOrLosingValue("shield", 0, true);
    }

    public void CalculatePorbability()
    {
        Player.CC blind = player.playerCc.Find(cc => cc.ccName == "blind");
        Player.CC fury = player.playerCc.Find(cc => cc.ccName == "fury");
        Player.CC sloth = player.playerCc.Find(cc => cc.ccName == "sloth");


        if (sloth != null)      // 나태 상태
        {
            player.hitProbability = 0;
        }
        else if (blind == null && fury != null)      // 폭주 상태
        {
            player.hitProbability = 75;
        }
        else if (blind != null && fury == null)     // 실명 상태
        {
            player.hitProbability = 50;
        }
        else if(blind != null && fury != null)      // 실명과 폭주 상태
        {
            player.hitProbability = 10;
        }
        else if(sloth == null && fury == null && blind == null)     // 아무런 상태 이상도 없는 상태
        {
            player.hitProbability = 100;
        } 
    }
    public void UsingDelay(float time)
    {
        if (isUsingCard == true)
        {
            Invoke("UsingCard", time);
        }
        else
        {
            UsingCard();
        }
    }
    public bool UsingCard()
    {
        isUsingCard = !isUsingCard;
        return isUsingCard;
    }
    public void PlayerDie()
    {
        GameManager.Instance.MoveScene("Test End Scene");
    }

    public void PlayerWin()
    {
        Managers.Stage.SelectLevel();
        GameManager.Instance.MoveScene("Test End Scene");
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
