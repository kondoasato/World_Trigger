using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private WeaponInfo info;     //������ϐ�
    private StatusInfo p_obj;    //�N���I�u�W�F�N�g�ϐ�
    private HP_Calculation hp_C; //HP_Calculation�X�N���v�g

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
