using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Calculation : StatusInfo
{
    private WeaponCollider _weaponCollider; //WeaponCollider�X�N���v�g
    private WeaponManager.WeaponID coll_id; //�N���I�u�W�F�N�gID�ێ��ϐ�

    private void Start()
    {
        //�R���|�[�l���g�擾
        _weaponCollider = GetComponent<WeaponCollider>();
    }

    private void Sub_HP()
    {

    }

    private void Update()
    {
        //�����蔻�肠������
        if ( _weaponCollider.IsOn_P(ref coll_id))
        {
            //�󂯂�_���[�W�����擾


            //hp���炷
            Sub_HP();
        }
    }
}
