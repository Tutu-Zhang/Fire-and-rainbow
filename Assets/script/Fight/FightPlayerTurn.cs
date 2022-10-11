using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("playerTime");
        //显示回合切换按钮
        GameObject turnBtn = GameObject.Find("turnBtn");
        turnBtn.SetActive(true);
        UIManager.Instance.ShowTip("玩家回合", Color.green, delegate ()
        {

            //若手牌已经耗尽，重新补满手牌
            if (FightCardManager.Instance.HasCard() == false)
            {
                FightCardManager.Instance.PrintCard();

                //UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            }


            //抽牌

            int drawCardCount = 8 - UIManager.Instance.GetUI<FightUI>("fightBackground").GetCardNum();
            //Debug.Log(drawCardCount);//已经修改为补满牌
            UIManager.Instance.GetUI<FightUI>("fightBackground").CreatCardItem (drawCardCount);//补满卡牌
            UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateCardItemPos();//更新卡牌位置

            //更新卡牌数
            //UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();

        });
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
