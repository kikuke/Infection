using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightsaberController : Part
{
    private float time;
    private int angle = 120;
    private float rotateSpeed = 100;
    private float rotateTime = 1;
    private float size = 10;
    private Quaternion previousAngle;
    private Vector3 targetVector;
    private GameObject lightsaber;
    private Slider coolTimeSlider;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        transform.localPosition = Vector3.zero;

        lightsaber = transform.GetChild(0).gameObject;
        lightsaber.transform.SetParent(transform.root);
        lightsaber.transform.localPosition = Vector3.zero;
        lightsaber.gameObject.SetActive(false);

        time = cooltime;

        coolTimeSlider = transform.root.Find("PlayerUI/" + transform.parent.name.Substring(0, transform.parent.name.IndexOf("Arm")) + "Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= cooltime)
        {
            time = 0;
            StartCoroutine(Attack());
        }
        time += Time.deltaTime;

        if(GameManager.player.GetComponent<PlayerController>().moveVector != Vector3.zero)
            targetVector = GameManager.player.GetComponent<PlayerController>().moveVector.normalized;

        CooltimeSlider();
    }

    IEnumerator Attack()
    {
        float rotateDegree = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        lightsaber.gameObject.SetActive(true);
        lightsaber.GetComponent<LightsaberEffectController>().damage = damage;
        previousAngle = lightsaber.transform.rotation = Quaternion.Euler(0, 0, rotateDegree);
        int i = 0;
        while (i < (FindObjectOfType<LightsaberUpgradeController>() != null ? size * (FindObjectOfType<LightsaberUpgradeController>().attackRange + 1) : size))
        {
            lightsaber.transform.localScale = new Vector3(1, i / 10f, 1);
            previousAngle = lightsaber.transform.rotation = Quaternion.Slerp(previousAngle, Quaternion.Euler(0, 0, rotateDegree - (angle / size) * i), rotateSpeed * Time.deltaTime);
            yield return null;
            i += 3;
        }
        i = -angle;
        while (i < angle)
        {
            previousAngle = lightsaber.transform.rotation = Quaternion.Slerp(previousAngle, Quaternion.Euler(0, 0, rotateDegree + i), 2*rotateSpeed * Time.deltaTime);
            time = 0;
            yield return null;
            i += 10;
        }
        lightsaber.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "광선검" + "</color>";
        string descriptioon =
            showName + "\n" +
            "전방을 향해 광선검을 휘두른다\n" +
            "공격력 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Damage")][partName].ToString());

        return descriptioon;
    }

    private void OnDestroy()
    {
        Destroy(lightsaber);
    }

    private void CooltimeSlider()
    {
        coolTimeSlider.value = time / cooltime * 100;
    }
}
