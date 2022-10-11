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

}
