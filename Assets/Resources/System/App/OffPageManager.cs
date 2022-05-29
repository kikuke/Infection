using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffPageManager : MonoBehaviour
{
    GameObject ad;
    GameObject offBG;
    private void Awake()
    {
        ad = GameObject.Find("GOAdManager(Clone)");
        offBG = transform.Find("OffBackGround").gameObject;
        offBG.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && offBG.activeSelf == true)
            offBG.SetActive(false);
    }

    public void YesButton()
    {
        ad.GetComponent<OffPageAdController>().ShowInterstitial();
        offBG.SetActive(true);
    }

    public void NoButton()
    {
        Destroy(gameObject);
    }
}
