using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInformationManager : MonoBehaviour
{
    public static List<Dictionary<string, object>> mainInformationlist;
    public static List<Dictionary<string, object>> partsInformationlist;
    // Start is called before the first frame update
    private void Awake()
    {
        mainInformationlist = JsonReader.Read(PathManager.FindPath("MainInformation"));
        partsInformationlist = JsonReader.Read(PathManager.FindPath("PartsInformation"));
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string Software(string software)
    {
        if (software == null)
        {
            if ((string)mainInformationlist[12]["Player"] == "null")
            {
                return "null";//스트링 널값임###
            }
            else
                return (string)mainInformationlist[12]["Player"];
        }
        else
        {
            mainInformationlist[12]["Player"] = software;
            JsonWriter.Write(mainInformationlist, PathManager.FindPath("MainInformation"));
            return (string)mainInformationlist[12]["Player"];
        }
    }

    public static int Rank(bool isedit)//true일경우 레벨업
    {
        if(isedit)
        {
            if ((int)mainInformationlist[0]["Player"] == 2)
            {
                if((string)MainInformationManager.mainInformationlist[13]["Player"] == "TRUE")
                {
                    mainInformationlist[0]["Player"] = int.Parse(mainInformationlist[0]["Player"].ToString()) + 1;
                    JsonWriter.Write(mainInformationlist, PathManager.FindPath("MainInformation"));
                }
            }
            else if((int)mainInformationlist[0]["Player"] == 4)
            {
                if ((string)MainInformationManager.mainInformationlist[14]["Player"] == "TRUE")
                {
                    mainInformationlist[0]["Player"] = int.Parse(mainInformationlist[0]["Player"].ToString()) + 1;
                    JsonWriter.Write(mainInformationlist, PathManager.FindPath("MainInformation"));
                }
            }
            else
            {
                mainInformationlist[0]["Player"] = int.Parse(mainInformationlist[0]["Player"].ToString()) + 1;
                JsonWriter.Write(mainInformationlist, PathManager.FindPath("MainInformation"));
            }
        }

        return int.Parse(mainInformationlist[0]["Player"].ToString());
    }

    public static float Exp(float getExp)//true일경우 레벨업
    {
        if (getExp != 0)
        {
            mainInformationlist[1]["Player"] = float.Parse(mainInformationlist[1]["Player"].ToString()) + getExp;

            while(float.Parse(mainInformationlist[1]["Player"].ToString()) >= float.Parse(mainInformationlist[2]["Player"].ToString()))
            {
                Rank(true);
                mainInformationlist[1]["Player"] = float.Parse(mainInformationlist[1]["Player"].ToString()) - float.Parse(mainInformationlist[2]["Player"].ToString());
                mainInformationlist[2]["Player"] = float.Parse(mainInformationlist[2]["Player"].ToString()) * 2f;//임시조치 경험치 획득량 증가
            }

            JsonWriter.Write(mainInformationlist, PathManager.FindPath("MainInformation"));
        }

        return float.Parse(mainInformationlist[1]["Player"].ToString());
    }

    public static int Gold(int getGold)//true일경우 레벨업
    {
        if (getGold != 0 && (int.Parse(mainInformationlist[3]["Player"].ToString()) + getGold) >= 0)
        {
            mainInformationlist[3]["Player"] = int.Parse(mainInformationlist[3]["Player"].ToString()) + getGold;
            JsonWriter.Write(mainInformationlist, PathManager.FindPath("MainInformation"));
        }
        else if (getGold != 0 && (int.Parse(mainInformationlist[3]["Player"].ToString()) + getGold) < 0)
        {
            MessageController.LowGold();
            return -1;//돈이 적은경우
        }
        return int.Parse(mainInformationlist[3]["Player"].ToString());
    }

    public static int Score(int getScore)//true일경우 레벨업
    {
        if (getScore != 0 && getScore > int.Parse(mainInformationlist[4]["Player"].ToString()))//점수갱신
        {
            mainInformationlist[4]["Player"] = getScore;
            JsonWriter.Write(mainInformationlist, PathManager.FindPath("MainInformation"));
        }

        return int.Parse(mainInformationlist[4]["Player"].ToString());
    }

    public static string Load(string operationName)
    {
        if(operationName == null)
        {
            if ((string)mainInformationlist[11]["Player"] == "null")
            {
                return "null";//스트링 널값임###
            }
            else
                return (string)mainInformationlist[11]["Player"];
        }
        else
        {
            mainInformationlist[11]["Player"] = operationName;
            JsonWriter.Write(mainInformationlist, PathManager.FindPath("MainInformation"));
            return (string)mainInformationlist[11]["Player"];
        }
    }
}
