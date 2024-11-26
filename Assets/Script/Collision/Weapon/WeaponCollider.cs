using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private WeaponInfo info;     //武器情報変数
    private StatusInfo p_obj;    //侵入オブジェクト変数
    private HP_Calculation hp_C; //HP_Calculationスクリプト

    private void Start()
    {
        info = GetComponent<WeaponInfo>();
        hp_C = GetComponent<HP_Calculation>();
    }

    private void OnTriggerEnter(Collider other) 
    {

        if (other.gameObject.GetComponent<HP_Calculation>())
        {
            other.gameObject.GetComponent<HP_Calculation>().Sub_HP(info.ID);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.GetComponent<HP_Calculation>())
        {
            other.gameObject.GetComponent<HP_Calculation>().Sub_HP(info.ID);
        }
    }
}
