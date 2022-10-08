using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightWin : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.StopAllCoroutines();
        Debug.Log("游戏胜利");
        //显示结束界面，自己制作吧
    }
}
