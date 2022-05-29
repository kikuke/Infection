using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreComputeManager : MonoBehaviour
{
    List<Dictionary<string, object>> operationLog;
    List<Dictionary<string, object>> operationSetting;

    GameObject timeScreen;
    GameObject killScreen;
    GameObject supplyScreen;
    GameObject clearScreen;
    GameObject scoreScreen;

    string operationName;

    string zone;
    bool isClear;
    float time;
    int score;
    int kill;
    int collectLevel;

    int timeScore;
    int collectScore;
    int clearScore;
    int killScore;

    float coroutineTime;
    float coroutineWaitTime;
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1f;
        operationLog = JsonReader.Read(PathManager.FindPath("OperationLog"));
        operationSetting = JsonReader.Read(PathManager.FindPath("OperationSetting"));

        timeScreen = transform.Find("Canvas/Time").gameObject;
        killScreen = transform.Find("Canvas/Kill").gameObject;
        supplyScreen = transform.Find("Canvas/Supply").gameObject;
        clearScreen = transform.Find("Canvas/Clear").gameObject;
        scoreScreen = transform.Find("Canvas/Score").gameObject;

        foreach (KeyValuePair<string, object> kv in operationLog[0])//나중에 이름 중복검사 할때도 확인하기
            operationName = kv.Key;

        zone = (string)operationLog[0][operationName];

        if ((string)operationLog[1][operationName] == "True")
            isClear = true;
        else
            isClear = false;

        time = (float)operationLog[2][operationName];
        score = (int)operationLog[3][operationName];
        kill = (int)operationLog[4][operationName];
        collectLevel = (int)operationLog[5][operationName];

        timeScore = (int)operationSetting[0][zone];
        collectScore = (int)operationSetting[1][zone];
        clearScore = (int)operationSetting[4][zone];
        Debug.Log((string)operationLog[1][operationName]);
        if (isClear)
            killScore = score - (int)(time * timeScore + collectLevel * collectScore + clearScore);
        else
            killScore = score - (int)(time * timeScore + collectLevel * collectScore);
    }
    void Start()
    {
        StartCoroutine(ScoreCompute());
    }

    private void Update()
    {
        Time.timeScale = 1f;
        if (Input.GetMouseButtonDown(0))
            if (coroutineTime != 0)
            {
                coroutineTime = 0;
                coroutineWaitTime = 0;
            }
    }

    IEnumerator ScoreCompute()
    {
        coroutineTime = 0.03f;
        StartCoroutine(ShowString(timeScreen, "작전 시간 : " + (int)(time/60) + "m " + (int)(time%60) + "s\n시간 점수 : +" + (int)(time * timeScore)));
        yield return new WaitForSeconds(coroutineWaitTime);
        StartCoroutine(ShowString(killScreen, "사살한 적 : " + kill.ToString("N0") + "\n사살 점수 : +" + killScore.ToString("N0")));
        yield return new WaitForSeconds(coroutineWaitTime);
        StartCoroutine(ShowString(supplyScreen, "수집 레벨 : " + collectLevel.ToString("N0") + "\n수집 점수 : +" + (collectLevel * collectScore).ToString("N0")));
        yield return new WaitForSeconds(coroutineWaitTime);
        if(isClear)
            StartCoroutine(ShowString(clearScreen, "작전결과 : 성공\n작전 점수 : +" + clearScore.ToString("N0")));
        else
            StartCoroutine(ShowString(clearScreen, "작전결과 : 실패\n작전 점수 : +0"));
        yield return new WaitForSeconds(coroutineWaitTime);
        StartCoroutine(ShowString(scoreScreen, "종합 평가 점수\n+" + score.ToString("N0")));
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

    public void Button()
    {
        if (coroutineTime == 0)
            SceneManager.LoadScene("OperationTerminate");
    }
}
