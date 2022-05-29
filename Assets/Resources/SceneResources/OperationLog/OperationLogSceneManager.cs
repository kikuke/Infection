using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class OperationLogSceneManager : MonoBehaviour
{
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
        loadScreen = transform.Find("Canvas/Load").gameObject;
        goldScreen = transform.Find("Canvas/Gold").gameObject;
        content = transform.Find("Canvas/SelectOperation/Viewport/Content").gameObject;
        operation = Resources.Load("SceneResources/OperationLog/Operation") as GameObject;
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
        Time.timeScale = 1f;
        if (Input.GetMouseButtonDown(0))
            coroutineTime = 0f;
    }
    //###대표할 로봇 선택 버튼도 만들기

    public void HelpButton()
    {
        MessageController.LogHelp();
    }

    public void SaveButton()
    {
        int i = 0;
        string save = operationName;

        string over = null;

        foreach (KeyValuePair<string, object> kv in OperationArchive.operationArchiveList[0])
            i++;

        if (MainInformationManager.Rank(false) >= 3 && i <= 10 && MainInformationManager.Gold(-1000) != -1)//나중에 현질 조건 달기, 최대 3개가능
        {
            i = 0;
            foreach (KeyValuePair<string, object> kv in OperationLogManager.operationLog[0])//나중에 이름 중복검사 할때도 확인하기
            {
                if (kv.Key == operationName)
                    break;
                i++;
            }

            List<Dictionary<string, object>> operationLogTemp = new List<Dictionary<string, object>>();
            Dictionary<string, object> temp = new Dictionary<string, object>();
            int j;
            for (int k = 0; k < OperationArchive.operationArchiveList.Count; k++)
            {
                foreach (KeyValuePair<string, object> kv in OperationArchive.operationArchiveList[0])
                {
                    if (kv.Key == operationName)
                        over += "O";//중복시 o추가
                    temp[kv.Key] = OperationArchive.operationArchiveList[k][kv.Key];
                }

                j = 0;
                foreach (KeyValuePair<string, object> kv in OperationLogManager.operationLog[0])
                {
                    if (j == i)
                    {
                        temp[kv.Key + over] = OperationLogManager.operationLog[k][kv.Key];//반복 되면 뒤에 단어 추가됨
                        break;
                    }
                    j++;
                }
                operationLogTemp.Add(temp);
                temp = new Dictionary<string, object>();
            }

            OperationArchive.operationArchiveList = operationLogTemp;
            JsonWriter.Write(OperationArchive.operationArchiveList, PathManager.FindPath("OperationArchive"));

            operationLogTemp = new List<Dictionary<string, object>>();
            temp = new Dictionary<string, object>();

            for (int k = 0; k < OperationLogManager.operationLog.Count; k++)
            {
                j = 0;
                foreach (KeyValuePair<string, object> kv in OperationLogManager.operationLog[0])
                {
                    if (j == 0)
                    {
                        temp[kv.Key] = OperationLogManager.operationLog[k][kv.Key];
                    }
                    else if (j != i)
                    {
                        temp[kv.Key] = OperationLogManager.operationLog[k][kv.Key];
                    }
                    j++;
                }
                operationLogTemp.Add(temp);
                temp = new Dictionary<string, object>();
            }

            OperationLogManager.operationLog = operationLogTemp;
            JsonWriter.Write(OperationLogManager.operationLog, PathManager.FindPath("OperationLog"));

            MainInform();
            SetOperationLog();

            selectOperation.SetActive(true);
            operationLog.SetActive(false);
        }
        else if(MainInformationManager.Rank(false) >= 3)
        {
            Debug.Log("보관함이 꽉 찼습니다.");
        }
        else
        {
            Debug.Log("권한 없음");
        }
    }

    public void DeleteButton()
    {
        int i = 0;
        string delete = operationName;
        if (MainInformationManager.Gold(-delgold) != -1)
        {
            if(MainInformationManager.Load(null) == operationName)
            {
                MainInformationManager.Load("null");
            }

            foreach (KeyValuePair<string, object> kv in OperationLogManager.operationLog[0])//나중에 이름 중복검사 할때도 확인하기
            {
                if (kv.Key == operationName)
                    break;
                i++;
            }

            List<Dictionary<string, object>> operationLogTemp = new List<Dictionary<string, object>>();
            Dictionary<string, object> temp = new Dictionary<string, object>();
            int j;
            for (int k = 0; k < OperationLogManager.operationLog.Count; k++)
            {
                j = 0;
                foreach (KeyValuePair<string, object> kv in OperationLogManager.operationLog[0])
                {
                    if (j == 0)
                    {
                        temp[kv.Key] = OperationLogManager.operationLog[k][kv.Key];
                    }
                    else if (j != i)
                    {
                        temp[kv.Key] = OperationLogManager.operationLog[k][kv.Key];
                    }
                    j++;
                }
                operationLogTemp.Add(temp);
                temp = new Dictionary<string, object>();
            }

            OperationLogManager.operationLog = operationLogTemp;
            JsonWriter.Write(OperationLogManager.operationLog, PathManager.FindPath("OperationLog"));

            MainInform();
            SetOperationLog();

            selectOperation.SetActive(true);
            operationLog.SetActive(false);
        }
        else
        {
            Debug.Log("돈없음");//창띄우던가 하기
        }
    }

    public void LoadButton()//점수에 비례해서 필요골드 올리기
    {
        string load = operationName;//나중에 이름 가져오기 버튼마다 인덱스 매겨서
        if(MainInformationManager.Gold(-logold) != -1)
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
        OperationLogSceneManager.operationName = operationName;
        string clear;
        if ((string)OperationLogManager.operationLog[1][operationName] == "True")
            clear = "성공";
        else
            clear = "실패";
        int score = (int)OperationLogManager.operationLog[3][operationName];
        int kill = (int)OperationLogManager.operationLog[4][operationName];
        int collectLevel = (int)OperationLogManager.operationLog[5][operationName];
        operationLog.transform.Find("Viewport/Content/Log/Zone").gameObject.GetComponent<Text>().text = "작전 구역 : " + OperationLogManager.operationLog[0][operationName];
        operationLog.transform.Find("Viewport/Content/Log/IsClear").gameObject.GetComponent<Text>().text = "클리어 : " + clear;
        operationLog.transform.Find("Viewport/Content/Log/Time").gameObject.GetComponent<Text>().text = "작전 시간 : " + (int)((float)OperationLogManager.operationLog[2][operationName]/60) + "m " + (int)((float)OperationLogManager.operationLog[2][operationName] % 60) + "s";
        operationLog.transform.Find("Viewport/Content/Log/Score").gameObject.GetComponent<Text>().text = "종합 점수 : " + score.ToString("N0");
        operationLog.transform.Find("Viewport/Content/Log/Kill").gameObject.GetComponent<Text>().text = "사살한 적 : " + kill.ToString("N0");
        operationLog.transform.Find("Viewport/Content/Log/CollectLevel").gameObject.GetComponent<Text>().text = "수집 레벨 : " + collectLevel.ToString("N0");

        logold = 300 + (int)(0.01f * (int)OperationLogManager.operationLog[3][operationName]);
        if ((string)OperationLogManager.operationLog[0][operationName] == "최종 미션: 숙주 사냥")
            logold *= 20;
        loadGold.GetComponent<Text>().text = "불러오기\n" + logold.ToString("N0") + " Gold";
        delgold = (100 * MainInformationManager.Rank(false));
        deleteGold.GetComponent<Text>().text = "기록삭제\n" + delgold.ToString("N0") + " Gold";
    }

    void SetOperationLog()
    {
        for(int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        GameObject opTemp;
        foreach (KeyValuePair<string, object> kv in OperationLogManager.operationLog[0])//나중에 이름 중복검사 할때도 확인하기
        {
            if(kv.Key != "Option")
            {
                opTemp = Instantiate(operation, content.transform) as GameObject;
                opTemp.GetComponent<OperationController>().OperationName(kv.Key);
                opTemp.GetComponent<OperationController>().Time((float)OperationLogManager.operationLog[2][kv.Key]);
                opTemp.GetComponent<OperationController>().Score((int)OperationLogManager.operationLog[3][kv.Key]);
            }
        }
    }

    void MainInform()
    {
        coroutineTime = 0.01f;
        if(MainInformationManager.Load(null) != "null")
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

    public void ArchiveButton()
    {
        SceneManager.LoadScene("OperationArchive");
    }

    public void MainButton()
    {
        SceneManager.LoadScene("Main");
    }
}
