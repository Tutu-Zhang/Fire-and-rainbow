using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("playerTime");
        //FightManager.Instance.SetBtn(false);

        UIManager.Instance.ShowTip("玩家回合", Color.green, delegate ()
        {

            //若手牌已经耗尽，重新补满手牌
            if (FightCardManager.Instance.HasCard() == false)
            {
                FightCardManager.Instance.PrintCard();

                //UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            }

            //回合前就执行效果的判定阶段,对于玩家,只有0101
            //BuffItem buff_1 = UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("0101");
            if (UIManager.Instance.GetUI<FightUI>("fightBackground").FindBuff("0101") != null)
            {
                BuffEffects.MatchBuff("0101");
            }
            


            //抽牌

            int drawCardCount = 8 - UIManager.Instance.GetUI<FightUI>("fightBackground").GetCardNum();
            //Debug.Log(drawCardCount);//已经修改为补满牌
            UIManager.Instance.GetUI<FightUI>("fightBackground").CreatCardItem (drawCardCount);//补满卡牌
            UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateCardItemPos();//更新卡牌位置

            FightManager.Instance.SetBtn(true);


        });
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
