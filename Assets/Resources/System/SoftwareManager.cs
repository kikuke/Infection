using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftwareManager : MonoBehaviour
{//소프트웨어 레벨 만들기
    public static GameObject softwares;
    public static GameObject satellites;
    public static float softwareMoveSpeed;
    public static float softwaremaxHp;
    public static float softwaredefend;
    public static float softwareDamage;
    public static float softwarecooltime;
    public static float softwareAtkRange;
    public static float softwarecooltimePer;
    public static float softwareaddDamPer;
    public static float softwareaddAtkSpPer;
    public static float softwareaddAtkRangePer;
    public static float softwareaddMvSpPer;
    public static float softwareaddDefendPer;
    public static float softwareaddMaxHpPer;
    public static int softwareDroneNum;

    // Start is called before the first frame update
    void Awake()//대부분의 소프트웨어는 start로 작동하고 아래의 변수를 더함. 위성의 경우는 개별작동
    {
        softwares = GameManager.player.transform.Find("Softwares").gameObject;
        satellites = softwares.transform.Find("Satellites").gameObject;
        SetBaseValue();
        SetBaseSoftware();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetBaseSoftware()
    {
        if(MainInformationManager.Software(null) != "null")
        {
            Debug.Log(MainInformationManager.Software(null));
            SupplySoftwareSystem.BaseSoft(MainInformationManager.Software(null));
            MainInformationManager.Software("null");
        }
    }

    static void SetBaseValue()
    {
        softwareMoveSpeed = 0;
        softwaremaxHp = 0;
        softwaredefend = 0;
        softwareDamage = 0;
        softwarecooltime = 0;
        softwareAtkRange = 0;
        softwarecooltimePer = 0;
        softwareaddDamPer = 0;
        softwareaddAtkSpPer = 0;
        softwareaddAtkRangePer = 0;
        softwareaddMvSpPer = 0;
        softwareaddDefendPer = 0;
        softwareaddMaxHpPer = 0;
        softwareDroneNum = 0;
    }

    public static void RestartSoftware()//소프트웨어 값들 초기화시킴
    {//파츠강화같은경우는 어떻게 처리??
        SetBaseValue();
        for (int i = 0; i < softwares.transform.childCount; i++)
        {
            if(softwares.transform.GetChild(i).name != "Satellites")
            {
                softwares.transform.GetChild(i).GetComponent<Software>().SetUp();//여기서 위의 능력치 및 파츠 능력치들을 초기화함.
            }
        }
        for (int i = 0; i < satellites.transform.childCount; i++)
        {
            satellites.transform.GetChild(i).GetComponent<Software>().SetUp();
        }
    }

    public static void AddStat()
    {
        for (int i = 0; i < softwares.transform.childCount; i++)
        {
            if (softwares.transform.GetChild(i).name != "Satellites")
            {
                softwares.transform.GetChild(i).GetComponent<Software>().AddStat();
            }
        }
        for (int i = 0; i < satellites.transform.childCount; i++)
        {
            satellites.transform.GetChild(i).GetComponent<Software>().AddStat();
        }
    }
}
