using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private string p_tag = "P"; //�v���C���[�E�G�̃^�O
    private WeaponInfo info;    //������ϐ�

    private bool isEnter = false; //�N���t���O
    private bool isStay = false;  //�������Ă���t���O
    private bool isExit = false;  //���ꂽ�t���O
    private bool isOn = false;    //�����蔻��t���O

    private void Start()
    {
        info = GetComponent<WeaponInfo>();
    }

    /// <summary>
    /// �����蔻���Ԃ�
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
