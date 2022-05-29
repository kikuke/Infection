using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsShopController : MonoBehaviour
{
    GameObject rankScreen;
    GameObject goldScreen;

    GameObject content;

    GameObject partsList;

    public static string perchaseProduct;

    public static GameObject partsInformation;

    float coroutineTime;
    // Start is called before the first frame update
    void Awake()
    {
        rankScreen = transform.Find("Rank").gameObject;
        goldScreen = transform.Find("Gold").gameObject;

        partsList = transform.Find("PartsList").gameObject;

        partsInformation = partsList.transform.Find("PartsInformation").gameObject;
        partsInformation.SetActive(false);
    }

    private void Start()
    {
        SetContents();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            coroutineTime = 0f;
    }

    public void ReSetting()
    {
        for (int i = 0; i < content.transform.childCount; i++)
            Destroy(content.transform.GetChild(i).gameObject);
        GameObject.Find("MechanicManager").GetComponent<MechanicManager>().MainInform();
        SetContents();
        MainInform();
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
        Debug.Log("@#@");
        screen.GetComponent<Text>().text = null;

        for (int i = 0; i < text.Length; i++)
        {
            screen.GetComponent<Text>().text += text.Substring(i, 1);
            yield return new WaitForSeconds(coroutineTime);
        }
    }

    public void CancelButton()
    {
        ReSetting();
        gameObject.SetActive(false);
    }

    public void InformCancelButton()
    {
        partsInformation.SetActive(false);
    }

    public int ProductPrice()///#####
    {
        return ((int)GlobalManager.hardwarelist[1][perchaseProduct]* (int)GlobalManager.hardwarelist[1][perchaseProduct] * 500);//임시조치
    }

    public void PerchaseButton()
    {
        if (MainInformationManager.Gold(-ProductPrice()) != -1)
        {//유의하기
            MechanicManager.partsInformation[0][perchaseProduct] = GlobalManager.hardwarelist[0][perchaseProduct];
            MechanicManager.partsInformation[1][perchaseProduct] = 0;

            JsonWriter.Write(MechanicManager.partsInformation, PathManager.FindPath("PartsInformation"));

            ReSetting();
            partsInformation.SetActive(false);
        }
    }

    public void SetContents()
    {
        content = transform.Find("PartsList/Viewport/Content").gameObject;
        bool isProduct;
        GameObject temp;
        foreach (KeyValuePair<string, object> kv1 in GlobalManager.hardwarelist[0])
        {
            isProduct = true;
            if ((string)kv1.Value == "Drone")
                isProduct = false;
            else
                foreach (KeyValuePair<string, object> kv2 in MechanicManager.partsInformation[0])
                    if (kv1.Key == kv2.Key)
                        isProduct = false;

            if(isProduct)
            {
                temp = Instantiate(Resources.Load("SceneResources/Mechanic/PartProduct"), content.transform) as GameObject;
                Debug.Log(temp);
                temp.GetComponent<PartProductController>().SetProductPart(kv1.Key);
            }
        }
    }
}
