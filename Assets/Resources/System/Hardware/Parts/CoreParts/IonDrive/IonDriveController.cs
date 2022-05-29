using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonDriveController : Part
{
    private float time;
    private float speed = 10;
    private float flyTime = 3;
    private Rigidbody2D rigidbody2D;
    private GameObject effect;
    private float effectScaleX;
    private Quaternion previousAngle;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        time = 0;
        rigidbody2D = GameManager.player.GetComponent<Rigidbody2D>();
        effect = transform.GetChild(0).gameObject;
        effectScaleX = effect.transform.localScale.x;
        effect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(time >= cooltime)
        {
            time = 0;
            Vector3 dir = FindObjectOfType<JoyStickController>().joyStickVector;
            if(dir != Vector3.zero)
                StartCoroutine(Fly(dir));
        }
        time += Time.deltaTime;

        effect.transform.localScale = new Vector3(transform.root.Find("Hardwares").localScale.x * effectScaleX, 1, 1);
    }

    IEnumerator Fly(Vector3 dir)
    {
        float i = 0;
        GameManager.player.GetComponent<PlayerController>().invincibility = true;
        effect.SetActive(true);
        float rotateDegree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        previousAngle = effect.transform.rotation = Quaternion.Euler(0, 0, rotateDegree);
        while (i <= flyTime)
        {
            float xInput = Input.GetAxisRaw("Horizontal");
            float yInput = Input.GetAxisRaw("Vertical");
            Vector3 moveVector = FindObjectOfType<JoyStickController>().joyStickVector;
            GameManager.player.transform.position += moveVector;
            GameManager.player.transform.position += dir * (GameManager.player.GetComponent<PlayerController>().moveSpeed + speed) * Time.deltaTime;
            rotateDegree = Mathf.Atan2(dir.y + moveVector.y, dir.x + moveVector.x) * Mathf.Rad2Deg;
            previousAngle = effect.transform.rotation = Quaternion.Slerp(previousAngle, Quaternion.Euler(0, 0, rotateDegree), 10f * Time.deltaTime);
            yield return null;
            i += Time.deltaTime;
        }
        time = 0;
        GameManager.player.GetComponent<PlayerController>().invincibility = false;
        effect.SetActive(false);
    }


    public override string Description()
    {
        string partName = base.Description();
        showName = colorCode[int.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Level")][partName].ToString())] + "이온 드라이브" + "</color>";
        string description =
            showName + "\n" +
            "전방으로 날아간다\n" +
            "\n이동속도 " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "AddMvSpPer")][partName].ToString()) * 100 + "% 증가\n" +
            "쿨타임 : " + float.Parse(GlobalManager.hardwarelist[GlobalManager.FindOption(GlobalManager.hardwarelist, "Cooltime")][partName].ToString()) + "초";

        return description;
    }
}
