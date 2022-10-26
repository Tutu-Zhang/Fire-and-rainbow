using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffects
{
    public static void MatchCard(string cardid)
    {
        switch (cardid)
        {
            case "0000":
                recover_Defend_0000();
                break;

            case "0001":
                attack_defend_0001();
                break;

            case "0010":
                reDrawCard_0010();
                break;

            case "0011":
                gamble_0011();
                break;

            case "0100":
                recover_0100();
                break;

            case "0101":
                recover_0101();
                break;

            case "0110":
                recover_0110();
                break;

            case "0111":
                defend_0111();
                break;

            case "1000":
                attack_1000();
                break;

            case "1001":
                attack_1001();
                break;

            case "1010":
                attack_1010();
                break;

            case "1011":
                attack_1011();
                break;

            case "1100":
                silence_1100();
                break;

            case "1101":
                counter_1101();
                break;

            case "1110":
                attack_1110();
                break;

            case "1111":
                gamble_1111();
                break;

        }
    }

    public static void recover_Defend_0000()
    {
        FightManager.Instance.GetDefendRecover(2);
    }

    public static void attack_defend_0001()
    {
        FightManager.Instance.Attack_Enemy(2);
        FightManager.Instance.GetDefendRecover(2);
    }

    public static void reDrawCard_0010()
    {
        UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("0010", 2);
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateCardItemPos();
    }

    public static void gamble_0011()
    {
        System.Random random = new System.Random();
        string card = "";

        while (card.Length < 4)
        {
            string num = "0";
            double temp = random.NextDouble();
            if (temp > 0.5)
            {
                num = "1";
            }

            card += num;
        }

        //Debug.Log("0011±¾´ÎÓ¡ÅÆÎª" + card);
        if (card == "0011")
        {
            MatchCard("1000");
        }
        else
            MatchCard(card);
    }

    public static void recover_0100()
    {
        FightManager.Instance.GetRecover(4);
    }

    public static void recover_0101()
    {
        UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("0101", 3);
    }

    public static void recover_0110()
    {
        FightManager.Instance.GetRecover(6);

        System.Random random = new System.Random();
        double temp = random.NextDouble();
        if(temp >= 0.5)
        {
            FightManager.Instance.GetRecover(6);
        }
    }

    public static void defend_0111()
    {
        UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("0111", 3);
    }

    public static void attack_1000()
    {
        FightManager.Instance.Attack_Enemy(4);
    }

    public static void attack_1001()
    {
        FightManager.Instance.Attack_Enemy(3);
        UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("1001", 3);
    }


    public static void attack_1010()
    {
        System.Random random = new System.Random();
        double temp = random.NextDouble();
        if (temp >= 0.5)
        {
            FightManager.Instance.Attack_Enemy(12);
        }
        else
        {
            FightManager.Instance.Attack_Enemy(6);
        }
    }

    public static void attack_1011()
    {
        UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("1011", 3);
    }

    public static void silence_1100()
    {
        FightManager.Instance.Attack_Enemy(4);
        UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("1100", 1);
    }

    public static void counter_1101()
    {
        UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("1101", 3);
    }

    public static void attack_1110()
    {
        FightManager.Instance.Attack_Enemy(6);
        UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("1110", 999);

    }

    public static void gamble_1111()
    {
        FightManager.Instance.Attack_Enemy(10);
        UIManager.Instance.GetUI<FightUI>("fightBackground").addBuff("1111", 5);
    }


}
