using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

class RWJsonCSV
{
    static string path = Application.persistentDataPath + "/Save";
    
    //######Json최적화 안됨. 불필요한 변환 과정을 거침.jsonPath.ReadJsonCSV("Hardware", null)이것을 이용하면 리스트도 필요없음. 나중에 시간 나면 바꾸기??
    public static void SetCSVToJson(string file)//제이슨 검사하고 없을 경우 생성하기//리소스 로드 이용한거임.//만약 나중에 업데이트하면 이 함수로 읽고 업데이트 csv파일을 별개로 만들어서 json에 덧붙이기
    {
        DirectoryInfo di = new DirectoryInfo(path + "/" +file.Substring(0,file.LastIndexOf("/")));

        Debug.Log(di);
        if (di.Exists == false)
            di.Create();

        JsonCSV jsonFile = new JsonCSV(file);//베이스 데이터임. 바뀌지 않음
        string jsonData = ObjectToJson(jsonFile);
        CreateJsonFile(path, file, jsonData);
    }

    public static string ReadJsonToString(string file)//제이슨이 있을때 사용하기. 스트리밍 데이터는 이걸로 읽음.리소스 로드처럼 사용하기
    {
        FileInfo fi = new FileInfo(path + "/" + file + ".json");//맞는지 확인하기

        if (fi.Exists == false)
            SetCSVToJson(file);

        JsonCSV jsonFile = LoadJsonFile<JsonCSV>(path, file);
        string data = null;
        int i = 0;
        foreach(string value in jsonFile.list)
        {
            i++;
            data += value;
            if (i == jsonFile.keyNum)
            {
                data += "\r\n";
                i = 0;
            }
            else
                data += ",";
        }

        return data;
    }

    public static void WriteListToJson(List<Dictionary<string, object>> list, string file)//이것이 주로쓰임. SetCSVToJson은 처음 초기화 상태에만 사용함. 초기화는json없애서 진행
    {
        FileInfo fi = new FileInfo(path + "/" + file + ".json");//맞는지 확인하기
        Debug.Log("@@@#" + fi.Exists);

        if (fi.Exists == false)
            SetCSVToJson(file);
        else
        {
            JsonCSV jsonFile = new JsonCSV();

            jsonFile.keyNum = 0;
            jsonFile.optionNum = list.Count;

            foreach (KeyValuePair<string, object> kv in list[0])
            {
                jsonFile.keyNum++;
                jsonFile.list.Add(kv.Key);
            }
            for (int i = 0; i < list.Count; i++)
                foreach (KeyValuePair<string, object> kv in list[i])
                    jsonFile.list.Add(kv.Value.ToString());

            string jsonData = ObjectToJson(jsonFile);

            CreateJsonFile(path, file, jsonData);
        }
    }


    static string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    static void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    static T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        return JsonUtility.FromJson<T>(jsonData);
    }
}
