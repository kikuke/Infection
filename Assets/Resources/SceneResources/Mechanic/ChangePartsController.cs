using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ChangePartsController : MonoBehaviour
{
    static GameObject parts;
    GameObject changePartsList;
    public static string partsName;//파츠명 ex)Head
    // Start is called before the first frame update
    void Awake()
    {
        parts = transform.Find("Parts").gameObject;
        changePartsList = transform.Find("ChangePartsList").gameObject;
        Parts();
    }

    private void Start()
    {
        changePartsList.SetActive(false);
    }
    //체인지 파트 리스트에서 파츠 프리팹들이 나열되는데 그거 누르면 인게임의 파츠 설명정보나오고 거기에 확인있음 그거누르면 바뀌게
    public static void Parts()
    {
        GameObject temp;
        int j = 5;
        for (int i = 0; i < parts.transform.childCount; i++)
        {
            if (parts.transform.GetChild(i).tag != "Untagged")
            {
                temp = Resources.Load(HardwareManager.FindPart((string)MainInformationManager.mainInformationlist[j]["Player"])) as GameObject;
                parts.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>(HardwareManager.FindPart((string)MainInformationManager.mainInformationlist[j]["Player"]) + "Image");
                j++;
            }
        }
    }
    //파츠상점은 여기의 파츠보유정보에서 키값검사후 하드웨어 파츠리스트에서빼서 출력
    //하드웨어 매니저 쪽에서 존이 3인 경우에만 메인의 정보로 시작하게하기
    //스크롤로 구현하기
    public void ChangePartsButton(string parts)//어느 부위인지 적기ex)Head
    {
        changePartsList.SetActive(true);
        ChangePartsListController.parts = parts;
        changePartsList.GetComponent<ChangePartsListController>().SetContents();
    }

    public void CancelButton()
    {
        gameObject.SetActive(false);
    }

    //???
    public void InformationButton(string name)
    {
        transform.Find(name).gameObject.SetActive(!transform.Find(name).gameObject.activeSelf);
    }
}

