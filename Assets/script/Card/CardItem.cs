using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Dictionary<string, string> data;//卡牌信息
    int num;

    public void Init(Dictionary<string, string> data,int index)
    {
        this.data = data;
        this.num = index+3;
    }

    private int index;

    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.5f, 0.25f);//放大
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();

        transform.Find("bg").GetComponent<Image>().material.SetColor("_LineColor",Color.yellow);//_LineColor是个什么东西
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_LineWidth", 10);
    }
    //鼠标移出
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.25f);
        transform.SetSiblingIndex(index);
        transform.Find("bg").GetComponent<Image>().material.SetColor("_LineColor", Color.black);//这是个什么东西
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_LineWidth", 1);

    }

    private void Start()
    {
        //Id	Name	Script	Type	Des	BgIcon	Icon	Expend	Arg0	Effects
        //唯一的标识（不能重复）	名称	卡牌添加的脚本	卡牌类型的Id	描述	卡牌的背景图资源路径	图标资源的路径	消耗的费用	属性值	特效
        //1000	普通攻击	AttackCardItem	10001	对单个敌人进行{0}点的伤害	Icon/BlueCard	Icon/sword_03e	1	3	Effects/GreenBloodExplosion
        Debug.Log(data["BgIcon"]);

        GameObject fightBackground = GameObject.Find("fightBackground");
        fightBackground.transform.GetChild (num).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
        //transform.Find("cardBackground(Clone)").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
        //transform.Find("卡牌模型/描述").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Des"]);
        //transform.Find("卡牌模型/名称").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Name"]);
        //transform.Find("卡牌模型/花费").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Extend"]);
        //以此类推，通过这种方式加载一张卡牌

        //transform.Find("卡牌模型/类型").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];


        //设置bg背景img的外框材质
        //transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));//选中后外框会有一个颜色的效果

    }


    //尝试使用卡牌
    public virtual bool TryUse()
    {
        //卡牌需要的费用
        //int cost = int.Parse(data["Extend"]);

/*        if (cost>FightManager.Instance.CurPowerCount)
        {
            //费用不足
            AudioManager.Instance.PlayEffect("Effect/loss");//使用失败

            //提示
            UIManager.Instance.ShowTip("费用不足",Color.red);

            return false;
        }
        else
        {*/
            //FightManager.Instance.CurPowerCount -= cost;
            //刷新费用数据
            //UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();

            //删除使用过的卡牌
            UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);

            return true;
        //}
    }
    //创建卡牌使用后特效
    public void PlayEffect(Vector3 pos)
    {
        GameObject effectobj = Instantiate(Resources.Load(data["Effects"])) as GameObject;
        effectobj.transform.position = pos;
        Destroy(effectobj, 2);
    }

    Vector2 initPos;//拖拽开始时记录卡牌位置

    //开始拖拽
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;

        //播放声音
        AudioManager.Instance.PlayEffect("文件路径");
    }

    //拖拽中
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }

    //结束拖拽
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos;
        transform.SetSiblingIndex(index);
    }
}
