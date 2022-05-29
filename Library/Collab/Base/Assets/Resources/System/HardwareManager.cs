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
    public static int hardwareDroneNum;
    private void Awake()
    {
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
        GameManager.player.GetComponent<PlayerController>().legpart.transform.GetComponentInChildren<Part>().Stat();//이런식으로 각파트에서 능력치 모두 초기화 해서 플레이어 주기.
        GameManager.player.GetComponent<PlayerController>().headpart.transform.GetComponentInChildren<Part>().Stat();
        GameManager.player.GetComponent<PlayerController>().bodypart.transform.GetComponentInChildren<Part>().Stat();
        GameManager.player.GetComponent<PlayerController>().corepart.transform.GetComponentInChildren<Part>().Stat();
        GameManager.player.GetComponent<PlayerController>().leftarmpart.transform.GetComponentInChildren<Part>().Stat();
        GameManager.player.GetComponent<PlayerController>().rightarmpart.transform.GetComponentInChildren<Part>().Stat();//드론도 나중에 추가하기
    }

    private void SetBaseParts()
    {
        GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().legpart, InstantiatePart(FindPart("BaseLeg")));//교체할 때는 클릭한걸로 하기
        GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().headpart, InstantiatePart(FindPart("BaseHead")));
        GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().bodypart, InstantiatePart(FindPart("BaseBody")));
        GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().corepart, InstantiatePart(FindPart("BaseCore")));
        GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().leftarmpart, InstantiatePart(FindPart("BaseArm")));
        GameManager.player.GetComponent<PlayerController>().MountPart(GameManager.player.GetComponent<PlayerController>().rightarmpart, InstantiatePart(FindPart("BaseArm")));
        //드론도 추가후 암은 왼쪽 오른쪽 구분하기
    }

    private GameObject InstantiatePart(string path)
    {
        return Instantiate(Resources.Load(path)) as GameObject;
    }

    private string FindPart(string partname)
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
