using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Calculation : StatusInfo
{
    private WeaponCollider _weaponCollider; //WeaponColliderスクリプト
    private WeaponManager.WeaponID coll_id; //侵入オブジェクトID保持変数

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
        //当たり判定あったら
        if ( _weaponCollider.IsOn_P(ref coll_id))
        {
            //受けるダメージ情報を取得


            //hp減らす
            Sub_HP();
        }
    }
}
