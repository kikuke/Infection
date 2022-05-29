using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationLogManager : MonoBehaviour
{
    public static List<Dictionary<string, object>> operationLog;
    private static List<Dictionary<string, object>> operationSetting;
    // Start is called before the first frame update
    private static int maxLog;

    public static string zone;
    public static bool isClear;
    public static float time;
    public static int score;
    public static int kill;
    public static int collectLevel;
    public static string corePart;
    public static string headPart;
    public static string bodyPart;
    public static string leftArmPart;
    public static string rightArmPart;
    public static string legPart;

    private static int timeScore;//이거 스테이지 마다 다르게하기
    private static int collectScore;

    private void Awake()
    {
        operationLog = JsonReader.Read(PathManager.FindPath("OperationLog"));
        RWJsonCSV.SetCSVToJson(PathManager.FindPath("OperationSetting"));
        operationSetting = JsonReader.Read(PathManager.FindPath("OperationSetting"));
        maxLog = 10;//최대 기록 개수
        score = 0;
        kill = 0;
    }

    void Start()
    {
        
    }

    public static void GetParts()
    {
        corePart = GameManager.player.transform.Find("Hardwares/Parts/CorePart").GetChild(0).name.Substring(0, GameManager.player.transform.Find("Hardwares/Parts/CorePart").GetChild(0).name.IndexOf("(Clone)"));
        headPart = GameManager.player.transform.Find("Hardwares/Parts/HeadPart").GetChild(0).name.Substring(0, GameManager.player.transform.Find("Hardwares/Parts/HeadPart").GetChild(0).name.IndexOf("(Clone)"));
        bodyPart = GameManager.player.transform.Find("Hardwares/Parts/BodyPart").GetChild(0).name.Substring(0, GameManager.player.transform.Find("Hardwares/Parts/BodyPart").GetChild(0).name.IndexOf("(Clone)"));
        leftArmPart = GameManager.player.transform.Find("Hardwares/Parts/LeftArmPart").GetChild(0).name.Substring(0, GameManager.player.transform.Find("Hardwares/Parts/LeftArmPart").GetChild(0).name.IndexOf("(Clone)"));
        rightArmPart = GameManager.player.transform.Find("Hardwares/Parts/RightArmPart").GetChild(0).name.Substring(0, GameManager.player.transform.Find("Hardwares/Parts/RightArmPart").GetChild(0).name.IndexOf("(Clone)"));
        legPart = GameManager.player.transform.Find("Hardwares/Parts/LegPart").GetChild(0).name.Substring(0, GameManager.player.transform.Find("Hardwares/Parts/LegPart").GetChild(0).name.IndexOf("(Clone)"));
    }

    public static void KillScore(int score)//몹한테 적용
    {
        OperationLogManager.kill++;
        OperationLogManager.score += score;
    }

    // Update is called once per frame
    void Update()//타입 설정씬도 만들기?PlayerManager에###
    {
        
    }

    public static void WriteOperationLog(string operationName)//특정 숫자 넘기면 앞의 리스트 비우기
    {
        int i = 0;

        foreach (KeyValuePair<string, object> kv in operationLog[0])//나중에 이름 중복검사 할때도 확인하기
            i++;
        if (i > maxLog)//기록 개수 제한
        {
            List<Dictionary<string, object>> operationLogTemp = new List<Dictionary<string, object>>();
            Dictionary<string, object> temp = new Dictionary<string, object>();
            int j;
            for(int k = 0; k < operationLog.Count; k++)
            {
                j = 0;
                foreach (KeyValuePair<string, object> kv in operationLog[0])
                {
                    if (j == 0)
                    {
                        temp[kv.Key] = operationLog[k][kv.Key];
                    }
                    else if(j > i - maxLog)
                    {
                        temp[kv.Key] = operationLog[k][kv.Key];
                    }
                    j++;
                }
                operationLogTemp.Add(temp);
                temp = new Dictionary<string, object>();
            }
            operationLog = operationLogTemp;//변수 옮겨담기. 이름변경할때도 이런 방식 사용하기. 끝에부분을 temp에서 제외하고 다시 담기
        }

        if (operationName == null)
        {
            bool isCheck;
            i = 1;

            while(true)
            {
                isCheck = false;
                if (operationLog[0] != null)
                    foreach (KeyValuePair<string, object> kv in operationLog[0])//나중에 이름 중복검사 할때도 확인하기
                        if (kv.Key == "Operation" + i)
                        {
                            isCheck = true;
                            i++;
                        }

                if (!isCheck)
                    break;
            }

            operationName = "Operation" + i;
        }

        timeScore = int.Parse(operationSetting[0][zone].ToString());
        collectScore = int.Parse(operationSetting[1][zone].ToString());
        Debug.Log("1#");
        score += (int)(time * timeScore);
        score += collectLevel * collectScore;
        if(isClear)
            score += int.Parse(operationSetting[4][zone].ToString());

        operationLog[0][operationName] = zone;
        operationLog[1][operationName] = isClear;
        operationLog[2][operationName] = time;
        operationLog[3][operationName] = score;
        operationLog[4][operationName] = kill;
        operationLog[5][operationName] = collectLevel;
        operationLog[6][operationName] = corePart;
        operationLog[7][operationName] = headPart;
        operationLog[8][operationName] = bodyPart;
        operationLog[9][operationName] = leftArmPart;
        operationLog[10][operationName] = rightArmPart;
        operationLog[11][operationName] = legPart;
        Debug.Log("2#");

        JsonWriter.Write(operationLog, PathManager.FindPath("OperationLog"));
    }
}
