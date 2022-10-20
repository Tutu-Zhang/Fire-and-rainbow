using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffects
{

    public static void MatchBuff(string buffid)
    {
        switch (buffid)
        {
            case "0101":
                buff_recover_0101();
                break;

            case "0111":
                break;

            case "1001":
                buff_attack_1001();
                break;

            case "1011":
                buff_attack_1011();
                break;

            case "1100":
                break;

            case "1101":               
                break;

            case "1111":
                break;

        }
    }

    public static void buff_recover_0101()
    {
        FightManager.Instance.GetRecover(3);
    }

    public static int buff_defend_0111(int damage)
    {
        int this_dmg = damage;
        this_dmg -= 2;

        if (this_dmg < 0)
            this_dmg = 0;

        System.Random random = new System.Random();
        double temp = random.NextDouble();
        if (temp >= 0.75)
        {
            this_dmg = 0;
        }

        return this_dmg;
    }

    public static void buff_attack_1001()
    {
        FightManager.Instance.Attack_Enemy(2);
    }

    public static void buff_attack_1011()
    {
        FightManager.Instance.Attack_Enemy(2);

        System.Random random = new System.Random();
        double temp = random.NextDouble();
        if (temp >= 0.7)
        {
            FightManager.Instance.Attack_Enemy(2);
        }
    }

    public static bool buff_silence_1100()
    {
        System.Random random = new System.Random();
        double temp = random.NextDouble();
        if (temp >= 0.7)
        {
            Debug.Log("跳过敌方回合");
            return true;
        }

        return false;
    }

    public static void buff_counter_1101(int dmg)
    {
        FightManager.Instance.Attack_Enemy(dmg);
    }

    public static int buff_gamble_1111()
    {
        System.Random random = new System.Random();
        double temp = random.NextDouble();

        if (temp > 0.25)
        {
            return 2;
        }
        else
            return 0;
    }

}
