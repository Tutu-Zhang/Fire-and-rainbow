using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("playerTime");
        //FightManager.Instance.SetBtn(false);

        UIManager.Instance.ShowTip("��һغ�", Color.green, delegate ()
        {

            //�������Ѿ��ľ������²�������
            if (FightCardManager.Instance.HasCard() == false)
            {
                FightCardManager.Instance.PrintCard();

                //UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            }

            //�غ�ǰ��ִ��Ч�����ж��׶�,�������,ֻ��0101
            //BuffItem buff_1 = UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("0101");
            if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("0101") != null)
            {
                BuffEffects.MatchBuff("0101");
            }
            


            //����

            int drawCardCount = 8 - UIManager.Instance.GetUI<FightUI>("fightBackground").GetCardNum();
            //Debug.Log(drawCardCount);//�Ѿ��޸�Ϊ������
            UIManager.Instance.GetUI<FightUI>("fightBackground").CreatCardItem (drawCardCount);//��������
            UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateCardItemPos();//���¿���λ��

            FightManager.Instance.SetBtn(true);


        });
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
