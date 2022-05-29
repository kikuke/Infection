using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartProductController : MonoBehaviour
{
    GameObject part;
    string partname;

    public void SetProductPart(string partname)
    {
        this.partname = partname;
        part = Resources.Load(HardwareManager.FindPart(partname)) as GameObject;
        transform.Find("PartName").GetComponent<Text>().text = part.GetComponent<Part>().Description().Substring(part.GetComponent<Part>().Description().IndexOf(">") + 1, part.GetComponent<Part>().Description().Substring(part.GetComponent<Part>().Description().IndexOf(">") + 1).IndexOf("</color>"));
        transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(HardwareManager.FindPart(partname) + "Image");
    }

    public void ShowButton()
    {
        PartsShopController.perchaseProduct = partname;
        PartsShopController.partsInformation.SetActive(true);
        PartsShopController.partsInformation.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(HardwareManager.FindPart(partname) + "Image");
        PartsShopController.partsInformation.transform.Find("Text").GetComponent<Text>().text = part.GetComponent<Part>().Description();
        PartsShopController.partsInformation.transform.Find("Price").GetComponent<Text>().text = "Gold : " +  GameObject.Find("PartsShop").GetComponent<PartsShopController>().ProductPrice();
    }
}
