using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToNext : MonoBehaviour
{
    private GameObject clickToNext_button;
    private int count = 1;

    void Start()
    {
        clickToNext_button = GameObject.Find("/Canvas/GameWindow/clickToNext");
       


        if (LevelManager.Instance.level == 0)
        {
            clickToNext_button.SetActive(true);
            GameObject guideText = GameObject.Find("/Canvas/GameWindow/guide");
            guideText.SetActive(true);
        }

        //很麻烦的写法，但是比再建一个txt简单多了
        PlayerPrefs.SetString("lv0guide1", "在赛博电子世界，我们会用网络攻击来战斗，而攻击形式会被具象为具体的形象。");
        PlayerPrefs.SetString("lv0guide2", "这便是你所看到的战斗界面。");
        PlayerPrefs.SetString("lv0guide3", "下面8位是信源区，每回合会有60%概率填充0，40%概率填充1。");
        PlayerPrefs.SetString("lv0guide4", "上面4个位置是信道区，将信源区的数字放入其中组合，可发动总计16种不同的技能，具体效果会在左上角的AI面板中写出。");
        PlayerPrefs.SetString("lv0guide5", "组合的技巧在于，技能组合中1的数量越多，技能效果越强力。");
        PlayerPrefs.SetString("lv0guide6", "以1开头的组合更倾向于攻击，而0开头的组合则更倾向于防守。");
        PlayerPrefs.SetString("lv0guide7", "不同的敌人需要不同的策。熟悉技能效果，合理规划资源，使用智慧战胜敌人吧！（点击任意位置结束教程）");
        //PlayerPrefs.SetString("lv0guide0", "进入加密通话，已为您暂时屏蔽敌人");
        PlayerPrefs.Save();


        clickToNext_button.GetComponent<Button>().onClick.AddListener(NextGuide);
        
    }

    private void NextGuide()
    {
        if (count <= 7 && LevelManager.Instance.level == 0)
        {
            GameObject guideText = GameObject.Find("/Canvas/GameWindow/guide");
            guideText.GetComponent<Text>().text = PlayerPrefs.GetString("lv0guide" + count.ToString());

            count += 1;
        }
        else
        {
            GameObject guideText = GameObject.Find("/Canvas/GameWindow/guide");

            guideText.SetActive(false);
            clickToNext_button.SetActive(false);
        }
    } 
        
}
