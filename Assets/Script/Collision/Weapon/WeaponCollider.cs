using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private string p_tag = "P"; //プレイヤー・敵のタグ
    private WeaponInfo info;    //武器情報変数

    private bool isEnter = false; //侵入フラグ
    private bool isStay = false;  //当たっているフラグ
    private bool isExit = false;  //離れたフラグ
    private bool isOn = false;    //当たり判定フラグ

    private void Start()
    {
        info = GetComponent<WeaponInfo>();
    }

    /// <summary>
    /// 当たり判定を返す
    /// </summary>
    public bool IsOn_P(ref WeaponInfo.WeaponID id)
    {
        if (isEnter || isStay)
        {
            isOn = true;
        }
        else if (isExit) { isOn = false; }

        isEnter = false;
        isStay = false;
        isExit = false;

        id = info.ID;

        return isOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isOn_p = (other.tag == p_tag);

        if (isOn_p)
        {
            isEnter = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        bool isOn_p = (other.tag == p_tag);

        if (isOn_p)
        {
            isStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool isOn_p = (other.tag == p_tag);

        if (isOn_p)
        {
            isExit = true;
        }
    }
}
