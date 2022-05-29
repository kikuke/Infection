using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardwareManager : MonoBehaviour
{//드론작동 만들기 소프트웨어 없을땐 소유는 할 수 있음
    public static float hardwareMoveSpeed;
    public static float hardwaremaxHp;
    public static float hardwaredefend;
    public static float hardwareDamage;
    public static float hardwarecooltime;
    public static float hardwareAtkRange;
    public static float hardwarecooltimePer;
    public static float hardwareaddDamPer;
    public static float hardwareaddAtkSpPer;
    public static float hardwareaddAtkRangePer;
    public static float hardwareaddMvSpPer;
    public static float hardwareaddDefendPer;
    public static float hardwareaddMaxHpPer;
    public static int hardwareDroneNum;
    private void Awake()
    {
        GameManager.player.GetComponent<PlayerController>().dronepart = new GameObject[3];
        GameManager.player.GetComponent<PlayerController>().SetVariables();
        SetBaseParts();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void HardwareStat()
    {
        hardwareMoveSpeed = 0;
        hardwaremaxHp = 0;
        hardwaredefend = 0;
        hardwareDamage = 0;
        hardwarecooltime = 0;
        hardwareAtkRange = 0;
        hardwarecooltimePer = 0;
        hardwareaddDamPer = 0;
        hardwareaddAtkSpPer = 0;
        hardwareaddAtkRangePer = 0;
        hardwareDroneNum = 0;
        hardwareaddMvSpPer = 0;
        hardwareaddMaxHpPer = 0;
        hardwareaddDefendPer = 0;
        GameManager.player.GetComponent<PlayerController>().legpart.transform.GetComponentInChildren<Part>().Stat();//이런식으로 각파트에서 능력치 모두 초기화 해서 플레이어 주기.
        GameManager.player.GetComponent<PlayerController>().headpart.transform.GetComponentInChildren<Part>().Stat();
        GameManager.player.GetComponent<PlayerController>().bodypart.transform.GetComponentInChildren<Part>().Stat();
        GameManager.player.GetComponent<PlayerController>().corepart.transform.GetComponentInChildren<Part>().Stat();
        GameManager.player.GetComponent<PlayerController>().leftarmpart.transform.GetComponentInChildren<Part>().Stat();
        GameManager.player.GetComponent<PlayerController>().rightarmpart.transform.GetComponentInChildren<Part>().Stat();//드론도 나중에 추가하기
        for (int i = 0; i < GameManager.player.GetComponent<PlayerController>().dronepart.Length; i++)
        {
            if (GameManager.player.GetComponent<PlayerController>().dronepart[i].transform.childCount != 0)
            {
                GameManager.player.GetComponent<PlayerController>().dronepart[i].transform.GetComponentInChildren<Part>().Stat();
            }
        }
    }

    private void SetBaseParts()
    {
        string loadOperation = MainInformationManager.Load(null);
        bool isArchive = false;
        foreach (KeyValuePair<string, object> kv in OperationArchive.operationArchiveList[0])
        {
            if (kv.Key == loadOperation)
                isArchive = true;
        }

        if(OperationLogManager.zone == "최종 미션 : 숙주 사냥")
        {
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().legpart, InstantiatePart(FindPart((string)MainInformationManager.mainInformationlist[10]["Player"])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().headpart, InstantiatePart(FindPart((string)MainInformationManager.mainInformationlist[6]["Player"])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().bodypart, InstantiatePart(FindPart((string)MainInformationManager.mainInformationlist[7]["Player"])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().corepart, InstantiatePart(FindPart((string)MainInformationManager.mainInformationlist[5]["Player"])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().leftarmpart, InstantiatePart(FindPart((string)MainInformationManager.mainInformationlist[8]["Player"])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().rightarmpart, InstantiatePart(FindPart((string)MainInformationManager.mainInformationlist[9]["Player"])));
        }
        else if (loadOperation == "null")
        {
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().legpart, InstantiatePart(FindPart("BaseLeg")));//교체할 때는 클릭한걸로 하기
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().headpart, InstantiatePart(FindPart("BaseHead")));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().bodypart, InstantiatePart(FindPart("BaseBody")));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().corepart, InstantiatePart(FindPart("BaseCore")));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().leftarmpart, InstantiatePart(FindPart("BaseArm")));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().rightarmpart, InstantiatePart(FindPart("BaseArm")));
        }
        else if(isArchive)
        {
            MainInformationManager.Load("null");//눌값으로 저장후 초기화
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().legpart, InstantiatePart(FindPart((string)OperationArchive.operationArchiveList[11][loadOperation])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().headpart, InstantiatePart(FindPart((string)OperationArchive.operationArchiveList[7][loadOperation])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().bodypart, InstantiatePart(FindPart((string)OperationArchive.operationArchiveList[8][loadOperation])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().corepart, InstantiatePart(FindPart((string)OperationArchive.operationArchiveList[6][loadOperation])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().leftarmpart, InstantiatePart(FindPart((string)OperationArchive.operationArchiveList[9][loadOperation])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().rightarmpart, InstantiatePart(FindPart((string)OperationArchive.operationArchiveList[10][loadOperation])));
        }
        else
        {
            MainInformationManager.Load("null");//눌값으로 저장후 초기화
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().legpart, InstantiatePart(FindPart((string)OperationLogManager.operationLog[11][loadOperation])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().headpart, InstantiatePart(FindPart((string)OperationLogManager.operationLog[7][loadOperation])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().bodypart, InstantiatePart(FindPart((string)OperationLogManager.operationLog[8][loadOperation])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().corepart, InstantiatePart(FindPart((string)OperationLogManager.operationLog[6][loadOperation])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().leftarmpart, InstantiatePart(FindPart((string)OperationLogManager.operationLog[9][loadOperation])));
            GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().rightarmpart, InstantiatePart(FindPart((string)OperationLogManager.operationLog[10][loadOperation])));
        }
    }

    public static void AddStat()
    {
        GameManager.player.GetComponent<PlayerController>().legpart.transform.GetComponentInChildren<Part>().AddStat();
        GameManager.player.GetComponent<PlayerController>().headpart.transform.GetComponentInChildren<Part>().AddStat();
        GameManager.player.GetComponent<PlayerController>().bodypart.transform.GetComponentInChildren<Part>().AddStat();
        GameManager.player.GetComponent<PlayerController>().corepart.transform.GetComponentInChildren<Part>().AddStat();
        GameManager.player.GetComponent<PlayerController>().leftarmpart.transform.GetComponentInChildren<Part>().AddStat();
        GameManager.player.GetComponent<PlayerController>().rightarmpart.transform.GetComponentInChildren<Part>().AddStat();
        for(int i = GameManager.player.GetComponent<PlayerController>().maxDroneNum; i > GameManager.player.GetComponent<PlayerController>().droneNum; i--)
        {
            if(GameManager.player.GetComponent<PlayerController>().dronepart[i - 1].transform.childCount != 0)
            {
                Destroy(GameManager.player.GetComponent<PlayerController>().dronepart[i - 1].transform.GetChild(i - 1).gameObject);
            }
        }

        for (int i = 0; i < GameManager.player.GetComponent<PlayerController>().dronepart.Length; i++)
        {
            if (GameManager.player.GetComponent<PlayerController>().dronepart[i].transform.childCount != 0)
            {
                GameManager.player.GetComponent<PlayerController>().dronepart[i].transform.GetComponentInChildren<Part>().AddStat();
            }
        }
    }

    private GameObject InstantiatePart(string path)
    {
        return Instantiate(Resources.Load(path)) as GameObject;
    }

    public static string FindPart(string partname)//원래 private였음
    {
        string path;
        int i;
        for (i = 0; i < GlobalManager.hardwarelist.Count; i++)
        {
            if ((string)GlobalManager.hardwarelist[i]["Option"] == "Part")
            {
                path = "System/Hardware/Parts/" + (string)GlobalManager.hardwarelist[i][partname] + "Parts/" + partname + "/" + partname;

                //Debug.Log(path);

                return path;
            }
        }

        return null;
    }
}
