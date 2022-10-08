using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FightUI : UIBase
{
    //private Text cardCountText;//��������
    //private Text noCardCountText;//���ƶ�����
    //private Text powerText;
    private Text hpText;
    private Image hpImage;
    private Text defText;
    private GameObject playerHPBar;

    private List<CardItem> cardItemList; //�洢��������ļ���
    private void Awake()
    {
        cardItemList = new List<CardItem>();


        //����Ӧ�ö�ȡUIԪ�ص�text��img
        //cardCountText = transform.Find("").GetComponent<Text>();
        //noCardCountText = transform.Find("").GetComponent<Text>();
        //powerText = transform.Find("").GetComponent<Text>();
        playerHPBar = GameObject.Find("playerHPBar");
        Debug.Log("�ҵ�Ѫ��");
        hpText = playerHPBar.transform.Find("PlayerHPText").GetComponent<Text>();
        hpImage = playerHPBar.transform.Find("PlayerHPFill").GetComponent<Image>();
        defText = playerHPBar.transform.Find("PlayerDefText").GetComponent<Text>();

        //��ȡ�غ��л���ť
        GameObject.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnBtn);
    }

    //��һغϽ������л������˻غ�
    private void onChangeTurnBtn()
    {
        //ֻ����Ҳ����л�
        if (FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            FightManager.Instance.ChangeType(FightType.Enemy);
        }
        Debug.Log("�غ��л�");
    }

    private void Start()
    {
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
        //���Ǻ���û�з���ֵ�������
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

    //������������
    public void CreatCardItem(int count)
    {
        for (int i = 0; i < count ; i++)
        {
            
            //Id	Name	Script	Type	Des	BgIcon	Icon	Expend	Arg0	Effects
            //Ψһ�ı�ʶ�������ظ���	����	������ӵĽű�	�������͵�Id	����	���Ƶı���ͼ��Դ·��	ͼ����Դ��·��	���ĵķ���	����ֵ	��Ч
            //1000	��ͨ����	AttackCardItem	10001	�Ե������˽���{0}����˺�	Icon/BlueCard	Icon/sword_03e	1	3	Effects/GreenBloodExplosion

            GameObject obj = Instantiate(Resources.Load("UI-img/card/cardBackground"), transform) as GameObject;//���ؿ���UI
            obj.GetComponent<Transform>().position = new Vector2(-3.1f, -3.2f);
            //var Item = obj.AddComponent<CardItem>();
            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            Debug.Log(System.Type.GetType(data["Script"]));
            CardItem Item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            Item.Init(data,i);
            cardItemList.Add(Item);
        }
    }

    //���¿���λ��
    public void UpdateCardItemPos()
    {
        Debug.Log("���¿���λ��");
        float offset = 10.0f / 8;
        Vector2 startPos = new Vector2(-2.8f, -3.37f);
        for (int i = 0; i < cardItemList.Count; i++)
        {
            cardItemList[i].GetComponent<Transform>().position = startPos;
            startPos.x = startPos.x + offset;
        }
    }

    //ɾ����������
    public void RemoveCard(CardItem item)
    {
        AudioManager.Instance.PlayEffect("");//ɾ����Ч���ļ���·��

        item.enabled = false;//���ÿ����߼�

        //��ӵ����Ƽ���
        //FightCardManager.Instance.usedCardList.Add(item.data["Id"]);

        //����ʹ�ú�Ŀ�������
        //noCardCountText.text = FightCardManager.Instance.usedCardList.Count.ToString();

        //�Ӽ�����ɾ��
        cardItemList.Remove(item);

        //ˢ�¿���λ��
        UpdateCardItemPos();

        //�����Ƶ����ƶ�Ч��
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, -700), 0.25f);

        item.transform.DOScale(0, 0.25f);

        Destroy(item.gameObject,1);


    }

    //������п���
    public void RemoveAllCards()
    {
        for (int i = cardItemList.Count-1; i >= 0; i--)
        {
            RemoveCard(cardItemList[i]);
        }
    }
}
