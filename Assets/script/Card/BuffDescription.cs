using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class BuffDescription : MonoBehaviour, IPointerClickHandler
{
    private int index;
    FightUI fightUI;
    GameObject Case;
    Text buffContent;

    bool ifshow = false;

    private void Start()
    {
        fightUI = UIManager.Instance.GetUI<FightUI>("fightBackground");
        Case = transform.Find("BuffCase").gameObject;

        buffContent = Case.transform.Find("Viewport").transform.Find("BuffText").GetComponent<Text>();
        Case.SetActive(ifshow);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ifshow = !ifshow;

        if (ifshow)
        {
            ShowBuffText();
        }
        else
        {
            HideBuffText();
        }
        
    }

    private void ShowBuffText()
    {
        List<BuffItem> bufflist = fightUI.returnBuffList();

        string buffText = "";

        Dictionary<string, string> BuffDesData = GameConfigManager.Instance.GetBuffDesById("1");

        for (int i = 0; i < bufflist.Count; i++)
        {
            buffText += bufflist[i].GetBuffId() +":"+ BuffDesData[bufflist[i].GetBuffId()] + ",Ê£Óà" + bufflist[i].GetLeftTime() + "»ØºÏ";
        }

        Case.SetActive(true);
        buffContent.text = buffText;

    }

    private void HideBuffText()
    {
        buffContent.text = "";
        Case.SetActive(false);
    }
}
