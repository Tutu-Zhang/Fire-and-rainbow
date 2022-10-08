using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//用户信息管理类（用户拥有的卡牌等
public class RoleManager : MonoBehaviour
{
    public static RoleManager Instance = new RoleManager();


    public List<string> cardList;//存储拥有的卡牌ID
    public void Init()
    {
        cardList = new List<string>();
        
        System.Random random = new System.Random();

        //这里要改一下，这些指的可能是
        for (int i = 0; i < 8; i++)
        {
            string num = "0";
            double temp = random.NextDouble();
            if (temp>=0.75)
            {
                num = "0";
            }
            
            cardList.Add(num);//相当于手牌列表

            Debug.Log("success"+i);
        }

        
    }
}
