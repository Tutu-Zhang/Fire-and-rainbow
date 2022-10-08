using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightLose : FightUnit
{
    public override void Init()
    {
        Debug.Log("你死了");
        FightManager.Instance.StopAllCoroutines();

        //显示失败界面 自己制作吧
    }

    public override void OnUpdate()
    {

    }
}
