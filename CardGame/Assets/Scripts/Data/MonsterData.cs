using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Util;

public class MonsterData : GenericSingleton<MonsterData>
{
    public int monsterHp;
    public int monsterMaxHp;
    public int monsterAd;
    public int monsterAp;
    public string monsterName;
    public Entity_MonsteraData.Param monsterData;
    public List<Entity_PatternData.Param> patterns = new List<Entity_PatternData.Param>();
    public Entity_PatternData.Param pattern;
    public List<MonsterCC> monsterCc = new List<MonsterCC>();
    public SpriteRenderer bg;
    public Transform monster;
    public GameObject hpText, patternText, patternStates, state;
    public Dictionary<string, GameObject> patternState = new Dictionary<string, GameObject>();

    public MonsterAnimation monsterAnim;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            GetDamage(10);
        }
    }
    public void LoadMonsterData(int level, int stageType, int strongDegree = 1, bool isBoss = false)
    {
        // ���� �ҷ�����
        monsterData = null;
        patterns.Clear();
        List<Entity_MonsteraData.Param> monsters = new List<Entity_MonsteraData.Param>();
        foreach(Entity_MonsteraData.Param _monsterData in Managers.Data.monsterDatabase.param) // ���� ��ũ���ͺ� ������ �ִ� �����͵��� �����´�.
        {
            string id = _monsterData.id;
            char no3 = id[2];
            char no4 = id[3];
            char _level = (char)(level + '0');
            char _stageType = (char)(stageType + '0');
            char last = id[id.Length - 1];

            if (no3 == _level && no4 == _stageType && last == 'Z' && isBoss == true)
            {
                    monsters.Add(_monsterData);
            }
            else if(no3 == _level && no4 == _stageType&& last != 'Z' && isBoss == false)
            {
                    monsters.Add(_monsterData);
            }
            Debug.Log(_monsterData.id);
        }
        for(int i  = 0; i < monsters.Count; i ++)
        {
            string id = monsters[i].id;
            if(id == "501102A" || id == "501102C")
            {
                monsters.Remove(monsters[i]);
                Debug.Log(monsters.Count);
            }
        }
        if (monsters.Count > 0)
        {

            int monsterNum = UnityEngine.Random.Range(0, monsters.Count);
            monsterData = monsters[monsterNum];
        }
        else
        {
            Debug.Log("�� ����!");
        }

        // ���� ���� �Է��ϱ�
        if(strongDegree == 1)
        {
            monsterMaxHp = monsterData.baseHp;
            monsterAd = monsterData.baseAd;
            monsterAp = monsterData.baseAp;
        }
        else if(strongDegree == 0)
        {
            Debug.Log("0�� �Ұ���!");
        }
        else
        {
            int strong = monsterData.statPlus + strongDegree;
            monsterMaxHp = monsterData.baseHp + strong * 5;
            monsterAd = monsterData.baseAd + strong;
            monsterAp = monsterData.baseAp + strong;
        }
        monsterName = monsterData.monsterName;
        monsterHp = monsterMaxHp;



        // ���� ���� �ҷ�����
        foreach (Entity_PatternData.Param _patternData in Managers.Data.patternDatabase.param)
        {
            if(_patternData.monsterId == monsterData.id)
            {
                patterns.Add(_patternData);
            }
        }
        string stage = monsterData.id.Substring(2,4);
        Debug.Log(stage);
        SpriteRenderer _bg = GameObject.FindGameObjectWithTag("BackGround").GetComponent<SpriteRenderer>();
        if (_bg != null)
        {
            bg = _bg;
            bg.sprite = Resources.Load<Sprite>($"Illustration/BG/{stage}");
            bg.transform.localScale = new Vector3(0.416f, 0.416f, 0.0f);
        }

        if(monsterAnim == null)
        {
            GameObject _monster = Resources.Load<GameObject>($"Prefabs/Monster/{monsterData.id}");
            GameObject monster = Instantiate(_monster);
            if(_monster == null)
            {
                monster = Instantiate(Resources.Load<GameObject>("Prefabs/Monster/501101A"), new Vector3(0.0f,-3.0f,0.0f), Quaternion.identity);
            }
            monster.transform.parent = bg.transform.GetChild(0);
            monster.transform.localPosition = new Vector3 (0,0,0);
            monsterAnim = GetOrAddComponent<MonsterAnimation>(monster.gameObject);
            monsterAnim.thisMonster = monsterData;
            monsterAnim.FindMyEyes();
        }
        else
        {
            monsterAnim.FindMyEyes();
        }

        if (hpText == null)
        {
            GameObject _hpText = GameObject.Find("MonsterHpText");
            hpText = _hpText;
        }
        if(patternText == null)
        {
            GameObject _patternText = GameObject.Find("PatternText");
            patternText = _patternText;
        }
        if(patternStates == null)
        {
            GameObject _patternStates = GameObject.Find("PatternState");
            patternStates = _patternStates;

            foreach(Transform pattern in patternStates.transform)
            {
                string _name = pattern.name;
                patternState.Add(_name, pattern.gameObject);
            }
        }
        if(state == null)
        {
            state = GameObject.Find("MonsterName");
        }
        hpText.GetComponent<TMP_Text>().text = $"HP : {monsterHp}";
        state.GetComponent<TMP_Text>().text = monsterName;
        PickPattern();
    }

    public void GetDamage(int amount)
    {
        monsterHp -= amount;
        hpText.GetComponent<TMP_Text>().text = $"HP : {monsterHp}";
        if(monsterHp <= 0)
        {
            PlayerData.Instance.PlayerWin();
        }
    }
    public void UsePattern()
    {
        if(pattern != null)
        {
            int type = pattern.type;
            int ad = pattern.ad;
            int ap = pattern.ap;
            int heal = pattern.heal;

            if (type == 1)
            {
                PlayerData.Instance.GainingOrLosingValue("currentHealth", -(ad + monsterAd));
            }
            else if (type == 2)
            {
                if (ad != 0)
                {
                    monsterAd += ad;
                }
                else if (ap != 0)
                {
                    monsterAp += ap;
                }
                else if (heal != 0)
                {
                    monsterHp += heal;
                }
            }
            else if(type == 3)
            {
                Debug.Log("���߿� ���� ����");
            }
        }
    }
    public void PickPattern(string id = null)
    {
        Entity_PatternData.Param nextPattern = new Entity_PatternData.Param();
        if (id != null)
        {
            nextPattern = null;
            
            for (int i = 0;i < patterns.Count; i++)
            {
                if(patterns[i].patternId == id)
                {
                    nextPattern = patterns[i];
                }
            }
            if(nextPattern == null)
            {
                    Debug.Log($"{id}�� ������ �������� �ʽ��ϴ�.");
                    int A = UnityEngine.Random.Range(0, patterns.Count);
                    nextPattern = patterns[A];
                    pattern = nextPattern;
            }
        }
        else
        {
            int A = UnityEngine.Random.Range(0, patterns.Count);
            nextPattern = patterns[A];
            pattern = nextPattern;
        }
        patternState["Attack"].SetActive(false);
        patternState["Heal"].SetActive(false);
        patternState["Defensive"].SetActive(false);
        if (pattern.type == 1)
        {
            patternState["Attack"].SetActive(true);
            patternText.GetComponent<TMP_Text>().text = $"{pattern.ad + monsterAd}";
        }
        else if(pattern.type == 2)
        {
            patternState["Heal"].SetActive(true);
            patternText.GetComponent<TMP_Text>().text = $"��ȭ!";
        }
        else
        {
            patternState["Defensive"].SetActive(true);
            patternText.GetComponent<TMP_Text>().text = $"�� �� �޽�!";
        }
    }

    public void GainingOrLosingCC(string ccName, int turn, int dmg, bool isAdd)
    {
        if(isAdd == true)
        {
            switch(ccName)
            {
                case ("blind"):
                    MonsterCC blind = monsterCc.Find(cc => cc.ccName == "blind");
                    if (blind == null)
                    {
                        MonsterCC newCc = new MonsterCC
                        {
                            ccName = "blind",
                            remainingTurn = turn,
                            damage = 0
                        };
                        monsterCc.Add(newCc);
                    }
                    else
                    {
                        blind.remainingTurn += turn;
                    }
                    break;
                case ("fury"):
                    MonsterCC fury = monsterCc.Find(cc => cc.ccName == "fury");
                    if (fury == null)
                    {
                        MonsterCC newCc = new MonsterCC
                        {
                            ccName = "blind",
                            remainingTurn = turn,
                            damage = 0
                        };
                        monsterCc.Add(newCc);
                    }
                    else
                    {
                        fury.remainingTurn += turn;
                    }
                    break;
                case ("sloth"):
                    MonsterCC sloth = monsterCc.Find(cc => cc.ccName == "sloth");
                    if (sloth == null)
                    {
                        MonsterCC newCc = new MonsterCC
                        {
                            ccName = "sloth",
                            remainingTurn = turn,
                            damage = 0
                        };
                        monsterCc.Add(newCc);
                    }
                    else
                    {
                        sloth.remainingTurn += turn;
                    }
                    break;
                case ("poison"):
                    MonsterCC poison = monsterCc.Find(cc => cc.ccName == "poison");
                    if (poison == null)
                    {
                        MonsterCC newCc = new MonsterCC
                        {
                            ccName = "sloth",
                            remainingTurn = turn,
                            damage = dmg
                        };
                        monsterCc.Add(newCc);
                    }
                    else
                    {
                        poison.remainingTurn += turn/2;
                        if(poison.damage < dmg)
                        {
                            poison.damage = dmg;
                        }
                    }
                    break;
            }
        }
        else if(isAdd == false)
        {
            switch (ccName)
            {
                case ("blind"):
                    MonsterCC blind = monsterCc.Find(cc => cc.ccName == "blind");
                    if (blind != null)
                    {
                        blind.remainingTurn -= turn;
                        if(blind.remainingTurn <= 0)
                        {
                            monsterCc.Remove(blind);
                        }
                    }
                    break;
                case ("fury"):
                    MonsterCC fury = monsterCc.Find(cc => cc.ccName == "fury");
                    if (fury != null)
                    {
                        fury.remainingTurn -= turn;
                        if (fury.remainingTurn <= 0)
                        {
                            monsterCc.Remove(fury);
                        }
                    }
                    break;
                case ("sloth"):
                    MonsterCC sloth = monsterCc.Find(cc => cc.ccName == "sloth");
                    if (sloth != null)
                    {
                        sloth.remainingTurn -= turn;
                        if (sloth.remainingTurn <= 0)
                        {
                            monsterCc.Remove(sloth);
                        }
                    }
                    break;
                case ("poison"):
                    MonsterCC poison = monsterCc.Find(cc => cc.ccName == "poison");
                    if (poison != null)
                    {
                        monsterHp -= poison.damage;
                        monsterAnim.GetDamaged();
                        poison.remainingTurn -= turn;
                        if (poison.damage < dmg)
                        {
                            poison.damage = dmg;
                        }
                        if (poison.remainingTurn <= 0)
                        {
                            monsterCc.Remove(poison);
                        }
                    }
                    break;
            }
        }
    }
    public void MonsterCCChange()
    {
        GainingOrLosingCC("blind", 1, 0,false);
        GainingOrLosingCC("fury", 1, 0, false);
        GainingOrLosingCC("sloth", 1, 0, false);
        GainingOrLosingCC("poison", 1, 0, false);
    }
    [System.Serializable]
    public class MonsterCC
    {
        public string ccName;
        public int remainingTurn;
        public int damage = 0;
    }
}
