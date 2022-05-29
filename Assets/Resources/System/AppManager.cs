using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    GameObject offPage;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    //취소버튼 및 끝나면 광고보여주는 스크립트 작성하기
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameObject.Find("OffPage(Clone)") == null)
            {
                Time.timeScale = 0f;
                offPage = GameObject.Instantiate(Resources.Load("System/App/OffPage")) as GameObject;
            }
            else
            {
                Destroy(offPage);
                Time.timeScale = 1f;
            }
        }
    }
}
