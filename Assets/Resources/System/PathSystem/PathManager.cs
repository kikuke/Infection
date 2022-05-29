using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private string csvpath;
    private static List<Dictionary<string, object>> paths;

    // Start is called before the first frame update
    void Awake()
    {
        csvpath = "System/PathSystem/Paths";
        RWJsonCSV.SetCSVToJson(csvpath);
        paths = JsonReader.Read(csvpath);
    }

    public static string FindPath(string name)
    {
        return (string)paths[0][name];
    }
}
