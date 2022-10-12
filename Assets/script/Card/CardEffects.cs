using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffects : MonoBehaviour
{
    public static void MatchCard(string cardid)
    {
        switch (cardid)
        {
            case "0000":
                break;

            case "0010":
                reDrawCard_0010();
                break;

            case "0100":
                recover_0100();
                break;

            case "0110":
                recover_0110();
                break;

            case "1000":
                attack_1000();
                break;

            case "1010":
                attack_1010();
                break;

            case "1110":
                attack_1110();
                break;
        }
    }

    public static void reDrawCard_0010()
    {
        if (FightCardManager.Instance.HasCard() == false)
        {
            FightCardManager.Instance.PrintCard();

            //UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        }

        UIManager.Instance.GetUI<FightUI>("fightBackground").CreatCardItem(4);
        UIManager.Instance.GetUI<FightUI>("fightBackground").UpdateCardItemPos();
    }

    public static void recover_0100()
    {
        FightManager.Instance.GetRecover(4);
    }

    public static void recover_0110()
    {
        FightManager.Instance.GetRecover(4);

        System.Random random = new System.Random();
        double temp = random.NextDouble();
        if(temp >= 0.5)
        {
            FightManager.Instance.GetRecover(4);
        }
    }

    public static void attack_1000()
    {
        FightManager.Instance.Attack_Enemy(4);
    }

    public static void attack_1010()
    {
        FightManager.Instance.Attack_Enemy(3);

        System.Random random = new System.Random();
        double temp = random.NextDouble();
        if (temp >= 0.5)
        {
            FightManager.Instance.Attack_Enemy(3);
        }
    }

    public static void attack_1110()
    {
        System.Random random = new System.Random();
        double temp = random.NextDouble();

        if(temp > 0.75)
        {
            FightManager.Instance.GetPlayHit(4);
        }
        else
        {
            FightManager.Instance.Attack_Enemy(8);
        }
    }



}
