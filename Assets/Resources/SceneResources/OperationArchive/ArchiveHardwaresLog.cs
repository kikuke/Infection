using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ArchiveHardwaresLog : MonoBehaviour
{
    static GameObject parts;
    GameObject partsInformation;
    public static string operationName;
    // Start is called before the first frame update
    void Awake()
    {
        parts = transform.Find("Parts").gameObject;
        partsInformation = transform.Find("PartsInformation").gameObject;
        partsInformation.SetActive(false);
    }

    public static void Parts()
    {
        GameObject temp;
        int j = 6;
        for (int i = 0; i < parts.transform.childCount; i++)
        {
            if (parts.transform.GetChild(i).tag != "Untagged")
            {
                temp = Resources.Load(HardwareManager.FindPart((string)OperationArchiveManager.operationArchiveList[j][operationName])) as GameObject;
                parts.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>(HardwareManager.FindPart((string)OperationArchiveManager.operationArchiveList[j][operationName]) + "Image");
                j++;
            }
        }
    }

    public void ShowPartInformation(string parts)//어느 부위인지 적기ex)Head
    {
        GameObject temp = null;
        int j;
        partsInformation.SetActive(true);
        if (parts == "LeftArm")
        {
            temp = Resources.Load(HardwareManager.FindPart((string)OperationArchiveManager.operationArchiveList[9][operationName])) as GameObject;
            partsInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(HardwareManager.FindPart((string)OperationArchiveManager.operationArchiveList[9][operationName]) + "Image");
            partsInformation.transform.Find("Text").GetComponent<Text>().text = temp.GetComponent<Part>().Description();
        }
        else if (parts == "RightArm")
        {
            temp = Resources.Load(HardwareManager.FindPart((string)OperationArchiveManager.operationArchiveList[10][operationName])) as GameObject;
            partsInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(HardwareManager.FindPart((string)OperationArchiveManager.operationArchiveList[10][operationName]) + "Image");
            partsInformation.transform.Find("Text").GetComponent<Text>().text = temp.GetComponent<Part>().Description();
        }
        else
        {
            for (j = 6; j < OperationArchiveManager.operationArchiveList.Count; j++)
            {
                temp = Resources.Load(HardwareManager.FindPart((string)OperationArchiveManager.operationArchiveList[j][operationName])) as GameObject;
                if (temp.tag == parts)
                    break;
            }
            partsInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(HardwareManager.FindPart((string)OperationArchiveManager.operationArchiveList[j][operationName]) + "Image");
            partsInformation.transform.Find("Text").GetComponent<Text>().text = temp.GetComponent<Part>().Description();
        }
    }

    public void InformationButton(string name)
    {
        transform.Find(name).gameObject.SetActive(!transform.Find(name).gameObject.activeSelf);
    }
}