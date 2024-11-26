using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Calculation : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponManagerオブジェクト")] private GameObject weaponObj;
    [SerializeField]
    [Header("ダメージクールタイム")] private float cool_t;

    private StatusInfo info;                //StatusInfoスクリプト
    private WeaponManager _weaponManager;   //WeaponManagerスクリプト
    private int damage_num;                 //ダメージ値
    private bool damage_flg = false;        //ダメージフラグ
    private float time = 0;                 //クールタイム計測

    /// <summary>
    /// damage_flgプロパティ
    /// </summary>
    public bool Damage_flg {  get { return damage_flg; } set { damage_flg = value; } }
    
    private void Start()
    {
        //コンポーネント取得
        info = GetComponent<StatusInfo>();
        _weaponManager = weaponObj.GetComponent<WeaponManager>();
    }

    /// <summary>
    /// ダメージ計算
    /// </summary>
    /// <param name="coll_id">侵入オブジェクトのID</param>
    public void Sub_HP(WeaponManager.WeaponID coll_id)
    {
        if (damage_flg)
        {
            return;
        }
        else
        {
            //受けるダメージ情報を取得
            damage_num = _weaponManager.GetDamage(coll_id);

            info.Hp -= damage_num;
            damage_flg = true;
        }
    }

    private void Update()
    {
        if (damage_flg) //ダメージ処理
        {
            time += Time.deltaTime;

            if (time > cool_t)
            {
                damage_flg = false;
            }
        }
    }
}
