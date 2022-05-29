using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadScene", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Intro");
    }
}
