using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gage : MonoBehaviour
{
    private Slider softwareSlider;

    // Start is called before the first frame update
    void Start()
    {
        softwareSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        softwareSlider.value = GameObject.Find("GameManager").GetComponent<GameManager>().supplygageper * 100;
    }
}
