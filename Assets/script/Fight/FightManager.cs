using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗类型枚举
public enum FightType
{
    None,
    Init,
    Player,//玩家回合
    Enemy,//敌人回合
    Win,
    Lose
}
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public FightUnit fightUnit;

    GameObject turnBtn;

    public int MaxHP;
    public int CurHP;


    //public int MaxPowerCount;//最大能量（好像用不到
    //public int CurPowerCount;//当前能量
    public int DefCount;//护甲

    public int TurnCount;//回合数,这个回合数是每到玩家回合加一次的



    private void Start()
    {
        turnBtn = GameObject.Find("turnBtn");
    }

    public void Init()//玩家起始数据
    {
        MaxHP = 15;
        CurHP = 15;
        DefCount = 10;
        TurnCount = 0;

    }

    private void Awake()
    {
        Instance = this;
    }

    //切换战斗类型
    public void ChangeType(FightType type)
    {
        GameObject obj = new GameObject(type.ToString());
        obj.transform.parent = GameObject.Find("TurnSets").transform;
        switch (type)
        {
            case FightType.None:
                break;
            case FightType.Init:
                fightUnit = obj.AddComponent<FightInit>();
                break;
            case FightType.Player:
                fightUnit = obj.AddComponent<FightPlayerTurn>();
                break;
            case FightType.Enemy:
                fightUnit = obj.AddComponent<FightEnemyTurn>();
                break;
            case FightType.Win:
                fightUnit = obj.AddComponent <FightWin>();
                break;
            case FightType.Lose:
                fightUnit = obj.AddComponent <FightLose>();
                break;
        }
        fightUnit.Init();
        if(type == FightType.Player)
        {

            TurnCount++;
        }
    }

    public int GetTurnCount()
    {
        return TurnCount;
    }

    //玩家受伤逻辑
    public void GetPlayHit(int hit)
    {
        int this_hit = hit + LevelManager.Instance.AttackFix;

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("0111") != null)
        {
            this_hit = BuffEffects.buff_defend_0111(this_hit);
        }

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("1101") != null)
        {
            BuffEffects.buff_counter_1101(this_hit);
        }

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("1110") != null)
        {
            if (BuffEffects.buff_keepAlive_1110(this_hit)){
                this_hit = 0;

                UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("1110").SetLeftTime(0);
            }
            
        }

        //优先扣护盾
        if (DefCount >= this_hit)
        {
            DefCount -= this_hit;
        }
        else
        {
            this_hit = this_hit - DefCount;
            DefCount = 0;
            CurHP -= this_hit;
            if (CurHP <= 0)
            {
                CurHP = 0;
                //删除敌人模型
                GameObject enemyModel = GameObject.FindGameObjectWithTag("Enemy");  
                Debug.Log("找到敌人模型");
                Destroy(enemyModel);
                
                //切换到游戏失败状态
                ChangeType(FightType.Lose);
            }
        }

        //更新界面
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateDef();
    }

    public void GetRecover(int recover)
    {
        AudioManager.Instance.PlayEffect("回复");
        CurHP += recover;
        if (CurHP > MaxHP)
            CurHP = MaxHP;

        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateDef();
    }

    public void GetDefendRecover(int recover)
    {
        AudioManager.Instance.PlayEffect("护甲");
        DefCount += recover;

        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateDef();
    }

    public void Attack_Enemy(int val)
    {
        AudioManager.Instance.PlayEffect("玩家攻击");
        ////专判1111buff   
        Debug.Log("AttackEnemy执行");
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("1111") != null)
        {
            if (BuffEffects.buff_gamble_1111())
            {
                EnemyManager.Instance.GetEnemy(0).Hited(val * 2);
                Debug.Log("AttackEnemy伤害翻倍");
            }
            else
            {
                EnemyManager.Instance.GetEnemy(0).Hited(val);
                Debug.Log("AttackEnemy吸血伤害");
                GetRecover(val);
            }
        }
        else
        {
            EnemyManager.Instance.GetEnemy(0).Hited(val);
        }
    }

    public void SetBtn(bool option)
    {
        turnBtn.SetActive(option);
    }

    private void Update()
    {
        if (fightUnit != null)
        {
            fightUnit.OnUpdate();
        }
    }
}
