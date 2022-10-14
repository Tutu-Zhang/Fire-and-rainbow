using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("playerTime");

        UIManager.Instance.ShowTip("��һغ�", Color.green, delegate ()
        {

            //�������Ѿ��ľ������²�������
            if (FightCardManager.Instance.HasCard() == false)
            {
                FightCardManager.Instance.PrintCard();

                //UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            }


            //����

            int drawCardCount = 8 - UIManager.Instance.GetUI<FightUI>("fightBackground").GetCardNum();
            //Debug.Log(drawCardCount);//�Ѿ��޸�Ϊ������
            UIManager.Instance.GetUI<FightUI>("fightBackground").CreatCardItem (drawCardCount);//��������
            UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateCardItemPos();//���¿���λ��


        });
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
