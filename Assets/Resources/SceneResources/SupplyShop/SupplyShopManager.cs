using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SupplyShopManager : MonoBehaviour
{
    GameObject rankScreen;
    GameObject goldScreen;
    GameObject softScreen;
    GameObject ad;

    GameObject randomSoftScreen;
    GameObject addRewardScreen;

    float rndTime;
    float drndTime;

    float coroutineTime;

    private void Awake()
    {
        Time.timeScale = 1f;
        rankScreen = transform.Find("Canvas/Rank").gameObject;
        goldScreen = transform.Find("Canvas/Gold").gameObject;
        softScreen = transform.Find("Canvas/Soft").gameObject;

        ad = GameObject.Find("GOAdManager(Clone)");

        randomSoftScreen = transform.Find("Canvas/RandomSoftButton/Text").gameObject;
        addRewardScreen = transform.Find("Canvas/AddRewardButton/Text").gameObject;

        rndTime = 1f;

        drndTime = 0f;

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
        if (Input.GetMouseButtonDown(0))
            coroutineTime = 0f;
        if(drndTime < rndTime)
            drndTime += Time.deltaTime;
    }

    public void RandomSoftButton()//가격설정해놓기
    {
        if (drndTime >= rndTime)
        {
            drndTime = 0f;
            if (MainInformationManager.Gold(-(MainInformationManager.Rank(false) * 100)) != -1)
            {
                string software = null;
                int i = 0;
                int choose;
                foreach (KeyValuePair<string, object> kv in GlobalManager.softwarelist[0])
                    i++;
                choose = Random.Range(1, i);
                Debug.Log(choose);
                i = 0;
                foreach (KeyValuePair<string, object> kv in GlobalManager.softwarelist[0])
                {
                    if (i == choose)
                    {
                        software = kv.Key;
                        break;
                    }
                    i++;
                }
                MainInformationManager.Software(software);
                MainInform();
            }
        }
    }

    public void MainInform()
    {
        coroutineTime = 0.01f;
        StartCoroutine(ShowString(rankScreen, "Rank\n" + MainInformationManager.Rank(false).ToString("N0")));
        StartCoroutine(ShowString(goldScreen, "Gold\n" + MainInformationManager.Gold(0).ToString("N0")));
        //수정중
        string software = null;

        if ((software = MainInformationManager.Software(null)) == "null")
            software = "미탑재";
        else
            software = SupplySoftwareSystem.GetSoftName(MainInformationManager.Software(null));

        StartCoroutine(ShowString(softScreen, "Software\n" + software));
        coroutineTime = 0f;

        StartCoroutine(ShowString(randomSoftScreen, "소프트웨어 보급\n-" + MainInformationManager.Rank(false) * 100 + "Gold"));
        StartCoroutine(ShowString(addRewardScreen, "골드 보급\n+" + MainInformationManager.Rank(false) * 1000 + "Gold"));
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

    public void AddRewardButton()
    {
        ad.GetComponent<RewardAdController>().ShowRewardedAd();
    }

    public void MainButton()
    {
        SceneManager.LoadScene("Main");
    }
}
