using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //클론지우는 함수 만들기
    public GameObject softwares;
    public GameObject corepart;
    public GameObject headpart;
    public GameObject bodypart;
    public GameObject leftarmpart;
    public GameObject rightarmpart;
    public GameObject legpart;//이동을 담당함.
    public GameObject[] dronepart;

    //각 능력치 변수들은 여러 요소들의 합으로 초기화하기. 바뀔때마다 초기화 한번씩. 더하는것이 아닌 다시 초기화하는것임을 생각
    //인공위성들의 쿨타임은 개별적으로 작동시키기

    //각 파츠콜리더에서 피해받은것 이쪽으로 전달하기

    private Rigidbody2D rigidbody;

    //플레이어 고유 능력치
    private float playerSpeed = 10f;//플레이어 고유 스피드
    private float playermaxHp;
    public float playerHp;
    private float playerdefend = 1f;
    private float playercooltimePer = 0;
    private float playeraddDamPer = 0;
    private float playeraddAtkSpPer = 0;
    private float playeraddAtkRangePer = 0;
    private float playeraddMvSpPer = 0;
    private float playeraddDefendPer = 0;



    //플레이어에게 영향을 주는 종합능력치
    public float moveSpeed;
    public float maxHp;
    public float defend;
    public float cooltimePer;
    public float addDamPer;
    public float addAtkSpPer;
    public float addAtkRangePer;
    public float addMvSpPer;
    public float addDefendPer;
    public int droneNum;
    public int maxDroneNum = 3;
    public float addDataPer;

    public Vector3 moveVector;
    public GameObject shield;
    public bool isLeft = false;
    public bool invincibility = false;

    // Start is called before the first frame update
    void Start()
    {
        playermaxHp = 10f;
        rigidbody = GetComponent<Rigidbody2D>();
        playerHp = playermaxHp;

        //시험용
        GameManager.supplySoftwareSystem.SetActive(true);//소프트웨어 선택
        SupplySoftwareSystem.isset = true;
    }

    // Update is called once per frame
    void Update()
    {
        Stat();
        HpUI();
    }

    private void Stat()//if문 달아서 제어?
    {
        HardwareManager.HardwareStat();
        //SoftwareManager.RestartSoftware();
        cooltimePer = (playercooltimePer + HardwareManager.hardwarecooltimePer + SoftwareManager.softwarecooltimePer);
        addDamPer = (playeraddDamPer + HardwareManager.hardwareaddDamPer + SoftwareManager.softwareaddDamPer) + 1;
        addAtkSpPer = (playeraddAtkSpPer + HardwareManager.hardwareaddAtkSpPer +SoftwareManager.softwareaddAtkSpPer) + 1;
        addAtkRangePer = (playeraddAtkRangePer + HardwareManager.hardwareaddAtkRangePer + SoftwareManager.softwareaddAtkRangePer) + 1;
        addMvSpPer = (playeraddMvSpPer + HardwareManager.hardwareaddMvSpPer + SoftwareManager.softwareaddMvSpPer) + 1;
        addDefendPer = (playeraddDefendPer + HardwareManager.hardwareaddDefendPer + SoftwareManager.softwareaddDefendPer) + 1;
        moveSpeed = (playerSpeed + HardwareManager.hardwareMoveSpeed + SoftwareManager.softwareMoveSpeed) * addMvSpPer;
        maxHp = playermaxHp * (HardwareManager.hardwareaddMaxHpPer + SoftwareManager.softwareaddMaxHpPer + 1);
        defend = (playerdefend + HardwareManager.hardwaredefend + SoftwareManager.softwaredefend) * addDefendPer;
        droneNum = HardwareManager.hardwareDroneNum + SoftwareManager.softwareDroneNum;
        //Debug.Log(HardwareManager.hardwareDroneNum);
        //Debug.Log(SoftwareManager.softwareDroneNum);
        HardwareManager.AddStat();
        SoftwareManager.AddStat();
    }

    private void SetType()//숙련도 시스템??
    {

    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetVariables()
    {
        softwares = transform.Find("Softwares").gameObject;
        corepart = transform.Find("Hardwares/Parts/CorePart").gameObject;
        headpart = transform.Find("Hardwares/Parts/HeadPart").gameObject;
        bodypart = transform.Find("Hardwares/Parts/BodyPart").gameObject;
        leftarmpart = transform.Find("Hardwares/Parts/LeftArmPart").gameObject;
        rightarmpart = transform.Find("Hardwares/Parts/RightArmPart").gameObject;
        legpart = transform.Find("Hardwares/Parts/LegPart").gameObject;
        for (int i = 0; i < dronepart.Length; i++)
        {
            dronepart[i] = transform.Find("Hardwares/Parts/DronePart/Drone" + i).gameObject;
        }
    }

    public bool MountPart(GameObject targetpart, GameObject part)//부품 선택후 해당부위 클릭하면 그 태그가 맞는지 검사후 교체함.
    {
        if (targetpart.CompareTag(part.tag) == true)
        {
            for (int i = 0; i < targetpart.transform.childCount; i++)
                Destroy(targetpart.transform.GetChild(i).gameObject);

            part.transform.SetParent(targetpart.transform);
            part.transform.localPosition = Vector3.zero;

            return true;
        }
        else
        {
            Debug.Log("선택 파츠에는 장착할 수 없습니다.");

            return false;
        }
    }

    private void Move()//파츠들에서 변수 참고해서 만들기
    {
        //float xInput = Input.GetAxisRaw("Horizontal");
        //float yInput = Input.GetAxisRaw("Vertical");
        //
        //moveVector = new Vector3(xInput, yInput, 0).normalized;

        moveVector = FindObjectOfType<JoyStickController>().joyStickVector;

        rigidbody.MovePosition(transform.position + moveVector * moveSpeed * Time.deltaTime);

        if (moveVector.x < 0 && isLeft == false)
        {
            isLeft = true;
            transform.Find("Hardwares").localScale = new Vector3(-1, 1, 1);
            transform.Find("Hardwares/Parts/LeftArmPart").GetChild(0).transform.localPosition = Vector3.right * -1.37f;
            transform.Find("Hardwares/Parts/LeftArmPart").GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "RightArm";
            transform.Find("Hardwares/Parts/RightArmPart").GetChild(0).transform.localPosition = Vector3.right * 1.37f;
            transform.Find("Hardwares/Parts/RightArmPart").GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "LeftArm";
        }
        if (moveVector.x > 0 && isLeft == true)
        {
            isLeft = false;
            transform.Find("Hardwares").localScale = new Vector3(1, 1, 1);
            transform.Find("Hardwares/Parts/LeftArmPart").GetChild(0).transform.localPosition = Vector3.right * 0;
            transform.Find("Hardwares/Parts/LeftArmPart").GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "LeftArm";
            transform.Find("Hardwares/Parts/RightArmPart").GetChild(0).transform.localPosition = Vector3.right * 0;
            transform.Find("Hardwares/Parts/RightArmPart").GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "RightArm";
        }
    }

    public void OnDamage(float damage)
    {
        if (!invincibility)
        {
            if (shield == null)
            {
                if (playerHp - (damage * 100 / (100 + defend)) <= 0)
                    playerHp = 0;
                else
                    playerHp -= damage * 100 / (100 + defend);

                Handheld.Vibrate();
                StartCoroutine(OnDamageEffect());
            }
            else
                shield.GetComponent<Shield>().shield--;
        }
    }

    public void Restore(float restoreHp)
    {
        if (playerHp + restoreHp >= maxHp)
        {
            playerHp = maxHp;
        }
        else
            playerHp += restoreHp;
    }

    private void HpUI()
    {
        transform.Find("PlayerUI/HpSlider").GetComponent<Slider>().value = (float)playerHp / (float)maxHp * 100;
    }

    private IEnumerator OnDamageEffect()
    {
        GameObject.Find("System/OnDamage/Image").gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        GameObject.Find("System/OnDamage/Image").gameObject.SetActive(false);
    }
}
