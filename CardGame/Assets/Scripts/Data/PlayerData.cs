using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : GenericSingleton<PlayerData>
{
    public Player player = new Player();

    private void Awake()
    {
        player.currentHealth = player.maxHealth;
        player.currentMana = player.maxMana;
    }


}
[System.Serializable]
public class Player
{
    public int adPower = 1;             // 물리 공격력
    public int apPower = 1;             // 마법 공격력
    public int baPower = 1;             // 기본 공격력
    public int plusPower = 0;           // 추가 공격력
    public int maxHealth = 30;          // 최대 체력
    public int currentHealth;           // 현재 체력
    public int maxMana = 3;             // 최대 마나
    public int currentMana;             // 현재 마나

    public int adDamage()               //물리 데미지
    {
        int _adDamage = this.adPower * this.baPower + this.plusPower;
        return _adDamage;
    }
    public int apDamage()               //마법 데미지
    {
        int _apDamage = this.apPower * this.baPower + this.plusPower;
        return _apDamage;
    }

}
