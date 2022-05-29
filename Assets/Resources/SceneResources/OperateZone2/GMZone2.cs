using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GMZone2 : MonoBehaviour
{
    GameObject killCounter;
    Text text;
    
    int clearKill;

    private void Awake()
    {
        killCounter = transform.Find("GameOverManager/KillCounter/Text").gameObject;
        text = killCounter.GetComponent<Text>();
        clearKill = 1200;
    }

    void Update()
    {
        GameManage();
        KillCounter();
    }

    private void KillCounter()
    {
        text.text = "현재 : " + OperationLogManager.kill + " / " + "목표 : " + clearKill;
    }

    private void GameManage()//킬로 점수 내는거 어떤 개체를 사냥했냐에 따라 개별 점수화 즉각적으로 더하기
    {
        if (!GameManager.isGameOver)
        {
            if (GameManager.gameTime >= GameManager.clearTime)
            {
                GameManager.isGameOver = true;
                GameOverManager.GameOver(false);//시간 초과시 실패임
            }
            else if(OperationLogManager.kill >= clearKill)
            {
                GameManager.isGameOver = true;
                if ((string)MainInformationManager.mainInformationlist[14]["Player"] == "FALSE")
                {
                    MainInformationManager.mainInformationlist[14]["Player"] = "TRUE";
                    MainInformationManager.mainInformationlist[0]["Player"] = 5;
                    JsonWriter.Write(MainInformationManager.mainInformationlist, PathManager.FindPath("MainInformation"));
                }
            }
        }
    }
}
