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
//敌人脚本
public class Enemy : MonoBehaviour
{
    protected Dictionary<string, string> data;//敌人数据表信息

    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionObj;

    //UI相关
    public Transform attackTf;
    public Transform defendTf;
    public Text defText;
    public Text hpText;
    public Image hpImg;

    //数值相关(这部分数值存储在txt中
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    //组件相关
    SkinnedMeshRenderer _meshRenderer;
    public Animator ani;

    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
    }

    void Start()
    {
        _meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();//好像是获取轮廓？
        ani = transform.GetComponent<Animator>();//用于播放动画，如受伤

        type = ActionType.None;
        //加载敌人血条和行动图标
        hpItemObj = UIManager.Instance.CreateHpItem();
        actionObj = UIManager.Instance.CreateActionIcon();


        attackTf = actionObj.transform.Find("atk");
        defendTf = actionObj.transform.Find("def");

        defText = actionObj.transform.Find("DefText").GetComponent<Text>();//找到组件中的防御力数值
        hpText = hpItemObj.transform.Find("EnemyHPText").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("EnemyHPFill").GetComponent<Image>();//找到血条图标



        //设置血条位置|在素材里预设好，应该就不用这个步骤了吧
        //hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.down*0.2f);//这里加向量是干什么了？
        //actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("").position);//“”里面代表放置在敌人模型的哪个组件的位置上

        //SetRandomAction();

        //初始化数值
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);

        UpdateHp();
        UpdateDefend();


    }

    //随机设定一个敌人行动
    public void SetRandomAction()
    {
        int ran = Random.Range(0, 3);

        type = (ActionType)ran;


        switch (type)
        {
            case ActionType.None:
                //设置图标不显示
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

    //更新敌人血量
    public void UpdateHp()
    {
        hpText.text = CurHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurHp / (float)MaxHp;
    }

    //更新敌人防御
    public void UpdateDefend()
    {
        defText.text = Defend.ToString();
    }

    //敌人被选中时显示红边
    public void OnSelect()
    {
        _meshRenderer.material.SetColor("OtlColor", Color.red);//这里好像是一个子着色器？
    }

    //未选中敌人时敌人的颜色
    public void OnUnSelect()
    {
        _meshRenderer.material.SetColor("OtlColor", Color.black);//这里好像是一个子着色器？
    }


    //受伤
    public void Hited(int val)
    {
        //先扣护盾
        if(Defend >= val)
        {
            Defend -= val;

            //受伤动画
            ani.Play("Hited", 0, 0);
        }
        else //再扣血量
        {
            val = val - Defend;
            Defend = 0;
            CurHp -= val;
            if (CurHp <= 0 )
            {
                CurHp = 0;
                //播放死亡
                ani.Play("die");

                //敌人从列表中移除
                EnemyManager.Instance.DeleteEnemy(this);

                //删除敌人的模型
                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
            }
            else
            {
                ani.Play("Hited", 0, 0);
            }

            //刷新血量等UI
            UpdateDefend();
            UpdateHp();
        }
    }

    //隐藏怪物头上的行动标志
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);
    }

    public IEnumerator DoAction()
    {
        HideAction();

        //播放对应动画(可以到excel表中进行配置，这里默认播放攻击动画
        ani.Play("attack");

        //等待一段时间后执行行为
        yield return new WaitForSeconds(0.5f);//等待0.5秒

        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:

                //加防御
                Defend += 1;
                UpdateDefend();
                //可以播放对应特效
                break;
            case ActionType.Attack:

                //玩家扣血
                FightManager.Instance.GetPlayHit(Attack);

                //摄像机抖动
                Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
                break;
        }

        //等待动画播放完，这里时间也可以配置
        yield return new WaitForSeconds(1);

        //播放待机
        ani.Play("idle");

    }
}
