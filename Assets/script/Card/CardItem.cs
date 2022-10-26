using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{
    public Dictionary<string, string> data;//卡牌信息
    int num;
    private bool IsInPlayArea = false;
    protected int cardNum;

    public int GetCardNum()
    {
        return cardNum;
    }

    public void Init(Dictionary<string, string> data,int index)
    {
        this.data = data;
        this.num = index;
    }

    private int index;

    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.1f, 0.2f);//放大
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();

    }
    //鼠标移出
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.2f);
        transform.SetSiblingIndex(index);


    }
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(1.2f, 0.25f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        if (!IsInPlayArea && FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            if(UIManager.Instance.GetUI<FightUI>("fightBackground").MoveCardToPlayArea(this))
                IsInPlayArea = true;
        }
        else if(IsInPlayArea && FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            UIManager.Instance.GetUI<FightUI>("fightBackground").MoveCardToHandArea(this);
            IsInPlayArea = false;
        }
    }

    public void SetPlayArea(bool temp)
    {
        IsInPlayArea = temp;
    }

    private void Start()
    {
        //Id	Name	Script	Type	Des	BgIcon	Icon	Expend	Arg0	Effects
        //唯一的标识（不能重复）	名称	卡牌添加的脚本	卡牌类型的Id	描述	卡牌的背景图资源路径	图标资源的路径	消耗的费用	属性值	特效
        //1000	普通攻击	AttackCardItem	10001	对单个敌人进行{0}点的伤害	Icon/BlueCard	Icon/sword_03e	1	3	Effects/GreenBloodExplosion
        //Debug.Log("本次抽牌第"+ num +"张牌的类型为"+data["BgIcon"]);
        //Transform cardArea = GameObject.Find("CardArea").transform;
        this.GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
    }


    //创建卡牌使用后特效
    public void PlayEffect(Vector3 pos)
    {
        GameObject effectobj = Instantiate(Resources.Load(data["Effects"])) as GameObject;
        effectobj.transform.position = pos;
        Destroy(effectobj, 2);
    }

}
