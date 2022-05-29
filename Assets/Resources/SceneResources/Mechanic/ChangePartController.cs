using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePartController : MonoBehaviour
{
    GameObject part;
    string partname;

    public void SetChangePart(string partname)
    {
        Debug.Log(partname);
        this.partname = partname;
        part = Resources.Load(HardwareManager.FindPart(partname)) as GameObject;
        transform.Find("PartName").GetComponent<Text>().text = part.GetComponent<Part>().Description().Substring(part.GetComponent<Part>().Description().IndexOf(">") + 1, part.GetComponent<Part>().Description().Substring(part.GetComponent<Part>().Description().IndexOf(">") + 1).IndexOf("</color>")); ;
        transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(HardwareManager.FindPart(partname) + "Image");
    }

    public void ChangeButton()
    {
        ChangePartsListController.changePart = partname;
        ChangePartsListController.partsInformation.SetActive(true);
        ChangePartsListController.partsInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(HardwareManager.FindPart(partname) + "Image");
        ChangePartsListController.partsInformation.transform.Find("Text").GetComponent<Text>().text = part.GetComponent<Part>().Description();
        ChangePartsListController.partsInformation.transform.Find("Level").GetComponent<Text>().text = "Level : " + MechanicManager.partsInformation[1][partname].ToString() + "\nNext : " + ChangePartsListController.RfGold() + "Gold\n" + MainInformationManager.Gold(0) + "Gold";
    }
    /*
    void temp(string parts)//이따 클릭시 정보 주는거임. 콘텐츠 인스턴스에 집어넣기
    {
        GameObject temp = null;
        int j;
        changePartsList.SetActive(true);
        if (parts == "LeftArm")
        {
            temp = Resources.Load(HardwareManager.FindPart((string)OperationLogManager.operationLog[9][partsName])) as GameObject;
            changePartsList.transform.Find("Image").GetComponent<Image>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
            changePartsList.transform.Find("Text").GetComponent<Text>().text = temp.GetComponent<Part>().Description();
        }
        else if (parts == "RightArm")
        {
            temp = Resources.Load(HardwareManager.FindPart((string)OperationLogManager.operationLog[10][partsName])) as GameObject;
            changePartsList.transform.Find("Image").GetComponent<Image>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
            changePartsList.transform.Find("Text").GetComponent<Text>().text = temp.GetComponent<Part>().Description();
        }
        else
        {
            for (j = 6; j < OperationLogManager.operationLog.Count; j++)
            {
                temp = Resources.Load(HardwareManager.FindPart((string)OperationLogManager.operationLog[j][partsName])) as GameObject;
                if (temp.tag == parts)
                    break;
            }
            changePartsList.transform.Find("Image").GetComponent<Image>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
            changePartsList.transform.Find("Text").GetComponent<Text>().text = temp.GetComponent<Part>().Description();
        }
    }
    */
}