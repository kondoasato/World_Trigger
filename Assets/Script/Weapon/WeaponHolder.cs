using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField]
    [Header("Weapon�I�u�W�F�N�g")] private GameObject[] weaponObj;
    [SerializeField]
    [Header("WeaponUI�I�u�W�F�N�g")] private GameObject[] weaponUIObj;

    private WeaponManager.WeaponID[] now_id; //����ID�ێ��z��
    private int now_index;                   //���݂̕���ԍ�
    private bool change_flg = false;         //����؂�ւ��҂��t���O

    private void Start()
    {
        //ID������------------------------------------------------
        now_id = new WeaponManager.WeaponID[weaponObj.Length];
        for (int i = 0; i < weaponObj.Length; i++)
        {
            now_id[i] = WeaponManager.WeaponID.None;
        }
        //--------------------------------------------------------

        //����ԍ�������
        now_index = 1;
    }

    /// <summary>
    /// ����؂�ւ�
    /// </summary>
    private void ChangeWeapon()
    {
        if (Input.GetKey(KeyCode.Alpha1)) { now_index = 1; change_flg = true; }
        else if (Input.GetKey(KeyCode.Alpha2)) { now_index = 2; change_flg = true; }
        else if (Input.GetKey(KeyCode.Alpha3)) { now_index = 3; change_flg = true; }
        else if (Input.GetKey(KeyCode.Alpha4)) { now_index = 4; change_flg = true; }
    }

    /// <summary>
    /// ����I�u�W�F�N�g�\���؂�ւ�
    /// </summary>
    private void WeaponActiveSwitch()
    {
        //�q�I�u�W�F�N�g�擾
        foreach (Transform i in this.gameObject.transform)
        {
            //���I�u�W�F�N�g�擾
            foreach (Transform j in i.gameObject.transform)
            {
                //�I�u�W�F�N�g��\��
                j.gameObject.SetActive(false);
            }
        }

        //���ݑI��ԍ��̎q�I�u�W�F�N�g�擾
        foreach (Transform i in weaponObj[now_index - 1].transform)
        {
            //�I�u�W�F�N�g�\��
            i.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (change_flg) //���͂��ꂽ�畐��؂�ւ��������s
        {
            //�\���؂�ւ�
            WeaponActiveSwitch();
            //UI�g��

            change_flg = false;
        }
        else //����؂�ւ����͂��������
        {
            ChangeWeapon();
        }
    }
}
