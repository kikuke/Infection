using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class OperationArchiveManager : MonoBehaviour
{
    public static List<Dictionary<string, object>> operationArchiveList;

    GameObject loadScreen;
    GameObject goldScreen;

    GameObject content;
    GameObject operation;

    public static string operationName;

    public static GameObject selectOperation;
    public static GameObject operationLog;

    static int logold;
    static int delgold;

    static GameObject loadGold;
    static GameObject deleteGold;

    float coroutineTime;

    private void Awake()
    {
        Time.timeScale = 1f;
        operationArchiveList = OperationArchive.operationArchiveList;
        loadScreen = transform.Find("Canvas/Load").gameObject;
        goldScreen = transform.Find("Canvas/Gold").gameObject;
        content = transform.Find("Canvas/SelectOperation/Viewport/Content").gameObject;
        operation = Resources.Load("SceneResources/OperationArchive/ArchiveOperation") as GameObject;
        selectOperation = transform.Find("Canvas/SelectOperation").gameObject;
        operationLog = transform.Find("Canvas/OperationLog").gameObject;
        operationLog.SetActive(false);
        loadGold = operationLog.transform.Find("Viewport/Content/ControlButton/LoadButton/Text").gameObject;
        deleteGold = operationLog.transform.Find("Viewport/Content/ControlButton/DeleteButton/Text").gameObject;

        MainInform();
        SetOperationLog();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    //나오면 화면창 초기화 한번 해주기 함수 만들어서.

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            coroutineTime = 0f;
    }
    //###대표할 로봇 선택 버튼도 만들기

    public void DeleteButton()
    {
        int i = 0;
        string delete = operationName;
        if (MainInformationManager.Load(null) == operationName)
        {
            MainInformationManager.Load("null");
        }

        foreach (KeyValuePair<string, object> kv in operationArchiveList[0])//나중에 이름 중복검사 할때도 확인하기
        {
            if (kv.Key == operationName)
                break;
            i++;
        }

        List<Dictionary<string, object>> operationLogTemp = new List<Dictionary<string, object>>();
        Dictionary<string, object> temp = new Dictionary<string, object>();
        int j;
        for (int k = 0; k < operationArchiveList.Count; k++)
        {
            j = 0;
            foreach (KeyValuePair<string, object> kv in operationArchiveList[0])
            {
                if (j == 0)
                {
                    temp[kv.Key] = operationArchiveList[k][kv.Key];
                }
                else if (j != i)
                {
                    temp[kv.Key] = operationArchiveList[k][kv.Key];
                }
                j++;
            }
            operationLogTemp.Add(temp);
            temp = new Dictionary<string, object>();
        }

        operationArchiveList = operationLogTemp;
        JsonWriter.Write(operationArchiveList, PathManager.FindPath("OperationArchive"));

        MainInform();
        SetOperationLog();

        selectOperation.SetActive(true);
        operationLog.SetActive(false);
        
    }

    public void LoadButton()//점수에 비례해서 필요골드 올리기
    {
        string load = operationName;//나중에 이름 가져오기 버튼마다 인덱스 매겨서
        if (MainInformationManager.Gold(-logold) != -1)
        {
            MainInformationManager.Load(load);
            MainInform();
        }
        else
        {
            Debug.Log("돈없음");//창띄우던가 하기
        }
    }

    public void CancelButton()
    {
        selectOperation.SetActive(true);
        operationLog.SetActive(false);
    }

    public static void OperationLog(string operationName)
    {
        OperationArchiveManager.operationName = operationName;
        string clear;
        if ((string)operationArchiveList[1][operationName] == "True")
            clear = "성공";
        else
            clear = "실패";
        int score = (int)operationArchiveList[3][operationName];
        int kill = (int)operationArchiveList[4][operationName];
        int collectLevel = (int)operationArchiveList[5][operationName];
        operationLog.transform.Find("Viewport/Content/Log/Zone").gameObject.GetComponent<Text>().text = "작전 구역 : " + operationArchiveList[0][operationName];
        operationLog.transform.Find("Viewport/Content/Log/IsClear").gameObject.GetComponent<Text>().text = "클리어 : " + clear;
        operationLog.transform.Find("Viewport/Content/Log/Time").gameObject.GetComponent<Text>().text = "작전 시간 : " + (int)((float)operationArchiveList[2][operationName] / 60) + "m " + (int)((float)operationArchiveList[2][operationName] % 60) + "s";
        operationLog.transform.Find("Viewport/Content/Log/Score").gameObject.GetComponent<Text>().text = "종합 점수 : " + score.ToString("N0");
        operationLog.transform.Find("Viewport/Content/Log/Kill").gameObject.GetComponent<Text>().text = "사살한 적 : " + kill.ToString("N0");
        operationLog.transform.Find("Viewport/Content/Log/CollectLevel").gameObject.GetComponent<Text>().text = "수집 레벨 : " + collectLevel.ToString("N0");

        logold = 300 + (int)(0.01f * (int)operationArchiveList[3][operationName]);
        if ((string)operationArchiveList[0][operationName] == "최종 미션: 숙주 사냥")
            logold *= 20;
        loadGold.GetComponent<Text>().text = "불러오기\n" + logold.ToString("N0") + " Gold";
        delgold = (100 * MainInformationManager.Rank(false));
        deleteGold.GetComponent<Text>().text = "기록삭제";
    }

    void SetOperationLog()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        GameObject opTemp;
        foreach (KeyValuePair<string, object> kv in operationArchiveList[0])//나중에 이름 중복검사 할때도 확인하기
        {
            if (kv.Key != "Option")
            {
                opTemp = Instantiate(operation, content.transform) as GameObject;
                opTemp.GetComponent<ArchiveOperationController>().OperationName(kv.Key);
                opTemp.GetComponent<ArchiveOperationController>().Time((float)operationArchiveList[2][kv.Key]);
                opTemp.GetComponent<ArchiveOperationController>().Score((int)operationArchiveList[3][kv.Key]);
            }
        }
    }

    void MainInform()
    {
        coroutineTime = 0.01f;
        if (MainInformationManager.Load(null) != "null")
            StartCoroutine(ShowString(loadScreen, "Load\n" + MainInformationManager.Load(null)));
        else
            StartCoroutine(ShowString(loadScreen, "Load\n미선택"));
        StartCoroutine(ShowString(goldScreen, "Gold\n" + MainInformationManager.Gold(0).ToString("N0")));
        coroutineTime = 0f;
    }

    IEnumerator ShowString(GameObject screen, string text)
    {
        screen.GetComponent<Text>().text = null;

        for (int i = 0; i < text.Length; i++)
        {
            screen.GetComponent<Text>().text += text.Substring(i, 1);
            yield return new WaitForSeconds(coroutineTime);
        }
    }

    public void LogButton()
    {
        SceneManager.LoadScene("OperationLog");
    }
}
