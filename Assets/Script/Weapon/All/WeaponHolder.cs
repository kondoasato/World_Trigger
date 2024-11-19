using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField]
    [Header("Weaponオブジェクト")] private GameObject[] weaponObj;
    [SerializeField]
    [Header("WeaponUIオブジェクト")] private GameObject[] weaponUIObj;
    [SerializeField]
    [Header("Menuオブジェクト")] private GameObject menuObj;

    private WeaponManager.WeaponID[] now_id; //現在ID保持配列
    private SetWeapon[] set;                   //SetWeaponスクリプト
    private int now_index = 0;               //現在の武器番号
    private bool change_flg = false;         //武器切り替え待ちフラグ
    private bool load_flg = false;           //
    private Vector3 defaultScale;            //UIデフォルトScale

    private void Start()
    {
        //ID初期化------------------------------------------------
        now_id = new WeaponManager.WeaponID[weaponObj.Length];
        set = new SetWeapon[weaponObj.Length];
        for (int i = 0; i < weaponObj.Length; i++)
        {
            now_id[i] = WeaponManager.WeaponID.None;

            //コンポーネント取得
            set[i] = weaponObj[i].GetComponent<SetWeapon>();
        }
        //--------------------------------------------------------

        //武器番号初期化
        now_index = 1;

        //デフォルトScale登録
        defaultScale = weaponUIObj[now_index].transform.localScale;


        //表示切り替え
        WeaponActiveSwitch();
        //UI拡大
        ExpansionUI();
        change_flg = false;
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

    /// <summary>
    /// 武器オブジェクト表示切り替え
    /// </summary>
    private void WeaponActiveSwitch()
    {
        //子オブジェクト取得
        foreach (Transform i in this.gameObject.transform)
        {
            //孫オブジェクト取得
            foreach (Transform j in i.gameObject.transform)
            {
                //オブジェクト非表示
                j.gameObject.SetActive(false);
            }
        }

        //現在選択番号の子オブジェクト取得
        foreach (Transform i in weaponObj[now_index - 1].transform)
        {
            //オブジェクト表示
            i.gameObject.SetActive(true);
            //ID取得
            now_id[now_index - 1] = set[now_index - 1].Now_id;
        }
    }

    /// <summary>
    /// 選択中の武器UI拡大
    /// </summary>
    private void ExpansionUI()
    {
        for (int i = 0; i < weaponUIObj.Length; i++)
        {
            weaponUIObj[i].transform.localScale = defaultScale; //Scale初期化

            //現在選択中なら
            if(i == now_index - 1)
            {
                weaponUIObj[i].transform.localScale *= 1.5f; //Scaleを二倍
            }
        }
    }

    /// <summary>
    /// 武器ID取得
    /// </summary>
    public WeaponManager.WeaponID GetID()
    {
        return now_id[now_index - 1];
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        if (change_flg) //入力されたら武器切り替え処理実行
        {
            //表示切り替え
            WeaponActiveSwitch();
            //UI拡大
            ExpansionUI();
            change_flg = false;
        }
        else //武器切り替え入力うけつけ状態
        {
            ChangeWeapon();
        }

        if (menuObj.activeSelf) //メニューオブジェクトオンの時
        {
            load_flg = true;
        }
        else
        {
            if (load_flg) //フラグ起動してたら
            {
                //表示切り替え
                WeaponActiveSwitch();
                load_flg = false;
            }
        }
    }
}
