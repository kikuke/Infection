using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArchiveOperationController : MonoBehaviour
{
    string operationName;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OperationLogButton()
    {
        OperationArchiveManager.operationLog.SetActive(true);
        ArchiveHardwaresLog.operationName = operationName;//이거 순서 유의하기 안되면 밑으로 보내기
        ArchiveHardwaresLog.Parts();
        OperationArchiveManager.OperationLog(operationName);
        OperationArchiveManager.selectOperation.SetActive(false);
        //이거 찾아서 활성화 시키고 operationname으로 정보들 가져오기
    }

    public void CancelButton()
    {
        OperationArchiveManager.selectOperation.SetActive(true);
        OperationArchiveManager.operationLog.SetActive(false);
    }

    public void OperationName(string operationName)
    {
        this.operationName = operationName;
        transform.Find("OperationName").GetComponent<Text>().text = operationName;
    }

    public void Time(float time)
    {
        transform.Find("Time").GetComponent<Text>().text = "Time\n" + (int)time / 60 + "m" + (int)time % 60 + "s";
    }

    public void Score(int score)
    {
        transform.Find("Score").GetComponent<Text>().text = "Score\n" + score.ToString("N0");
    }
}
