using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    static bool isOp;
    static bool isStop;

    GameObject timer;
    Text timerText;

    static GameObject gameClear;
    static GameObject gameOver;

    string gT;
    string cT;
    int gTmin;
    int cTmin;

    private void Awake()
    {
        timer = transform.Find("Timer/Text").gameObject;
        timerText = timer.GetComponent<Text>();
        gameClear = transform.Find("GameClear").gameObject;
        gameOver = transform.Find("GameOver").gameObject;
        gameClear.SetActive(false);
        gameOver.SetActive(false);
        isStop = false;
        isOp = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        cTmin = (int)GameManager.clearTime / 60;
        if((int)(GameManager.clearTime % 60) < 10)
            cT = "0" + ((int)GameManager.clearTime % 60).ToString();
        else
            cT = ((int)GameManager.clearTime % 60).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        if(isStop)
            Time.timeScale = 0f;
    }

    public static void GameOver(bool isclear)//모은 데이터들 csv에 저장//복원은 하드웨어만 됨.//스테이지3는 다르게 작용하게하기?
    {
        if (!isOp)
        {
            OperationLogManager.isClear = isclear;
            OperationLogManager.time = GameManager.gameTime;
            OperationLogManager.collectLevel = GameManager.supplylevel;
            OperationLogManager.GetParts();
            OperationLogManager.WriteOperationLog(null);//기본 값임. 나중에 결과창에서 설정할 수 있게끔 하기
                                                        //false일 경우 파괴되는 이미지 넣기
            if (isclear)
            {
                gameClear.SetActive(true);
            }
            else
            {
                gameOver.SetActive(true);
            }
            Time.timeScale = 0f;
            //다른 나머지 ui도 setactive비활성화
            //csv로 truefalse로 클리어여부 파악해서 씬이동
            isStop = true;
            isOp = true;
        }
    }

    private void Timer()
    {
        gTmin = (int)GameManager.gameTime / 60;
        if ((int)GameManager.gameTime % 60 < 10)
            gT = "0" + ((int)GameManager.gameTime % 60).ToString();
        else
            gT = ((int)GameManager.gameTime % 60).ToString();
        timerText.text = gTmin + " : " + gT + " / " + cTmin + " : " + cT;
    }

    public void GameOverButton()//두개 전부 공유//csv로 truefalse로 클리어여부 파악해서 씬이동
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ScoreCompute");
    }
}
