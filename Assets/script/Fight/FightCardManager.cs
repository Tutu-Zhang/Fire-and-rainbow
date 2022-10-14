using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCardManager 
{
    public static FightCardManager Instance = new FightCardManager();


    public List<string> cardList;//牌堆里的牌，也就是不在玩家手里的牌

    //public List<string> usedCardList;//弃牌堆



    public void Init()
    {
        cardList = new List<string>();

        //usedCardList = new List<string>();

        System.Random random = new System.Random();
        
        while (cardList.Count<16)
        {
            string num = "0";
            double temp = random.NextDouble();
            if (temp >= 0.6)
            {
                num = "1";
            }

            cardList.Add(num);//相当于手牌列表
        }

       // Debug.Log(cardList.Count);//输出手牌堆数量
    }

    public void PrintCard()
    {
        System.Random random = new System.Random();
        //补满手牌
        while (cardList.Count < 16)
        {
            string num = "0";
            double temp = random.NextDouble();
            if (temp >= 0.6)
            {
                num = "1";
            }

            cardList.Add(num);//相当于手牌列表
        }

        //Debug.Log(cardList.Count);//输出手牌堆数量
    }

    //是否有卡
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    //抽卡
    public string DrawCard()
    {
        if (!HasCard())
            PrintCard();

        string id = cardList[cardList.Count - 1];//牌库？
        cardList.RemoveAt(cardList.Count - 1);//将对应卡牌从牌库中移除
        return id;
    }


}
