using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField]
    [Header("Weapon�I�u�W�F�N�g")] private GameObject[] weaponObj;
    [SerializeField]
    [Header("WeaponUI�I�u�W�F�N�g")] private GameObject[] weaponUIObj;
    [SerializeField]
    [Header("Menu�I�u�W�F�N�g")] private GameObject menuObj;

    private WeaponManager.WeaponID[] now_id; //����ID�ێ��z��
    private SetWeapon[] set;                   //SetWeapon�X�N���v�g
    private int now_index = 0;               //���݂̕���ԍ�
    private bool change_flg = false;         //����؂�ւ��҂��t���O
    private bool load_flg = false;           //
    private Vector3 defaultScale;            //UI�f�t�H���gScale

    private void Start()
    {
        //ID������------------------------------------------------
        now_id = new WeaponManager.WeaponID[weaponObj.Length];
        set = new SetWeapon[weaponObj.Length];
        for (int i = 0; i < weaponObj.Length; i++)
        {
            now_id[i] = WeaponManager.WeaponID.None;

            //�R���|�[�l���g�擾
            set[i] = weaponObj[i].GetComponent<SetWeapon>();
        }
        //--------------------------------------------------------

        //����ԍ�������
        now_index = 1;

        //�f�t�H���gScale�o�^
        defaultScale = weaponUIObj[now_index].transform.localScale;


        //�\���؂�ւ�
        WeaponActiveSwitch();
        //UI�g��
        ExpansionUI();
        change_flg = false;
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
            //ID�擾
            now_id[now_index - 1] = set[now_index - 1].Now_id;
        }
    }

    /// <summary>
    /// �I�𒆂̕���UI�g��
    /// </summary>
    private void ExpansionUI()
    {
        for (int i = 0; i < weaponUIObj.Length; i++)
        {
            weaponUIObj[i].transform.localScale = defaultScale; //Scale������

            //���ݑI�𒆂Ȃ�
            if(i == now_index - 1)
            {
                weaponUIObj[i].transform.localScale *= 1.5f; //Scale���{
            }
        }
    }

    /// <summary>
    /// ����ID�擾
    /// </summary>
    public WeaponManager.WeaponID GetID()
    {
        return now_id[now_index - 1];
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void Update()
    {
        if (change_flg) //���͂��ꂽ�畐��؂�ւ��������s
        {
            //�\���؂�ւ�
            WeaponActiveSwitch();
            //UI�g��
            ExpansionUI();
            change_flg = false;
        }
        else //����؂�ւ����͂��������
        {
            ChangeWeapon();
        }

        if (menuObj.activeSelf) //���j���[�I�u�W�F�N�g�I���̎�
        {
            load_flg = true;
        }
        else
        {
            if (load_flg) //�t���O�N�����Ă���
            {
                //�\���؂�ւ�
                WeaponActiveSwitch();
                load_flg = false;
            }
        }
    }
}
