using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//�û���Ϣ�����ࣨ�û�ӵ�еĿ��Ƶ�
public class RoleManager : MonoBehaviour
{
    public static RoleManager Instance = new RoleManager();


    public List<string> cardList;//�洢ӵ�еĿ���ID
    public void Init()
    {
        cardList = new List<string>();
        
        System.Random random = new System.Random();

        //����Ҫ��һ�£���Щָ�Ŀ�����
        for (int i = 0; i < 8; i++)
        {
            string num = "0";
            double temp = random.NextDouble();
            if (temp>=0.75)
            {
                num = "0";
            }
            
            cardList.Add(num);//�൱�������б�

            Debug.Log("success"+i);
        }

        
    }
}
