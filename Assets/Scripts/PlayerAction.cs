using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;

    Rigidbody2D rigid;
    Animator anim;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;
    GameObject scanObject;

    //////무기/////

    public float DreamGauge;
    public GameObject[] Equipments;
    public bool[] hasEquip; // 0.달팽이풀 1.열쇠 2.책 3.촛불
    GameObject equipEquipment;
    GameObject nearObject;
    int equipEquipmentIndex = -1;
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool sDown4;
    bool iDown;
    public item equip; //장착 아이템 
    


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    /*void GetInput()
    {
        sDown1 = Input.GetButtonUp("Swap1");
        sDown2 = Input.GetButtonUp("Swap2");
        sDown3 = Input.GetButtonUp("Swap3");
        sDown4 = Input.GetButtonUp("Swap4");
    }*/
    // Update is called once per frame
    void Update()
    {
        //GetInput();
        //Swap();
        // move value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //Check button Down & Up
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        iDown = Input.GetButtonDown("interaction");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
        sDown4 = Input.GetButtonDown("Swap4");

        if (sDown1==true)
            Debug.Log("!!");
        
        // Check Horizontal Move

        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        // Animation
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);

        // Direction
        if (vDown && v == 1)
        {
            dirVec = Vector3.up;
        }
        else if (vDown && v == -1)
        {
            dirVec = Vector3.down;
        }
        else if (hDown && h == -1)
        {
            dirVec = Vector3.left;
        }
        else if (hDown && h == 1)
        {
            dirVec = Vector3.right;
        }

        // Scan Object
        if (Input.GetKey(KeyCode.E) && scanObject != null && scanObject.CompareTag("GoToOutDoor"))
        {
            //Debug.Log("This is :" + scanObject.name);
            this.transform.position = new Vector3(0, -46, 0);
            //manager.Action(scanObject);
        }

        if (Input.GetKey(KeyCode.E) && scanObject != null && scanObject.CompareTag("GoToInsideDoor"))
        {
            //Debug.Log("This is :" + scanObject.name);
            this.transform.position = new Vector3(-6, -9, 0);
            //manager.Action(scanObject);


        }

        //Swap 함수
        if (sDown1 && (!hasEquip[0] || equipEquipmentIndex == 0)) return;
        if (sDown2 && (!hasEquip[1] || equipEquipmentIndex == 1)) return;
        if (sDown3 && (!hasEquip[2] || equipEquipmentIndex == 2)) return;
        if (sDown4 && (!hasEquip[3] || equipEquipmentIndex == 3)) return;


        int EquipIndex = -1;
        if (sDown1)
        {
            EquipIndex = 0;
            Debug.Log("sDown1");
        }
        if (sDown2) EquipIndex = 1;
        if (sDown3) EquipIndex = 2;
        if (sDown4) EquipIndex = 3;

        if (sDown1 || sDown2 || sDown3 || sDown4)
        {
            if (equipEquipment != null)
                equipEquipment.SetActive(false);
            Debug.Log("equipEquipmentIndex");
            equipEquipmentIndex = EquipIndex;
            equipEquipment = Equipments[EquipIndex];
            equipEquipment.SetActive(true);
        }

        //interaction 함수

        if (iDown && nearObject != null)
        {
            if(nearObject.tag == "SnailGrass")
            {
                hasEquip[0] = true;

                //Destroy(nearObject);
            }
            else if (nearObject.tag == "key")
            {
                hasEquip[1] = true;

                Destroy(nearObject);
            }
            else if (nearObject.tag == "book")
            {
                hasEquip[2] = true;

                Destroy(nearObject);
            }
            else if (nearObject.tag == "candle")
            {
                hasEquip[3] = true;

                Destroy(nearObject);
            }
            else if (nearObject.tag == "GoToOutDoor")
            {
        
            }
            else if (nearObject.tag == "GoToInsideDoor")
            {

            }


        }

    }

    void FixedUpdate()
    {
        // Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        // Ray
        Debug.DrawRay(rigid.position, dirVec * 1.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }
    /*void Swap()
    {
        
        if (sDown1 && (!hasEquip[0] || equipEquipmentIndex == 0)) return;
        if (sDown2 && (!hasEquip[1] || equipEquipmentIndex == 1)) return;
        if (sDown3 && (!hasEquip[2] || equipEquipmentIndex == 2)) return;
        if (sDown4 && (!hasEquip[3] || equipEquipmentIndex == 3)) return;


        int EquipIndex = -1;
        if (sDown1) { EquipIndex = 0;
            Debug.Log("sDown1");
        }
        if (sDown2) EquipIndex = 1;
        if (sDown3) EquipIndex = 2;
        if (sDown4) EquipIndex = 3;

        if (sDown1||sDown2||sDown3||sDown4)
        {
            if(equipEquipment != null)
                equipEquipment.SetActive(false);
            Debug.Log("equipEquipmentIndex");
            equipEquipmentIndex = EquipIndex;
            equipEquipment = Equipments[EquipIndex];
            equipEquipment.SetActive(true);
        }

    }*/
    void OnTriggerStay2D(Collider2D collision) /////충돌 체크 문제 해결 필요
    {

        if (collision.CompareTag("SnailGrass") && collision.CompareTag("key") && collision.CompareTag("book") && collision.CompareTag("candle"))
        { 
            nearObject = collision.gameObject;
            Debug.Log("stay");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Equipment")
            nearObject = null;
    }

}
