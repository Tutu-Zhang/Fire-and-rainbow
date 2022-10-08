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
            //回复行动力
            //FightManager.Instance.CurPowerCount = 3;
            //UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();

            //若手牌已经耗尽，重新补满手牌
            if (FightCardManager.Instance.HasCard() == false)
            {
                FightCardManager.Instance.Init();

                //UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            }


            //抽牌

            int drawCardCount = FightCardManager.Instance.cardList.Count;
            Debug.Log(drawCardCount);//这个数目有点问题，先设置为8
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
