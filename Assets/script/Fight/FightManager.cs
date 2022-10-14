using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ս������ö��
public enum FightType
{
    None,
    Init,
    Player,//��һغ�
    Enemy,//���˻غ�
    Win,
    Lose
}
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;


    public FightUnit fightUnit;

    public int MaxHP;
    public int CurHP;

    //public int MaxPowerCount;//��������������ò���
    //public int CurPowerCount;//��ǰ����
    public int DefCount;//����


    public void Init()//�����ʼ����
    {
        MaxHP = 15;
        CurHP = 15;
        DefCount = 10;
    }

    private void Awake()
    {
        Instance = this;
    }

    //�л�ս������
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

    //��������߼�
    public void GetPlayHit(int hit)
    {
        //���ȿۻ���
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
                //�л�����Ϸʧ��״̬
                ChangeType(FightType.Lose);
            }
        }

        //���½���
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
