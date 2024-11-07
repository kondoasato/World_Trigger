using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private string p_tag = "P"; //プレイヤー・敵のタグ

    private bool isEnter = false; //侵入フラグ
    private bool isStay = false;  //当たっているフラグ
    private bool isExit = false;  //離れたフラグ
    private bool isOn = false;    //当たり判定フラグ

    /// <summary>
    /// 当たり判定を返す
    /// </summary>
    public bool IsOn_P()
    {
        if (isEnter || isStay)
        {
            isOn = true;
        }
        else if (isExit) { isOn = false; }

        isEnter = false;
        isStay = false;
        isExit = false;

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
