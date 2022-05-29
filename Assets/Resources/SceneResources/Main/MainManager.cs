using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    GameObject rankScreen;
    GameObject expScreen;
    GameObject scoreScreen;
    GameObject goldScreen;

    GameObject corepart;
    GameObject headpart;
    GameObject bodypart;
    GameObject leftarmpart;
    GameObject rightarmpart;
    GameObject legpart;

    float coroutineTime;

    private void Awake()
    {
        Time.timeScale = 1f;
        rankScreen = transform.Find("Canvas/Rank").gameObject;
        expScreen = transform.Find("Canvas/Exp").gameObject;
        scoreScreen = transform.Find("Canvas/Score").gameObject;
        goldScreen = transform.Find("Canvas/Gold").gameObject;

        corepart = transform.Find("Canvas/Robot/Parts/CorePart").gameObject;
        headpart = transform.Find("Canvas/Robot/Parts/HeadPart").gameObject;
        bodypart = transform.Find("Canvas/Robot/Parts/BodyPart").gameObject;
        leftarmpart = transform.Find("Canvas/Robot/Parts/LeftArmPart").gameObject;
        rightarmpart = transform.Find("Canvas/Robot/Parts/RightArmPart").gameObject;
        legpart = transform.Find("Canvas/Robot/Parts/LegPart").gameObject;

        SetRobot();

        MainInform();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1f;
        EventMessage();

        if (Input.GetMouseButtonDown(0))
            coroutineTime = 0f;
    }

    public void HelpButton()
    {
        MessageController.MainHelp();
    }

    void EventMessage()
    {
        if(MainInformationManager.Rank(false) >= 3)
        {
            if (MessageController.NonRepeatMessage("Rank3"))
                MessageController.Rank3();
        }
        if (MainInformationManager.Rank(false) >= 5)
        {
            if (MessageController.NonRepeatMessage("Rank5"))
                MessageController.Rank5();
        }
    }

    void SetRobot()
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
        coroutineTime = 0.01f;
        StartCoroutine(ShowString(rankScreen, MainInformationManager.Rank(false).ToString("N0")));
        StartCoroutine(ShowString(expScreen, (float.Parse(MainInformationManager.mainInformationlist[1]["Player"].ToString())/ float.Parse(MainInformationManager.mainInformationlist[2]["Player"].ToString())*100).ToString("N0") + "%"));
        transform.Find("Canvas/ExpImage/ExpGage").localScale = new Vector3(float.Parse(MainInformationManager.mainInformationlist[1]["Player"].ToString()) / float.Parse(MainInformationManager.mainInformationlist[2]["Player"].ToString()), 1, 1);
        StartCoroutine(ShowString(scoreScreen, MainInformationManager.Score(0).ToString("N0")));
        StartCoroutine(ShowString(goldScreen, MainInformationManager.Gold(0).ToString("N0")));
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

    public void MechanicButton()
    {
        if(MainInformationManager.Rank(false) < 5)
        {
            MessageController.LowRank5();
        }
        else
            SceneManager.LoadScene("Mechanic");
    }

    public void ShopButton()
    {
        if (MainInformationManager.Rank(false) < 3)
        {
            MessageController.LowRank3();
        }
        else
            SceneManager.LoadScene("SupplyShop");
    }

    public void LogButton()
    {
        if (MainInformationManager.Rank(false) < 3)
        {
            MessageController.LowRank3();
        }
        else
            SceneManager.LoadScene("OperationLog");
    }

    public void StartButton()
    {
        SceneManager.LoadScene("SelectOperation");
    }
}
