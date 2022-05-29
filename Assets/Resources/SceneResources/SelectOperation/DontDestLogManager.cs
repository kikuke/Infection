using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestLogManager : MonoBehaviour
{
    public static string zone;
    public static bool gameClear = false;
    public static List<Dictionary<string, object>> messageEvent;

    private void Awake()
    {
        messageEvent = JsonReader.Read(PathManager.FindPath("MessageEvent"));
        DontDestroyOnLoad(gameObject);
    }
}
