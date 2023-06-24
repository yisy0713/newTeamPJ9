using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerAction player;
    //public Boss boss;
    //public int stage;
    public float playtime;
    public bool isBattle;

    public Image Equip1; //장착한 아이템 번호
    public Image Equip2; //장착한 아이템 번호
    public Image Equip3; //장착한 아이템 번호
    public Image Equip4; //장착한 아이템 번호
    public RectTransform DreamGaugeGroup;
    public RectTransform DreamGaugeBar;

    // Start is called before the first frame update
    void Start()
    {
        //playtime += Time.deltaTime;

    }

    void LateUpdate()
    {
        
        Equip1.color = new Color(1, 1, 1, player.hasEquip[0] ? 1 : 0);
        Equip2.color = new Color(1, 1, 1, player.hasEquip[1] ? 1 : 0);
        Equip3.color = new Color(1, 1, 1, player.hasEquip[2] ? 1 : 0);
        Equip4.color = new Color(1, 1, 1, player.hasEquip[3] ? 1 : 0);

        DreamGaugeBar.localScale = new Vector3(1/5, 1, 1);
    }
}
