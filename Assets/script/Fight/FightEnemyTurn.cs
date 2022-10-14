using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyTurn : FightUnit
{
    public override void Init()
    {
        //ɾ�����п���(���ǲ���Ҫ��ʾɾ������
        //UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();

        GameObject turnBtn = GameObject.Find("turnBtn");
        turnBtn.SetActive(false);
        //��ʾ���˻غ���ʾ
        UIManager.Instance.ShowTip("���˻غ�", Color.red, delegate ()
        {
            //���ػغ��л���ť

            
            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());

            //��ʾ�غ��л���ť
            turnBtn.SetActive(true);

        });

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
