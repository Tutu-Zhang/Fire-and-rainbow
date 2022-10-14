using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyTurn : FightUnit
{
    public override void Init()
    {
        //删除所有卡牌(我们不需要显示删除卡牌
        //UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();

        GameObject turnBtn = GameObject.Find("turnBtn");
        turnBtn.SetActive(false);
        //显示敌人回合提示
        UIManager.Instance.ShowTip("敌人回合", Color.red, delegate ()
        {
            //隐藏回合切换按钮

            
            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());

            //显示回合切换按钮
            turnBtn.SetActive(true);

        });

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
