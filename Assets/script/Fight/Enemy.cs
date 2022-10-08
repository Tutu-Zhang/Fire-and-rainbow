using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum ActionType
{
    None,
    Defend,
    Attack,
}
//���˽ű�
public class Enemy : MonoBehaviour
{
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

    //������
    SkinnedMeshRenderer _meshRenderer;
    public Animator ani;

    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
    }

    void Start()
    {
        _meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();//�����ǻ�ȡ������
        ani = transform.GetComponent<Animator>();//���ڲ��Ŷ�����������

        type = ActionType.None;
        //���ص���Ѫ�����ж�ͼ��
        hpItemObj = UIManager.Instance.CreateHpItem();
        actionObj = UIManager.Instance.CreateActionIcon();


        attackTf = actionObj.transform.Find("atk");
        defendTf = actionObj.transform.Find("def");

        defText = actionObj.transform.Find("DefText").GetComponent<Text>();//�ҵ�����еķ�������ֵ
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

    //����趨һ�������ж�
    public void SetRandomAction()
    {
        int ran = Random.Range(0, 3);

        type = (ActionType)ran;


        switch (type)
        {
            case ActionType.None:
                //����ͼ�겻��ʾ
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(false);
                break;
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

    //���µ���Ѫ��
    public void UpdateHp()
    {
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
        //�ȿۻ���
        if(Defend >= val)
        {
            Defend -= val;

            //���˶���
            ani.Play("Hited", 0, 0);
        }
        else //�ٿ�Ѫ��
        {
            val = val - Defend;
            Defend = 0;
            CurHp -= val;
            if (CurHp <= 0 )
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
            else
            {
                ani.Play("Hited", 0, 0);
            }

            //ˢ��Ѫ����UI
            UpdateDefend();
            UpdateHp();
        }
    }

    //���ع���ͷ�ϵ��ж���־
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);
    }

    public IEnumerator DoAction()
    {
        HideAction();

        //���Ŷ�Ӧ����(���Ե�excel���н������ã�����Ĭ�ϲ��Ź�������
        ani.Play("attack");

        //�ȴ�һ��ʱ���ִ����Ϊ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��

        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:

                //�ӷ���
                Defend += 1;
                UpdateDefend();
                //���Բ��Ŷ�Ӧ��Ч
                break;
            case ActionType.Attack:

                //��ҿ�Ѫ
                FightManager.Instance.GetPlayHit(Attack);

                //���������
                Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
                break;
        }

        //�ȴ����������꣬����ʱ��Ҳ��������
        yield return new WaitForSeconds(1);

        //���Ŵ���
        ani.Play("idle");

    }
}
