using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FightLose : FightUnit
{
    public override void Init()
    {
        Debug.Log("你死了");
        FightManager.Instance.StopAllCoroutines();

        //需要显示一个结算UI，点击按钮方可回到选择页面或加载下一关
        SceneManager.LoadScene("selectScene");
    }

    public override void OnUpdate()
    {

    }
}
