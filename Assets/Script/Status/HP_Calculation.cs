using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Calculation : StatusInfo
{
    private WeaponCollider _weaponCollider; //WeaponCollider�X�N���v�g

    private void Start()
    {
        //�R���|�[�l���g�擾
        _weaponCollider = GetComponent<WeaponCollider>();
    }


}
