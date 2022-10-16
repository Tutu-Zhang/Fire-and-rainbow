using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour
{
    public string Buff_Id;
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

    public void PassTurn()
    {
        left_time--;
        if (left_time == 0)
            UIManager.Instance.GetUI<FightUI>("fightBackground").RemoveBuff(this);
    }


}
