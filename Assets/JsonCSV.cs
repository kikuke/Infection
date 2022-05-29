using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonCSV
{
    public List<string> list = new List<string>();
    public int keyNum;
    public int optionNum;

    public JsonCSV() { }

    public JsonCSV(string filepath)
    {
        list = new List<string>();
        keyNum = 0;
        optionNum = 0;
        //json파일이 없다면 해당경로로 가서 기본 파일 가져와서 세팅.
        TextAsset csvFile = Resources.Load(filepath) as TextAsset;
        //행

        string[] rows = csvFile.text.Split('\n');
        
        foreach(string row in rows)
        {
            string[] values = row.Split(',');
            foreach(string value in values)
            {
                if (optionNum == 0)
                    keyNum++;
                list.Add(value);
            }
            optionNum++;
        }
    }

    public string ReadJsonCSV(string key, string option)
    {
        int i;
        int keyIndex = 0;

        for(i = 0; i < keyNum; i++)
        {
            if (list[i] == key)
            {
                keyIndex = i;
                break;
            }
        }
        if(option == null)
            return list[keyNum + keyIndex];
        for (i = 0; i < optionNum; i++)
        {
            if(list[i*keyNum] == option)
            {
                return list[i * keyNum + keyIndex];
            }
        }

        return null;
    }
}
