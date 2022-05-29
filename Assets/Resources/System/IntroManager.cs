using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (MessageController.NonRepeatMessage("Event1"))
        {
            MessageController.Event1();
        }
        else
            SceneManager.LoadScene("Main");
    }

    public void SkipButton()
    {
        SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
