using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerInformationSystem : MonoBehaviour
{
    GameObject mainBackGround;
    GameObject partsButton;
    GameObject parts;
    GameObject partsInformation;
    GameObject softwaresButton;
    GameObject exitButton;
    GameObject softwares;
    GameObject drone;

    private bool mainbutton;
    // Start is called before the first frame update
    void Awake()
    {
        mainBackGround = transform.Find("Main/MainBackGround").gameObject;
        partsButton = transform.Find("Main/PartsButton").gameObject;
        parts = transform.Find("Hardwares/Parts").gameObject;
        partsInformation = transform.Find("Hardwares/PartsInformation").gameObject;
        softwaresButton = transform.Find("Main/SoftwaresButton").gameObject;
        exitButton = transform.Find("Main/Exit").gameObject;
        softwares = transform.Find("Softwares").gameObject;
        drone = transform.Find("Hardwares/Parts/ShowDrone").gameObject;
        mainbutton = false;
        mainBackGround.SetActive(false);
        partsButton.SetActive(false);
        parts.SetActive(false);
        partsInformation.SetActive(false);
        softwaresButton.SetActive(false);
        exitButton.SetActive(false);
        //softwares.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainbutton)
            Time.timeScale = 0;
        //Main();
    }

    private void MainSet(bool isActive)
    {
        mainBackGround.SetActive(isActive);
        partsButton.SetActive(isActive);
        softwaresButton.SetActive(isActive);
        exitButton.SetActive(isActive);
    }

    private void Parts()
    {
        for(int i = 0; i < parts.transform.childCount; i++)
        {
            if (parts.transform.GetChild(i).tag != "Untagged" && parts.transform.GetChild(i).tag != "Drone")//드론은 기본이미지 만들어서 사용하기
            {
                //parts.transform.GetChild(i).GetComponent<Image>().sprite = GameManager.player.transform.Find("Hardwares/Parts/" + parts.transform.GetChild(i).name).GetChild(0).GetComponent<SpriteRenderer>().sprite;
                parts.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/" + parts.transform.GetChild(i).tag + "Parts/" + PartName(parts.transform.GetChild(i).name) + "/" + PartName(parts.transform.GetChild(i).name) + "Image");
            }//스프라이트 이미지 바꾸기 폴더옆에 이미지 따로 만들어서경로로 찾기
            //else if(parts.transform.GetChild(i).tag == "Drone")
            //{
            //
            //}
        }
    }

    public void DroneParts()
    {
        drone.SetActive(true);
        for(int i = 0; i < 3; i++)
            drone.transform.Find("Drone" + i).gameObject.SetActive(true);

        for (int i = GameManager.player.GetComponent<PlayerController>().maxDroneNum; i > GameManager.player.GetComponent<PlayerController>().droneNum;)
        {
            i -= 1;
            drone.transform.Find("Drone" + i).gameObject.SetActive(false);
        }

        for (int count = 0; count < 3; count++)
        {
            if (GameManager.player.transform.Find("Hardwares/Parts/DronePart/Drone" + count).childCount != 0)
                parts.transform.Find("ShowDrone/Drone" + count).GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/DroneParts/" + PartName("DronePart/Drone" + count) + "/" + PartName("DronePart/Drone" + count) + "Image");
        }
    }

    public void ShowPartInformation(string parts)//어느 부위인지 적기ex)HeadPart
    {
        partsInformation.SetActive(true);
        if (parts.Contains("Drone"))
        {
            if (GameManager.player.transform.Find("Hardwares/Parts/DronePart/" + parts).childCount != 0)
            {
                Debug.Log(Resources.Load<Sprite>("System/Hardware/Parts/DroneParts/" + PartName("DronePart/" + parts) + " / " + PartName("DronePart/" + parts) + "Image"));
                partsInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/DroneParts/" + PartName("DronePart/" + parts) + "/" + PartName("DronePart/" + parts) + "Image");
                partsInformation.transform.Find("Text").GetComponent<Text>().text = GameManager.player.transform.Find("Hardwares/Parts/DronePart/" + parts).GetChild(0).GetComponent<Part>().Description();
            }
            else
            {
                partsInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Alpha Image");
                partsInformation.transform.Find("Text").GetComponent<Text>().text = null;
            }
        }
        else if (parts.Substring(0, parts.IndexOf("Part")) == "LeftArm")
        {
            partsInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/ArmParts/" + PartName(parts) + "/" + PartName(parts) + "Image");
            partsInformation.transform.Find("Text").GetComponent<Text>().text = GameManager.player.transform.Find("Hardwares/Parts/LeftArmPart").GetChild(0).GetComponent<Part>().Description();
        }
        else if (parts.Substring(0, parts.IndexOf("Part")) == "RightArm")
        {
            partsInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/ArmParts/" + PartName(parts) + "/" + PartName(parts) + "Image");
            partsInformation.transform.Find("Text").GetComponent<Text>().text = GameManager.player.transform.Find("Hardwares/Parts/RightArmPart").GetChild(0).GetComponent<Part>().Description();
        }
        else
        {
            partsInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Hardware/Parts/" + parts.Substring(0, parts.IndexOf("Part")) + "Parts/" + PartName(parts) + "/" + PartName(parts) + "Image");
            partsInformation.transform.Find("Text").GetComponent<Text>().text = GameManager.player.transform.Find("Hardwares/Parts/" + parts).GetChild(0).GetComponent<Part>().Description();
        }
    }

    private string PartName(string _partType)
    {
        string partName = GameManager.player.transform.Find("Hardwares/Parts/" + _partType).GetChild(0).name;
        partName = partName.Substring(0, partName.IndexOf("(Clone)"));
        Debug.Log(partName);
        return partName;
    }

    public void PartsButton()
    {
        MainSet(false);
        parts.SetActive(true);
        Parts();
    }

    public void SoftwaresButton()
    {
        MainSet(false);
        softwares.SetActive(true);
        Softwares();
    }

    private void Softwares()
    {
        Transform softwares = transform.Find("Softwares/SoftwareScrollView/Viewport/Content");
        for (int i = 0; i < softwares.childCount; i++)
        {
            if (softwares.GetChild(i).CompareTag("Satellite"))
            {
                softwares.GetChild(i).transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Software/Satellites/" + softwares.GetChild(i).name + "/" + softwares.GetChild(i).name + "Image");
                GameObject software = GameManager.player.transform.Find("Softwares/Satellites/" + softwares.GetChild(i).name + "(Clone)").gameObject;
                softwares.GetChild(i).transform.Find("Text").GetComponent<Text>().text = software.GetComponent<Software>().Description(0);
            }
            else
            {
                softwares.GetChild(i).transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("System/Software/" + softwares.GetChild(i).name + "/" + softwares.GetChild(i).name + "Image");
                GameObject software = GameManager.player.transform.Find("Softwares/" + softwares.GetChild(i).name + "(Clone)").gameObject;
                softwares.GetChild(i).transform.Find("Text").GetComponent<Text>().text = software.GetComponent<Software>().Description(0);
            }
        }
    }

    public void MainButton()
    {
        mainbutton = !mainbutton;
        if (mainbutton)
        {
            MainSet(true);
            Time.timeScale = 0;
        }
        else
        {
            MainSet(false);
            Time.timeScale = 1f;
        }
        if (!mainbutton)//다끄기
        {
            mainBackGround.SetActive(false);
            partsButton.SetActive(false);
            parts.SetActive(false);
            partsInformation.SetActive(false);
            drone.SetActive(false);
            softwares.SetActive(false);
            softwaresButton.SetActive(false);
            exitButton.SetActive(false);
        }
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }

    public void InformationButton(string name)
    {
        transform.Find(name).gameObject.SetActive(!transform.Find(name).gameObject.activeSelf);
    }
}
