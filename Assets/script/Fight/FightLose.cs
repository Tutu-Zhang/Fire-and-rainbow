using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FightLose : FightUnit
{
    public override void Init()
    {
        Debug.Log("������");
        FightManager.Instance.StopAllCoroutines();

        //��Ҫ��ʾһ������UI�������ť���ɻص�ѡ��ҳ��������һ��
        SceneManager.LoadScene("selectScene");
    }

    public override void OnUpdate()
    {

    }
}
