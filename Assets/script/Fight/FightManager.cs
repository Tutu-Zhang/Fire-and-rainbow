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

    public int MaxHP;
    public int CurHP;

    //public int MaxPowerCount;//最大能量（好像用不到
    //public int CurPowerCount;//当前能量
    public int DefCount;//护甲


    public void Init()//玩家起始数据
    {
        MaxHP = 15;
        CurHP = 15;
        DefCount = 10;
    }

    private void Awake()
    {
        Instance = this;
    }

    //切换战斗类型
    public void ChangeType(FightType type)
    {
        switch (type)
        {
            case FightType.None:
                break;
            case FightType.Init:
                fightUnit = new FightInit();
                break;
            case FightType.Player:
                fightUnit = new FightPlayerTurn();
                break;
            case FightType.Enemy:
                fightUnit = new FightEnemyTurn();
                break;
            case FightType.Win:
                fightUnit = new FightWin();
                break;
            case FightType.Lose:
                fightUnit = new FightLose();
                break;
        }
        fightUnit.Init();
    }

    //玩家受伤逻辑
    public void GetPlayHit(int hit)
    {
        //优先扣护盾
        if (DefCount >= hit)
        {
            DefCount -= hit;
        }
        else
        {
            hit = hit - DefCount;
            DefCount = 0;
            CurHP -= hit;
            if (CurHP <= 0)
            {
                CurHP = 0;
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
        CurHP += recover;
        if (CurHP > MaxHP)
            CurHP = MaxHP;

        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateDef();
    }

    public void Attack_Enemy(int val)
    {
        EnemyManager.Instance.GetEnemy(0).Hited(val);
    }

    private void Update()
    {
        if (fightUnit != null)
        {
            fightUnit.OnUpdate();
        }
    }
}
