using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightLose : FightUnit
{
    public override void Init()
    {
        Debug.Log("������");
        FightManager.Instance.StopAllCoroutines();

        //��ʾʧ�ܽ��� �Լ�������
    }

    public override void OnUpdate()
    {

    }
}
