using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���˹�����
public class EnemyManager 
{
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList;//�洢ս���еĵ���
 
    //���ص�����Դ
    public void loadRes(string id)
    {

        enemyList = new List<Enemy>();
        
        //�����ȡ���ǹؿ���
        /*
        Id	Name	EnemyIds	Pos
        Id	�ؿ�����	����Id������	���й����λ��
        10001	1	10001	"0,0,0"
        10002	2	10001=10001	"0,0,0=0,0,1"
        10003	3	10001=10002=10003	"3,0,1=0,0,1=-3,0,1"
        */
        Dictionary<string, string> LevelData = GameConfigManager.Instance.GetLevelById(id);

        string[] enemyids = LevelData["EnemyIds"].Split('=');

        string[] enemyPos = LevelData["Pos"].Split('=');

        for (int i = 0; i < enemyids.Length; i++)
        {
            string enemyid = enemyids[i];
            string[] posArr = enemyPos[i].Split(',');

            //��һ�ص���λ������
            //float x = float.Parse(posArr[0]);
            float x = float.Parse("-0.3");
            //float y = float.Parse(posArr[1]);
            float y = float.Parse("1.4");

            //����id��ȡ�������˵���Ϣ
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyid);

            Debug.Log(enemyData["Model"]);
            
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;//����Դ���ض�Ӧ�ĵ���ģ��    

            Enemy enemy = obj.AddComponent<Enemy>();//Ϊ����������ӽű�

            enemy.Init(enemyData);//�洢������Ϣ

            enemyList.Add(enemy);//�洢�������б�

            obj.transform.position = new Vector2(x, y);
        }
    }

    //ɾ������
    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //�������Ƿ��ɱ���˵��ж�
        if (enemyList.Count == 0)
        {
            FightManager.Instance.ChangeType(FightType.Win);
        }
    }
    //ִ����Ȼ���Ĺ������Ϊ
    public IEnumerator DoAllEnemyAction()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }

        //�ж����������е�����Ϊ
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }

        //�л�����һغ�
        FightManager.Instance.ChangeType(FightType.Player);
    }
}
