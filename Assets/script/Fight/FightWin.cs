using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FightWin : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.StopAllCoroutines();
        Debug.Log("��Ϸʤ��");

    }
    public override void OnUpdate()
    {

    }
}
