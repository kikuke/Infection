using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static List<Dictionary<string, object>> hardwarelist;
    public static List<Dictionary<string, object>> softwarelist;
    public static List<Dictionary<string, object>> enemylist;
    // Start is called before the first frame update
    private void Awake()
    {
        RWJsonCSV.SetCSVToJson(PathManager.FindPath("HardwareList"));
        RWJsonCSV.SetCSVToJson(PathManager.FindPath("SoftwareList"));
        RWJsonCSV.SetCSVToJson(PathManager.FindPath("EnemyList"));

        hardwarelist = JsonReader.Read(PathManager.FindPath("HardwareList"));
        softwarelist = JsonReader.Read(PathManager.FindPath("SoftwareList"));
        enemylist = JsonReader.Read(PathManager.FindPath("EnemyList"));
    }

    public static int FindOption(List<Dictionary<string, object>> list, string option)
    {
        int i;
        for(i = 0; i < list.Count; i++)
        {
            if ((string)list[i]["Option"] == option)
                return i;
        }
        return -1;//비정상 값
    }
}
