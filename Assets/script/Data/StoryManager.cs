using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    private int levelCount;

    private GameObject dialogueText;
    private string dialogue;
    private Dictionary<string, string> DialogueData;
    private int MaxCount;
    private int CurCount = 1;

    private Image BackgroundImg;
    private string imgPath;

    public Button button;

    public void Start()
    {
        GameConfigManager.Instance.Init();
        levelCount = LevelManager.Instance.level;
        //levelCount = 1;//调试用

        //改变背景图物体
        BackgroundImg = GameObject.FindGameObjectWithTag("DialogueBackground").GetComponent<Image>();
        imgPath = "UI-img/CG/" + levelCount.ToString() + "img";
        Debug.Log(imgPath);
        Texture2D texture = Resources.Load<Texture2D>(imgPath);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        BackgroundImg.sprite = sp;


        //获取旁白物体
        dialogueText = GameObject.FindGameObjectWithTag("DialogueText");
        DialogueData = GameConfigManager.Instance.GetDialogueBeforeDataById(levelCount.ToString());
        MaxCount = int.Parse(DialogueData["count"]);
        ChangeDialogue();

        button.onClick.AddListener(ChangeDialogue);
    }

    private void ChangeDialogue()
    {
        dialogue = DialogueData[CurCount.ToString()];
        dialogueText.GetComponent<Text>().text = dialogue;
        CurCount += 1;

        //所有剧情文案加载完毕
        if (CurCount >= MaxCount+1)
        {
            button.gameObject.SetActive(false);
            Invoke("GoToGame", 1f);
        }
    }

    private void GoToGame()
    {
        SceneManager.LoadScene("game1");
    }
}
