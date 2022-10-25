using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using Excel;


public class ExcelReader : MonoBehaviour
{
    public static ExcelReader Instance = new ExcelReader();

    private DataSet BeforeDialogueResult;
    private DataSet AfterDialogueResult;

    public void Awake()
    {
        Debug.Log("��ʼ��ȡexcel�ļ�");
        //��ȡ�����ļ�
        FileStream BeforeStream = File.Open("Data/DialogueBefore.xlsx", FileMode.Open, FileAccess.Read, FileShare.Read);
        FileStream AfterStream = File.Open("Data/DialogueAfter", FileMode.Open, FileAccess.Read, FileShare.Read);

        Debug.Log("��ȡ����excel�ļ���"+BeforeStream);
        //���ļ����н�����
        IExcelDataReader BeforeExcelReader = ExcelReaderFactory.CreateOpenXmlReader(BeforeStream);//��ȡ 2007���Ժ�İ汾
        IExcelDataReader AfterExcelReader = ExcelReaderFactory.CreateOpenXmlReader(AfterStream);//��ȡ 2007���Ժ�İ汾
        //��ȫ�����ݶ�ȡ����������result��
        BeforeDialogueResult = BeforeExcelReader.AsDataSet();
        AfterDialogueResult = AfterExcelReader.AsDataSet();
    }

    //����λ�û�ȡ��Ӧ����
    public string GetDialogue(int x,int y,string WhichToGet)
    {
        x += 3;
        y += 2;

        string str = "";
        if (WhichToGet == "before")
        {
            str = BeforeDialogueResult.Tables[0].Rows[x][y].ToString();
        }
        else if(WhichToGet == "after")
        {
            str = AfterDialogueResult.Tables[0].Rows[x][y].ToString();
        }
        else
        {
            str = "ExcelReader��ȡ�ļ�ʱ�д�����ȷ�Ĳ���";
        }
        return str;
    }
}
