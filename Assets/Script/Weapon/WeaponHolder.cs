using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField]
    [Header("Weaponオブジェクト")] private GameObject[] weaponObj;
    [SerializeField]
    [Header("WeaponUIオブジェクト")] private GameObject[] weaponUIObj;

    private WeaponManager.WeaponID[] now_id; //現在ID保持配列
    private int now_index;                   //現在の武器番号
    private bool change_flg = false;         //武器切り替え待ちフラグ

    private void Start()
    {
        //ID初期化------------------------------------------------
        now_id = new WeaponManager.WeaponID[weaponObj.Length];
        for (int i = 0; i < weaponObj.Length; i++)
        {
            now_id[i] = WeaponManager.WeaponID.None;
        }
        //--------------------------------------------------------


    }

    /// <summary>
    /// 武器切り替え
    /// </summary>
    private void ChangeWeapon()
    {
        if (Input.GetKey(KeyCode.Alpha1)) { now_index = 1; change_flg = true; }
        else if (Input.GetKey(KeyCode.Alpha2)) { now_index = 2; change_flg = true; }
        else if (Input.GetKey(KeyCode.Alpha3)) { now_index = 3; change_flg = true; }
        else if (Input.GetKey(KeyCode.Alpha4)) { now_index = 4; change_flg = true; }
    }

    private void Update()
    {
        if (change_flg)
        {

            change_flg = false;
        }
        else
        {
            ChangeWeapon();
        }
    }
}
