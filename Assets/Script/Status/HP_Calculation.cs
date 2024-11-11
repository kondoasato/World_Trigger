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

    private void Sub_HP()
    {

    }

    private void Update()
    {
        if ( _weaponCollider.IsOn_P())
        {
            //hp減らす
            Sub_HP();
        }
    }
}
