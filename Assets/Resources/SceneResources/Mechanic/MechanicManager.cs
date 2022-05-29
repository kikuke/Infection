using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MechanicManager : MonoBehaviour
{
    public static List<Dictionary<string, object>> partsInformation;

    GameObject chageParts;
    GameObject partsShop;

    GameObject rankScreen;
    GameObject goldScreen;

    GameObject corepart;
    GameObject headpart;
    GameObject bodypart;
    GameObject leftarmpart;
    GameObject rightarmpart;
    GameObject legpart;

    float coroutineTime;
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1f;
        partsInformation = JsonReader.Read(PathManager.FindPath("PartsInformation"));
        rankScreen = transform.Find("Canvas/Rank").gameObject;
        goldScreen = transform.Find("Canvas/Gold").gameObject;

        chageParts = GameObject.Find("ChangeParts");
        partsShop = GameObject.Find("PartsShop");

        corepart = transform.Find("Canvas/Robot/Parts/CorePart").gameObject;
        headpart = transform.Find("Canvas/Robot/Parts/HeadPart").gameObject;
        bodypart = transform.Find("Canvas/Robot/Parts/BodyPart").gameObject;
        leftarmpart = transform.Find("Canvas/Robot/Parts/LeftArmPart").gameObject;
        rightarmpart = transform.Find("Canvas/Robot/Parts/RightArmPart").gameObject;
        legpart = transform.Find("Canvas/Robot/Parts/LegPart").gameObject;
        MainInform();
        SetRobot();
    }

    void Start()
    {
        chageParts.SetActive(false);
        partsShop.SetActive(false);
    }

    void Update()
    {
        Time.timeScale = 1f;
        if (Input.GetMouseButtonDown(0))
            coroutineTime = 0f;
    }

    /*
    //분리시켜서 사용하기 및 인덱스번호 잘보기
    public void SaveButton()//파츠 교체때 사용하기
    {
        GameObject temp = Resources.Load(HardwareManager.FindPart((string)partsInformation[6][operationName])) as GameObject;
        MainInformationManager.mainInformationlist[5]["Player"] = temp.name;
        temp = Resources.Load(HardwareManager.FindPart((string)partsInformation[7][operationName])) as GameObject;
        MainInformationManager.mainInformationlist[6]["Player"] = temp.name;
        temp = Resources.Load(HardwareManager.FindPart((string)partsInformation[8][operationName])) as GameObject;
        MainInformationManager.mainInformationlist[7]["Player"] = temp.name;
        temp = Resources.Load(HardwareManager.FindPart((string)partsInformation[9][operationName])) as GameObject;
        MainInformationManager.mainInformationlist[8]["Player"] = temp.name;
        temp = Resources.Load(HardwareManager.FindPart((string)partsInformation[10][operationName])) as GameObject;
        MainInformationManager.mainInformationlist[9]["Player"] = temp.name;
        temp = Resources.Load(HardwareManager.FindPart((string)partsInformation[11][operationName])) as GameObject;
        MainInformationManager.mainInformationlist[10]["Player"] = temp.name;

        JsonWriter.Write(MainInformationManager.mainInformationlist, PathManager.FindPath("MainInformation"));
    }
    */

    public void HelpButton()
    {
        MessageController.MechanicHelp();
    }

    public void SetRobot()
    {
        GameObject temp = Resources.Load(HardwareManager.FindPart((string)MainInformationManager.mainInformationlist[5]["Player"])) as GameObject;
        corepart.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
        temp = Resources.Load(HardwareManager.FindPart((string)MainInformationManager.mainInformationlist[6]["Player"])) as GameObject;
        headpart.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
        temp = Resources.Load(HardwareManager.FindPart((string)MainInformationManager.mainInformationlist[7]["Player"])) as GameObject;
        bodypart.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
        temp = Resources.Load(HardwareManager.FindPart((string)MainInformationManager.mainInformationlist[8]["Player"])) as GameObject;
        leftarmpart.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
        temp = Resources.Load(HardwareManager.FindPart((string)MainInformationManager.mainInformationlist[9]["Player"])) as GameObject;
        rightarmpart.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
        temp = Resources.Load(HardwareManager.FindPart((string)MainInformationManager.mainInformationlist[10]["Player"])) as GameObject;
        legpart.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
    }

    public void MainInform()
    {
        coroutineTime = 0.03f;
        StartCoroutine(ShowString(rankScreen, "Rank\n" + MainInformationManager.Rank(false)));
        StartCoroutine(ShowString(goldScreen, "Gold\n" + MainInformationManager.Gold(0)));
        coroutineTime = 0f;
    }

    IEnumerator ShowString(GameObject screen, string text)
    {
        screen.GetComponent<Text>().text = null;

        for (int i = 0; i < text.Length; i++)
        {
            screen.GetComponent<Text>().text += text.Substring(i, 1);
            yield return new WaitForSeconds(coroutineTime);
        }
    }

    public void ChangePartsButton()
    {
        chageParts.SetActive(true);
    }

    public void MainButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void PartsShopButton()
    {
        partsShop.SetActive(true);
        partsShop.GetComponent<PartsShopController>().MainInform();
    }
}