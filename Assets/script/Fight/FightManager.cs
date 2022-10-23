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

    //��������߼�
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
                //ɾ������ģ��
                GameObject enemyModel = GameObject.FindGameObjectWithTag("Enemy");  
                Debug.Log("�ҵ�����ģ��");
                Destroy(enemyModel);
                
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
        AudioManager.Instance.PlayEffect("�ظ�");
        CurHP += recover;
        if (CurHP > MaxHP)
            CurHP = MaxHP;

        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateDef();
    }

    public void GetDefendRecover(int recover)
    {
        AudioManager.Instance.PlayEffect("����");
        DefCount += recover;

        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateDef();
    }

    public void Attack_Enemy(int val)
    {
        AudioManager.Instance.PlayEffect("��ҹ���");
        ////ר��1111buff   
        Debug.Log("AttackEnemyִ��");
        if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("1111") != null)
        {
            if (BuffEffects.buff_gamble_1111())
            {
                EnemyManager.Instance.GetEnemy(0).Hited(val * 2);
                Debug.Log("AttackEnemy�˺�����");
            }
            else
            {
                EnemyManager.Instance.GetEnemy(0).Hited(val);
                Debug.Log("AttackEnemy��Ѫ�˺�");
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
