using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour
{
    private Transform joyStick;
    private Transform joyStickBackground;
    private Vector3 centerVector;
    public Vector3 joyStickVector;
    private float radius;

    private bool setPos = false;

    // Start is called before the first frame update
    void Start()
    {
        joyStickBackground = transform.GetChild(0);
        joyStick = transform.GetChild(1);
        radius = joyStickBackground.GetComponent<RectTransform>().sizeDelta.y * 2f;
        centerVector = joyStick.transform.position;

        joyStick.gameObject.SetActive(setPos);
        joyStickBackground.gameObject.SetActive(setPos);

        //float canvas = transform.parent.GetComponent<RectTransform>().localScale.x;
        //radius *= canvas;
    }

    public void Drag(BaseEventData _data)
    {
        PointerEventData data = _data as PointerEventData;

        if(setPos == false)
        {
            joyStickBackground.GetComponent<RectTransform>().position = data.position;
            centerVector = data.position;

            setPos = true;

            joyStick.gameObject.SetActive(setPos);
            joyStickBackground.gameObject.SetActive(setPos);
        }

        Vector3 pos = data.position;

        joyStickVector = (pos - centerVector).normalized;

        float distance = Vector3.Distance(pos, centerVector);

        if (distance < radius)
            joyStick.position = centerVector + joyStickVector * distance;
        else
            joyStick.position = centerVector + joyStickVector * radius;

        float rotateDegree = Mathf.Atan2(joyStick.transform.position.y - centerVector.y, joyStick.transform.position.x - centerVector.x) * Mathf.Rad2Deg;
        joyStickBackground.transform.rotation = Quaternion.Euler(0, 0, rotateDegree);
    }

    public void DragEnd()
    {
        joyStick.position = centerVector;
        joyStickVector = Vector3.zero;

        setPos = false;

        joyStickBackground.transform.rotation = Quaternion.Euler(0, 0, 0);

        joyStick.gameObject.SetActive(setPos);
        joyStickBackground.gameObject.SetActive(setPos);
    }
}
