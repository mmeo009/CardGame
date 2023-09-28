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
    public int adPower = 1;             // ���� ���ݷ�
    public int apPower = 1;             // ���� ���ݷ�
    public int baPower = 1;             // �⺻ ���ݷ�
    public int plusPower = 0;           // �߰� ���ݷ�
    public int maxHealth = 30;          // �ִ� ü��
    public int currentHealth;           // ���� ü��
    public int maxMana = 3;             // �ִ� ����
    public int currentMana;             // ���� ����

    public int adDamage()               //���� ������
    {
        int _adDamage = this.adPower * this.baPower + this.plusPower;
        return _adDamage;
    }
    public int apDamage()               //���� ������
    {
        int _apDamage = this.apPower * this.baPower + this.plusPower;
        return _apDamage;
    }

}
