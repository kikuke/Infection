using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OperationTerminate : MonoBehaviour
{
    List<Dictionary<string, object>> operationLog;
    List<Dictionary<string, object>> operationSetting;

    GameObject scoreScreen;
    GameObject timeScreen;
    GameObject expScreen;
    GameObject goldScreen;

    GameObject corepart;
    GameObject headpart;
    GameObject bodypart;
    GameObject leftarmpart;
    GameObject rightarmpart;
    GameObject legpart;

    bool isnaming;

    private GameObject namingOperation;
    private InputField inputField;

    GameObject noticeSave;

    string operationName;
    string zone;
    float time;
    int score;
    float exp;
    int gold;

    float coroutineTime;
    float coroutineWaitTime;
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1f;
        operationLog = JsonReader.Read(PathManager.FindPath("OperationLog"));
        RWJsonCSV.SetCSVToJson(PathManager.FindPath("OperationSetting"));
        operationSetting = JsonReader.Read(PathManager.FindPath("OperationSetting"));
        scoreScreen = transform.Find("Canvas/Score").gameObject;
        timeScreen = transform.Find("Canvas/Time").gameObject;
        expScreen = transform.Find("Canvas/Exp").gameObject;
        goldScreen = transform.Find("Canvas/Gold").gameObject;

        corepart = transform.Find("Canvas/Robot/Parts/CorePart").gameObject;
        headpart = transform.Find("Canvas/Robot/Parts/HeadPart").gameObject;
        bodypart = transform.Find("Canvas/Robot/Parts/BodyPart").gameObject;
        leftarmpart = transform.Find("Canvas/Robot/Parts/LeftArmPart").gameObject;
        rightarmpart = transform.Find("Canvas/Robot/Parts/RightArmPart").gameObject;
        legpart = transform.Find("Canvas/Robot/Parts/LegPart").gameObject;

        isnaming = false;
        namingOperation = GameObject.Find("NamingOperation").gameObject;
        inputField = namingOperation.transform.Find("InputField").GetComponent<InputField>();
        namingOperation.SetActive(false);

        noticeSave = GameObject.Find("NoticeSave").gameObject;
        noticeSave.SetActive(false);

        foreach (KeyValuePair<string, object> kv in operationLog[0])//나중에 이름 중복검사 할때도 확인하기
            operationName = kv.Key;

        zone = (string)operationLog[0][operationName];
        
        time = (float)operationLog[2][operationName];
        score = (int)operationLog[3][operationName];
        //나중에 csv만들어서 저장하기
        exp = score * (float)operationSetting[2][zone];
        gold = (int)(score * (float)operationSetting[3][zone]);
        MainInformationManager.Score(score);
        MainInformationManager.Exp(exp);
        MainInformationManager.Gold(gold);

        SetRobot();

        StartCoroutine(OperationCompute());
    }

    void Update()
    {
        Time.timeScale = 1f;
        if (Input.GetMouseButtonDown(0))
            coroutineTime = 0f;
    }

    public void NoticeSaveButton()
    {
        noticeSave.SetActive(false);
    }
    
    void SetRobot()
    {
        GameObject temp = Resources.Load(HardwareManager.FindPart((string)operationLog[6][operationName])) as GameObject;
        corepart.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
        temp = Resources.Load(HardwareManager.FindPart((string)operationLog[7][operationName])) as GameObject;
        headpart.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
        temp = Resources.Load(HardwareManager.FindPart((string)operationLog[8][operationName])) as GameObject;
        bodypart.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
        temp = Resources.Load(HardwareManager.FindPart((string)operationLog[9][operationName])) as GameObject;
        leftarmpart.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
        temp = Resources.Load(HardwareManager.FindPart((string)operationLog[10][operationName])) as GameObject;
        rightarmpart.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
        temp = Resources.Load(HardwareManager.FindPart((string)operationLog[11][operationName])) as GameObject;
        legpart.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
    }

    IEnumerator OperationCompute()
    {
        coroutineTime = 0.03f;
        StartCoroutine(ShowString(scoreScreen, "Score\n" + score.ToString("N0")));
        yield return new WaitForSeconds(coroutineWaitTime);
        StartCoroutine(ShowString(timeScreen, "Time\n" + (int)(time / 60) + "m " + (int)(time % 60) + "s"));
        yield return new WaitForSeconds(coroutineWaitTime);
        StartCoroutine(ShowString(expScreen, "Exp\n+" + exp.ToString("N0")));
        yield return new WaitForSeconds(coroutineWaitTime);
        StartCoroutine(ShowString(goldScreen, "Gold\n+" + gold.ToString("N0")));
        yield return new WaitForSeconds(coroutineWaitTime);
        coroutineTime = 0f;
    }

    IEnumerator ShowString(GameObject screen, string text)
    {
        screen.GetComponent<Text>().text = null;
        coroutineWaitTime = text.Length * coroutineTime + 0.7f;
        if (coroutineTime == 0)
            coroutineWaitTime = 0f;

        for (int i = 0; i < text.Length; i++)
        {
            screen.GetComponent<Text>().text += text.Substring(i, 1);
            yield return new WaitForSeconds(coroutineTime);
        }
    }

    public void MainButton()
    {
         SceneManager.LoadScene("Main");
    }

    public void NamingOperationButton()
    {
        if (!isnaming)
        {
            namingOperation.SetActive(true);
            inputField.text = null;
            isnaming = true;
        }
    }

    public void ChangeNameButton()
    {
        if (isnaming)
        {
            ChangeName(inputField.text);
            namingOperation.SetActive(false);
            isnaming = false;
        }
    }

    private void ChangeName(string operationName)
    {
        if(operationName == null)
            return;
        this.operationName = operationName;
        Debug.Log(operationName);
        if(operationName != null)
        {
            int i = 0;
            foreach (KeyValuePair<string, object> kv in OperationArchive.operationArchiveList[0])//나중에 이름 중복검사 할때도 확인하기
            {
                if (kv.Key == operationName)
                    operationName += "O";
                i++;
            }
            i = 0;
            foreach (KeyValuePair<string, object> kv in operationLog[0])//나중에 이름 중복검사 할때도 확인하기
            {
                if (kv.Key == operationName)
                    operationName += "O";
                i++;
            }

            List<Dictionary<string, object>> operationLogTemp = new List<Dictionary<string, object>>();
            Dictionary<string, object> temp = new Dictionary<string, object>();
            int j;
            for (int k = 0; k < operationLog.Count; k++)
            {
                j = 0;
                foreach (KeyValuePair<string, object> kv in operationLog[0])
                {
                    if (j != i-1)
                    {
                        temp[kv.Key] = operationLog[k][kv.Key];
                    }
                    else if (j == i-1)
                    {
                        temp[operationName] = operationLog[k][kv.Key];
                    }
                    j++;
                }
                operationLogTemp.Add(temp);
                temp = new Dictionary<string, object>();
            }
            operationLog = operationLogTemp;//변수 옮겨담기. 이름변경할때도 이런 방식 사용하기. 끝에부분을 temp에서 제외하고 다시 담기
            JsonWriter.Write(operationLog, PathManager.FindPath("OperationLog"));
        }
    }
}
