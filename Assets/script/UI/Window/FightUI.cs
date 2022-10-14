using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FightUI : UIBase
{
    //private Text cardCountText;//卡牌数量
    //private Text noCardCountText;//弃牌堆数量
    //private Text powerText;
    private Text hpText;
    private Image hpImage;
    private Text defText;
    private GameObject playerHPBar;

    private GameObject playCardZone;
    private GameObject cardZone;
    private GameObject cardArea;

    private List<CardItem> cardItemList; //手牌区集合
    private List<CardItem> PlayCardList; //出牌区集合

    Button turnBtn, UseBtn;
    private void Awake()
    {
        cardItemList = new List<CardItem>();
        PlayCardList = new List<CardItem>();
    }

    //玩家回合结束，切换到敌人回合
    private void onChangeTurnBtn()
    {
        //只有玩家才能切换
        if (FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            RemoveAllCards(true);
            FightManager.Instance.ChangeType(FightType.Enemy);
        }
        Debug.Log("回合切换");
    }

    private void UseCard()
    {
        string cardId = "";
        for(int i = 0; i < PlayCardList.Count; i++)
        {
            cardId = cardId + PlayCardList[i].GetCardNum().ToString();
        }
        Debug.Log(cardId);


        RemoveAllCards(true);//为true时为移除出牌区卡片，false时移除手牌区
        CardEffects.MatchCard(cardId); //Matchcard顺便就执行卡的效果

    }

    private void Start()
    {

        playerHPBar = UIManager.Instance.CreatePlayerHpItem();
        Debug.Log("找到血条");
        hpText = playerHPBar.transform.Find("PlayerHPText").GetComponent<Text>();
        hpImage = playerHPBar.transform.Find("PlayerHPFill").GetComponent<Image>();
        defText = playerHPBar.transform.Find("PlayerDefText").GetComponent<Text>();

        playCardZone = transform.Find("PlayCardZone").gameObject;
        cardZone = transform.Find("CardZone").gameObject;
        cardArea = transform.Find("CardArea").gameObject;
        turnBtn = GameObject.Find("turnBtn").GetComponent<Button>();
        UseBtn = GameObject.Find("UseBtn").GetComponent<Button>();
        UseBtn.gameObject.SetActive(false);

        //获取回合切换按钮
        turnBtn.onClick.AddListener(onChangeTurnBtn);
        //获取出牌按钮
        UseBtn.onClick.AddListener(UseCard);

        UpdateHP();
        UpdateDef();


        //UpdateCardCount();
        //UpdateUsedCardCount();
        //UpdatePower();
    }

    public void UpdateHP()
    {
        hpText.text = FightManager.Instance.CurHP + "/" + FightManager.Instance.MaxHP;
        hpImage.fillAmount = (float)FightManager.Instance.CurHP / (float)FightManager.Instance.MaxHP;
    }

/*    public void UpdatePower()
    {
        //我们好像没有法力值这个东西
        powerText.text = FightManager.Instance.CurPowerCount + "/" + FightManager.Instance.MaxPowerCount;
    }*/

    public void UpdateDef()
    {
        defText.text = FightManager.Instance.DefCount.ToString();
    }

/*    public void UpdateCardCount()
    {
        cardCountText.text = FightCardManager.Instance.cardList.Count.ToString();
    }*/

/*    public void UpdateUsedCardCount()
    {
        noCardCountText.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }*/

    //创建卡牌物体
    public void CreatCardItem(int count)
    {
        for (int i = 0; i < count ; i++)
        {
            
            //Id	Name	Script	Type	Des	BgIcon	Icon	Expend	Arg0	Effects
            //唯一的标识（不能重复）	名称	卡牌添加的脚本	卡牌类型的Id	描述	卡牌的背景图资源路径	图标资源的路径	消耗的费用	属性值	特效
            //1000	普通攻击	AttackCardItem	10001	对单个敌人进行{0}点的伤害	Icon/BlueCard	Icon/sword_03e	1	3	Effects/GreenBloodExplosion

            GameObject obj = Instantiate(Resources.Load("UI-img/card/cardBackground"), transform.Find("CardArea") ,false) as GameObject;//加载卡牌UI，并根据父对象设置
            obj.GetComponent<Transform>().position = new Vector2(-3.1f, -3.2f);
            //var Item = obj.AddComponent<CardItem>();
            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            Debug.Log("当前创建的牌类型为" + System.Type.GetType(data["Script"]));
            
            CardItem Item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            Item.Init(data,i);
            cardItemList.Add(Item);
            //Debug.Log("现在有" + cardItemList.Count + "张牌");
        }
    }

    //更新卡牌位置
    public void UpdateCardItemPos()
    {
        Debug.Log("更新卡牌位置");
        for(int i = 0; i < cardItemList.Count; i++)
        {
            Transform card = cardItemList[i].transform;
            card.position = cardZone.transform.GetChild(i).position;
            card.transform.localScale = new Vector2(1,1);
        }
     
    }

    public void UpdatePlayCardPos()
    {
        Debug.Log("更新上方牌区位置");
        for (int i = 0; i < PlayCardList.Count; i++)
        {
            Transform card = PlayCardList[i].transform;
            card.position = playCardZone.transform.GetChild(i).position;
            card.transform.localScale = new Vector2(1, 1);
        }

        if (PlayCardList.Count == 4)
            UseBtn.gameObject.SetActive(true);
        else
            UseBtn.gameObject.SetActive(false);
    }

    public bool MoveCardToPlayArea(CardItem card)
    {
        CardItem nowcard = card;
        Debug.Log(nowcard);

        if (PlayCardList.Count < 4)
        {
            PlayCardList.Add(nowcard);
            UpdatePlayCardPos();
            //Debug.Log("已经移动卡牌至出牌区");
            cardItemList.Remove(nowcard);
            UpdateCardItemPos();
            Debug.Log("现在手牌区和出牌区各有" + cardItemList.Count + " " + PlayCardList.Count + "张牌");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MoveCardToHandArea(CardItem card)
    {
        CardItem nowcard = card;
        Debug.Log(nowcard);
        cardItemList.Add(nowcard);
        UpdateCardItemPos();
        //Debug.Log("已经移动卡牌至手牌");
        PlayCardList.Remove(nowcard);
        UpdatePlayCardPos();
        Debug.Log("现在手牌区和出牌区各有" + cardItemList.Count + " " + PlayCardList.Count + "张牌");
    }

    public int GetCardNum()
    {
        return cardItemList.Count;
    }

    public int GetPlayCardNum()
    {
        return PlayCardList.Count;
    }

    //删除卡牌物体
    public void RemoveCard(CardItem item, bool ifInPlayArea)
    {
        //AudioManager.Instance.PlayEffect("");//删除音效（文件夹路径

        item.enabled = false;//禁用卡牌逻辑

        //从集合中删除
        if (ifInPlayArea)
            PlayCardList.Remove(item);
        else 
            cardItemList.Remove(item);

        //刷新卡牌位置
        UpdateCardItemPos();
        UpdatePlayCardPos();

        item.transform.DOScale(0, 0.25f);

        Destroy(item.gameObject,1);


    }

    //清空所有卡牌
    public void RemoveAllCards(bool ifInPlayArea)
    {
        if (!ifInPlayArea)
        {
            for (int i = cardItemList.Count - 1; i >= 0; i--)
            {
                RemoveCard(cardItemList[i], ifInPlayArea);
            }
        }
        else
        {
            for (int i = PlayCardList.Count - 1; i >= 0; i--)
            {
                RemoveCard(PlayCardList[i], ifInPlayArea);
            }
        }
    }
}
