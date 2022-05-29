using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMZone1 : MonoBehaviour
{
    private void Awake()
    {
        GameManager.clearTime = 60 * 7f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManage();
    }

    private void GameManage()//킬로 점수 내는거 어떤 개체를 사냥했냐에 따라 개별 점수화 즉각적으로 더하기
    {
        if (!GameManager.isGameOver)
        {
            if (GameManager.gameTime >= GameManager.clearTime)
            {
                GameManager.isGameOver = true;
                if ((string)MainInformationManager.mainInformationlist[13]["Player"] == "FALSE")
                {
                    MainInformationManager.mainInformationlist[13]["Player"] = "TRUE";
                    MainInformationManager.mainInformationlist[0]["Player"] = 3;
                    JsonWriter.Write(MainInformationManager.mainInformationlist, PathManager.FindPath("MainInformation"));
                }
            }
        }
    }
}
