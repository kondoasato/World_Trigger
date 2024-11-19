using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Calculation : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponManagerオブジェクト")] private GameObject weaponObj;

    private GameObject p_obj;               //侵入オブジェクト
    private StatusInfo info;                //StatusInfoスクリプト
    private WeaponCollider _weaponCollider; //WeaponColliderスクリプト
    private WeaponManager.WeaponID coll_id; //侵入オブジェクトID保持変数
    private WeaponManager _weaponManager;
    private int damage_num;

    private void Start()
    {
        //コンポーネント取得
        _weaponCollider = GetComponent<WeaponCollider>();
        _weaponManager = weaponObj.GetComponent<WeaponManager>();
    }

    private void Sub_HP()
    {
        info.Hp -= damage_num;
    }

    private void Update()
    {
        //当たり判定あったら
        if ( _weaponCollider.IsOn_P(ref p_obj, ref coll_id))
        {
            //コンポーネント取得
            info = p_obj.GetComponent<StatusInfo>();

            //受けるダメージ情報を取得
            damage_num = _weaponManager.GetDamage(coll_id);

            Debug.Log(damage_num);

            //hp減らす
            Sub_HP();
        }
    }
}
