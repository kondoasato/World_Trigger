using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Calculation : StatusInfo
{
    private WeaponCollider _weaponCollider; //WeaponColliderスクリプト

    private void Start()
    {
        //コンポーネント取得
        _weaponCollider = GetComponent<WeaponCollider>();
    }


}
