using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GMZone3 : MonoBehaviour//타이머도 시간경과가 아닌 다음 보스몹의 젠시간을 알려주는 용도로 사용?
{
    GameObject levelCounter;
    Text text;
    public static int level;//보스잡으면 하나씩 올라감 보스 사망때 증가하도록 집어넣기 킬카운터 변형해서 사용

    int clearLevel;

    private void Awake()
    {
        level = 1;
        levelCounter = transform.Find("GameOverManager/Level/Text").gameObject;
        text = levelCounter.GetComponent<Text>();
        clearLevel = 4;//보스 3마리임
    }

    void Update()
    {
        GameManage();
        LevelCounter();
    }
    //보스몹 스포너는 따로 만들기
    private void LevelCounter()
    {
        text.text = "Level : " + level;
    }

    private void GameManage()//현재 레벨에 따른 다른 보상
    {
        if (!GameManager.isGameOver)
        {
            if (GameManager.gameTime >= GameManager.clearTime)
            {
                GameManager.isGameOver = true;
                GameOverManager.GameOver(false);//시간 초과시 실패임
            }
            else if (level >= clearLevel)
            {
                GameManager.isGameOver = true;
                DontDestLogManager.gameClear = true;
                GameOverManager.GameOver(true);//클리어 하면 엔딩크레딧으로 이동
            }
        }
    }
}
