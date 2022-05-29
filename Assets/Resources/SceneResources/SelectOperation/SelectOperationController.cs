using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectOperationController : MonoBehaviour
{
    string operationName;
    public int operateNum;

    public void SelectButton()
    {
        if(operateNum == 2)
        {
            if(MainInformationManager.Rank(false) < 3)
            {
                MessageController.LowRank3();
                return;
            }
        }
        if(operateNum == 3)
        {
            if (MainInformationManager.Rank(false) < 5)
            {
                MessageController.LowRank5();
                return;
            }
        }
        DontDestLogManager.zone = operationName;
        SelectOperationManager.OperateZoneButton(operateNum);
    }

    public void OperationName(string operationName)
    {
        this.operationName = operationName;
        transform.Find("OperationName").GetComponent<Text>().text = operationName;
    }

    public void Rank(int rank)
    {
        transform.Find("Rank").GetComponent<Text>().text = "Rank\n" + rank.ToString("N0");
    }

    public void Gold(int gold)
    {
        transform.Find("Gold").GetComponent<Text>().text = "Gold\n" + gold.ToString("N0");
    }
}
