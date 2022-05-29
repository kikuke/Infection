using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestManager : MonoBehaviour
{
    private void Awake()
    {
        if (GameObject.Find("GOAdManager(Clone)") == null)
        {
            Instantiate(Resources.Load("System/GOAdManager") as GameObject);
        }
        if (GameObject.Find("GOAppManager(Clone)") == null)
        {
            Instantiate(Resources.Load("System/GOAppManager") as GameObject);
        }
        if (GameObject.Find("GODontDestLog(Clone)") == null)
        {
            Instantiate(Resources.Load("System/GODontDestLog") as GameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
