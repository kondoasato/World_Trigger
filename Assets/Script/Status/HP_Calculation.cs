using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Calculation : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponManager�I�u�W�F�N�g")] private GameObject weaponObj;

    private GameObject p_obj;               //�N���I�u�W�F�N�g
    private StatusInfo info;                //StatusInfo�X�N���v�g
    private WeaponCollider _weaponCollider; //WeaponCollider�X�N���v�g
    private WeaponManager.WeaponID coll_id; //�N���I�u�W�F�N�gID�ێ��ϐ�
    private WeaponManager _weaponManager;
    private int damage_num;

    private void Start()
    {
        //�R���|�[�l���g�擾
        _weaponCollider = GetComponent<WeaponCollider>();
        _weaponManager = weaponObj.GetComponent<WeaponManager>();
    }

    private void Sub_HP()
    {
        info.Hp -= damage_num;
    }

    private void Update()
    {
        //�����蔻�肠������
        if ( _weaponCollider.IsOn_P(ref p_obj, ref coll_id))
        {
            //�R���|�[�l���g�擾
            info = p_obj.GetComponent<StatusInfo>();

            //�󂯂�_���[�W�����擾
            damage_num = _weaponManager.GetDamage(coll_id);

            Debug.Log(damage_num);

            //hp���炷
            Sub_HP();
        }
    }
}
