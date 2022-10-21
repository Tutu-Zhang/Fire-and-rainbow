using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour
{
    private string Buff_Id;
    public int left_time;


    public void Init(string id, int time)
    {
        Buff_Id = id;
        left_time = time;
    }

    public string GetBuffId()
    {
        return Buff_Id;
    }

    public int GetLeftTime()
    {
        return left_time;
    }

    public void AddLeftTime(int time)
    {
        left_time += time;
    }

    public void SetLeftTime(int time)
    {
        left_time = time;
    }

    public void PassTurn()
    {
        left_time--;
        if (left_time <= 0)
        {
            if (this.Buff_Id == "0010")
                changeBackDrawCardProbability();

            UIManager.Instance.GetUI<FightUI>("fightBackground").RemoveBuff(this);
        }
    }

    //当Buff为0010，buff消失时抽卡概率变回0.4
    private void changeBackDrawCardProbability()
    {
        FightCardManager.Instance.SetPro_Low();
    }
}
