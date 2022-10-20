using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FightWin : FightUnit
{
    public Button BackToSelect;
    public Button GoToAfterGame;

    public override void Init()
    {
        FightManager.Instance.StopAllCoroutines();
        Debug.Log("ÓÎÏ·Ê¤Àû");
    
        Invoke("ShowWindow", 1f);
    }

    private void ShowWindow()
    {
        GameObject obj = GameObject.Find("/Canvas/GameWindow/GameWin");
        obj.SetActive(true);

        BackToSelect = GameObject.FindGameObjectWithTag("WinGTL").GetComponent<Button>();
        BackToSelect.onClick.AddListener(GoToSelectScence);
        GoToAfterGame = GameObject.FindGameObjectWithTag("WinGTS").GetComponent<Button>();
        GoToAfterGame.onClick.AddListener(GoToAfterGameScence);
    }

    private void GoToSelectScence()
    {
        SceneManager.LoadScene("selectScene");
    }

    private void GoToAfterGameScence()
    {
        SceneManager.LoadScene("AfterGame");
    }

    public override void OnUpdate()
    {

    }
}
