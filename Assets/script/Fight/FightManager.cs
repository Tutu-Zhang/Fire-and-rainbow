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

    GameObject turnBtn;

    public int MaxHP;
    public int CurHP;

    public int turnCount = 0;

    //public int MaxPowerCount;//��������������ò���
    //public int CurPowerCount;//��ǰ����
    public int DefCount;//����

    public int TurnCount;//�غ���,����غ�����ÿ����һغϼ�һ�ε�



    private void Start()
    {
        turnBtn = GameObject.Find("turnBtn");
    }

    public void Init()//�����ʼ����
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

    //�л�ս������
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
                fightUnit = obj.AddComponent < FightWin>();
                break;
            case FightType.Lose:
                fightUnit = obj.AddComponent < FightLose>();
                break;
        }
        fightUnit.Init();
        if(type == FightType.Player)
        {
            UIManager.Instance.GetUI<FightUI>("fightBackground").BuffPassTurn();
            TurnCount++;
        }
    }

    public int GetTurnCount()
    {
        return TurnCount;
    }

    //��������߼�
    public void GetPlayHit(int hit)
    {
        int this_hit = hit;

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("0111") != null)
        {
            this_hit = BuffEffects.buff_defend_0111(this_hit);
        }

        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("1101") != null)
        {
            BuffEffects.buff_counter_1101(this_hit);
        }

        //���ȿۻ���
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
        
        int gamble = 1;
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("1111") != null)
        {
            gamble = BuffEffects.buff_gamble_1111();
        }

        if (gamble != 0)
        {
            EnemyManager.Instance.GetEnemy(0).Hited(val * gamble);
        }
        else
        {
            GetPlayHit(val); //���������1111buffʱ�ж�ʧ���˺�ת�Ƶ��Լ������������Ȼ��Ϊ�з���ɵ��˺�������ʧ����������1101��0111 buff��Ȼ����Ч��
                             //Ҳ����˵��������ͬʱ��1111��1101������buff����ô�ж��ɹ��˾��ǵз��ܵ������˺���ʧ���˾��ǵз����Ҷ��ܵ�һ���˺�
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
