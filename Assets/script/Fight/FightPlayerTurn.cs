using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("playerTime");
        //��ʾ�غ��л���ť
        GameObject turnBtn = GameObject.Find("turnBtn");
        turnBtn.SetActive(true);
        UIManager.Instance.ShowTip("��һغ�", Color.green, delegate ()
        {
            //�ظ��ж���
            //FightManager.Instance.CurPowerCount = 3;
            //UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();

            //�������Ѿ��ľ������²�������
            if (FightCardManager.Instance.HasCard() == false)
            {
                FightCardManager.Instance.Init();

                //UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            }


            //����

            int drawCardCount = FightCardManager.Instance.cardList.Count;
            Debug.Log(drawCardCount);//�����Ŀ�е����⣬������Ϊ8
            UIManager.Instance.GetUI<FightUI>("fightBackground").CreatCardItem (drawCardCount);//��������
            UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateCardItemPos();//���¿���λ��

            //���¿�����
            //UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        });
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
