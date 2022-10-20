using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum ActionType
{
    Defend,
    Attack,
    None,
}
//���˽ű�
public class Enemy : MonoBehaviour
{
    public static Enemy Instance = new Enemy();
    protected Dictionary<string, string> data;//�������ݱ���Ϣ

    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionObj;

    //UI���
    public Transform attackTf;
    public Transform defendTf;
    public Text defText;
    public Text hpText;
    public Image hpImg;

    //��ֵ���(�ⲿ����ֵ�洢��txt��
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;
    public int Lv4BossLives = 3;
    public bool ifLv4BossConsumeLives = false;

    //������
    SkinnedMeshRenderer _meshRenderer;
    public Animator ani;

    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
    }

    void Start()
    {
        Instance = this;
        //_meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();//�����ǻ�ȡ������
        GameObject enemy = GameObject.Find("EnemyWaiting(Clone)");       
        ani = enemy.GetComponent<Animator>();//��ȡ�����ؼ�

        Debug.Log(ani);
        type = ActionType.None;

        //���ص���Ѫ�����ж�ͼ��
        hpItemObj = UIManager.Instance.CreateEnemyHpItem();
        actionObj = UIManager.Instance.CreateActionIcon();


        attackTf = actionObj.transform.Find("atk");
        defendTf = actionObj.transform.Find("def");

        defText = hpItemObj.transform.Find("EnemyDEFText").GetComponent<Text>();//�ҵ�����еķ�������ֵ
        hpText = hpItemObj.transform.Find("EnemyHPText").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("EnemyHPFill").GetComponent<Image>();//�ҵ�Ѫ��ͼ��



        //����Ѫ��λ��|���ز���Ԥ��ã�Ӧ�þͲ�����������˰�
        //hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.down*0.2f);//����������Ǹ�ʲô�ˣ�
        //actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("").position);//���������������ڵ���ģ�͵��ĸ������λ����

        //SetRandomAction();

        //��ʼ����ֵ
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);

        UpdateHp();
        UpdateDefend();


    }

    //���µ���Ѫ��
    public void UpdateHp()
    {
        if (CurHp > MaxHp)
        {
            CurHp = MaxHp;
        }

        hpText.text = CurHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurHp / (float)MaxHp;
    }

    //���µ��˷���
    public void UpdateDefend()
    {
        defText.text = Defend.ToString();
    }

    //���˱�ѡ��ʱ��ʾ���
    public void OnSelect()
    {
        _meshRenderer.material.SetColor("OtlColor", Color.red);//���������һ������ɫ����
    }

    //δѡ�е���ʱ���˵���ɫ
    public void OnUnSelect()
    {
        _meshRenderer.material.SetColor("OtlColor", Color.black);//���������һ������ɫ����
    }


    //����
    public void Hited(int val)
    {
        ani.SetBool("isHitted", true);
        Invoke("SetisHittedToFalse", 0.3f);


        //�ȿۻ���
        if (Defend >= val)
        {
            Defend -= val;

            //���˶���
            //ani.SetBool("isHitted", true);
            UpdateDefend();
        }
        else //�ٿ�Ѫ��
        {
            val = val - Defend;
            Defend = 0;
            CurHp -= val;
            if (CurHp <= 0 )
            {
                //���Ĺ�boss��������
                if (LevelManager.Instance.level == 4 && Lv4BossLives > 0)
                {
                    CurHp = 1;
                    Defend += 100;
                    Lv4BossLives -= 1;

                    UpdateHp();
                    UpdateDefend();
                    ifLv4BossConsumeLives = true;
                }
                else
                {
                    CurHp = 0;
                    //��������
                    ani.Play("die");

                    //���˴��б����Ƴ�
                    EnemyManager.Instance.DeleteEnemy(this);

                    //ɾ�����˵�ģ��
                    Destroy(gameObject, 1);
                    Destroy(actionObj);
                    Destroy(hpItemObj);
                }

            }
            else
            {
                ani.SetBool("isHitted", true);               
            }

            //ˢ��Ѫ����UI
            UpdateDefend();
            UpdateHp();
        }
        
    }

    //�����ӳٴ���
    public void SetisHittedToFalse()
    {
        ani.SetBool("isHitted", false);
    }

    //���ع���ͷ�ϵ��ж���־
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);
    }

    public IEnumerator DoAction()
    {
        
        switch (LevelManager.Instance.level)
        {

            case 0:
                yield return EnemySkill.Instance.EnemyActio0(this,type);
                break;
            case 1:
                yield return EnemySkill.Instance.EnemyActio1(this,type, CurHp);
                break;
            case 2:
                yield return EnemySkill.Instance.EnemyActio2(this, type);
                break;
            case 3:
                yield return EnemySkill.Instance.EnemyActio3(this, type);
                break;
            case 4:
                yield return EnemySkill.Instance.EnemyActio4(this, type);
                break;
        }
    }


    //����趨һ�������ж�
    public void SetRandomAction()
    {
        int ran = Random.Range(0, 2);

        type = (ActionType)ran;

        //type = ActionType.Attack;

        switch (type)
        {
/*            case ActionType.None:
                //����ͼ�겻��ʾ
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(false);
                break;*/
            case ActionType.Defend:
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(true);
                break;
            case ActionType.Attack:
                attackTf.gameObject.SetActive(true);
                defendTf.gameObject.SetActive(false);
                break;
        }
    }
}
