using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FightLose : FightUnit
{
    public Button BackToSelect;
    public Button ReBuildGame;

    public override void Init()
    {
        FightManager.Instance.StopAllCoroutines();
        Debug.Log("”Œœ∑ ß∞‹");

        Invoke("ShowWindow", 1f);

                                    
    }
    private void ShowWindow()
    {
        GameObject obj = GameObject.Find("/Canvas/GameWindow/GameLose");
        obj.SetActive(true);

        BackToSelect = GameObject.FindGameObjectWithTag("LoseGTL").GetComponent<Button>();
        BackToSelect.onClick.AddListener(GoToSelectScence);
        ReBuildGame = GameObject.FindGameObjectWithTag("LoseGTS").GetComponent<Button>();
        ReBuildGame.onClick.AddListener(GoToReBuildGame);
    }

    private void GoToSelectScence()
    {
        SceneManager.LoadScene("selectScene");
    }

    private void GoToReBuildGame()
    {
        SceneManager.LoadScene("game1");
    }

    public override void OnUpdate()
    {

    }
}
