using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCardManager 
{
    public static FightCardManager Instance = new FightCardManager();


    public List<string> cardList;//�ƶ�����ƣ�Ҳ���ǲ�������������

    //public List<string> usedCardList;//���ƶ�



    public void Init()
    {
        cardList = new List<string>();

        //usedCardList = new List<string>();

        System.Random random = new System.Random();
        //��������
        while (cardList.Count<8)
        {
            /*//�����һ�ſ�
            int tempIndex = Random.Range(0, tempList.Count);

            //��ӵ�����
            cardList.Add(tempList[tempIndex]);

            //��ʱ����ɾ��
            tempList.RemoveAt(tempIndex);*/


            string num = "0";
            double temp = random.NextDouble();
            if (temp >= 0.75)
            {
                num = "1";
            }

            cardList.Add(num);//�൱�������б�
        }

        Debug.Log(cardList.Count);//������ƶ�����
    }

    public void PrintCard()
    {
        System.Random random = new System.Random();
        //��������
        while (cardList.Count < 8)
        {
            /*//�����һ�ſ�
            int tempIndex = Random.Range(0, tempList.Count);

            //��ӵ�����
            cardList.Add(tempList[tempIndex]);

            //��ʱ����ɾ��
            tempList.RemoveAt(tempIndex);*/


            string num = "0";
            double temp = random.NextDouble();
            if (temp >= 0.75)
            {
                num = "1";
            }

            cardList.Add(num);//�൱�������б�
        }

        Debug.Log(cardList.Count);//������ƶ�����
    }

    //�Ƿ�����
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    //�鿨
    public string DrawCard()
    {
        string id = cardList[cardList.Count - 1];//�ƿ⣿
        cardList.RemoveAt(cardList.Count - 1);//����Ӧ���ƴ��ƿ����Ƴ�
        return id;
    }


}
