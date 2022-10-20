using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigManager
{
    public static GameConfigManager Instance = new GameConfigManager();

    //���Ʊ�
    private GameConfigData cardData;
    //���˱�
    private GameConfigData enemyData;
    //�ؿ���
    private GameConfigData levelData;

    private GameConfigData cardTypeData;

    private GameConfigData playerSkill;

    private GameConfigData DialogueBeforeData;

    private GameConfigData DialogueAfterData;

    private TextAsset textAsset;

    //��ʼ�������ļ���txt �洢���ڴ��У�
    public void Init()
    {
        //��ȡ���ƣ����˵ȵ�����
        textAsset = Resources.Load<TextAsset>("Data/card");
        cardData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/cardType");
        cardTypeData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/playerSkill");
        playerSkill = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/DialogueBefore");
        DialogueBeforeData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/DialogueAfter");
        DialogueAfterData = new GameConfigData(textAsset.text);
    }

    public List<Dictionary<string,string>> GetCardLines()
    {
        return cardData.GetLines();
    }

    public List<Dictionary<string, string>> GetEnemyLines()
    {
        return enemyData.GetLines();
    }

    public List<Dictionary<string, string>> GetLevelLines()
    {
        return levelData.GetLines();
    }

    public List<Dictionary<string, string>> GetDialogueBeforeDatas()
    {
        return DialogueBeforeData.GetLines();
    }

    public List<Dictionary<string, string>> GetDialogueAfterDataData()
    {
        return DialogueAfterData.GetLines();
    }




    public Dictionary<string,string> GetCardById(string id)
    {
        return cardData.GetOneById(id);
    }
    public Dictionary<string, string> GetEnemyById(string id)
    {
        return enemyData.GetOneById(id);
    }
    public Dictionary<string, string> GetLevelById(string id)
    {
        return levelData.GetOneById(id);
    }

    public Dictionary<string, string> GetCardTypeById(string id)
    {
        return cardTypeData.GetOneById(id);
    }

    public Dictionary<string, string> GetPlayerSkillsById(string id)
    {
        return playerSkill.GetOneById(id);
    }

    public Dictionary<string, string> GetDialogueBeforeDataById(string id)
    {
        return DialogueBeforeData.GetOneById(id);
    }

    public Dictionary<string, string> GetDialogueAfterDataById(string id)
    {
        return DialogueAfterData.GetOneById(id);
    }
}
