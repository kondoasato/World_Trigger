using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Calculation : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponManager�I�u�W�F�N�g")] private GameObject weaponObj;
    [SerializeField]
    [Header("�_���[�W�N�[���^�C��")] private float cool_t;

    private StatusInfo info;                //StatusInfo�X�N���v�g
    private WeaponManager _weaponManager;   //WeaponManager�X�N���v�g
    private int damage_num;                 //�_���[�W�l
    private bool damage_flg = false;        //�_���[�W�t���O
    private float time = 0;                 //�N�[���^�C���v��

    /// <summary>
    /// damage_flg�v���p�e�B
    /// </summary>
    public bool Damage_flg {  get { return damage_flg; } set { damage_flg = value; } }
    
    private void Start()
    {
        //�R���|�[�l���g�擾
        info = GetComponent<StatusInfo>();
        _weaponManager = weaponObj.GetComponent<WeaponManager>();
    }

    /// <summary>
    /// �_���[�W�v�Z
    /// </summary>
    /// <param name="coll_id">�N���I�u�W�F�N�g��ID</param>
    public void Sub_HP(WeaponManager.WeaponID coll_id)
    {
        if (damage_flg)
        {
            return;
        }
        else
        {
            //�󂯂�_���[�W�����擾
            damage_num = _weaponManager.GetDamage(coll_id);

            info.Hp -= damage_num;
            damage_flg = true;
        }
    }

    private void Update()
    {
        if (damage_flg) //�_���[�W����
        {
            time += Time.deltaTime;

            if (time > cool_t)
            {
                damage_flg = false;
            }
        }
    }
}
