using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private string p_tag = "P"; //�v���C���[�E�G�̃^�O

    private bool isEnter = false; //�N���t���O
    private bool isStay = false;  //�������Ă���t���O
    private bool isExit = false;  //���ꂽ�t���O
    private bool isOn = false;    //�����蔻��t���O

    /// <summary>
    /// �����蔻���Ԃ�
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
