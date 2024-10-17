using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWeapon : MonoBehaviour
{
    [SerializeField]
    [Header("Weaponオブジェクト(Prefab)")] private GameObject[] weaponObj;
    [SerializeField]
    [Header("WeaponImageオブジェクト")] private GameObject imageObj;
    [SerializeField]
    [Header("Menuオブジェクト")] private GameObject menuObj;

    private ChangeHolderImage menu_Image;  //weaponImageについてるchangeHolderスクリプト
    private WeaponInfo[] info;             //WeaponInfoスクリプト
    private WeaponManager.WeaponID now_id; //現在の武器ID
    private bool load_flg = false;


    private void Start()
    {
        //各種コンポーネント取得
        menu_Image = imageObj.GetComponent<ChangeHolderImage>();
        info = new WeaponInfo[weaponObj.Length];
        for (int i = 0; i < weaponObj.Length; i++)
        {
            info[i] = weaponObj[i].GetComponent<WeaponInfo>();
        }
    }

    private void Update()
    {
        if (menuObj.activeSelf) //メニューオブジェクトONの時
        {
            // すべての子オブジェクトを取得
            foreach (Transform n in this.gameObject.transform)
            {
                GameObject.Destroy(n.gameObject); //子オブジェクト削除
            }
            load_flg = true;
        }
        else
        {
            if (load_flg) //フラグ起動してたら
            {
                for (int i = 0; i < weaponObj.Length; i++)
                {
                    //IDが一致していたら
                    if (menu_Image.ID == info[i].ID)
                    {
                        now_id = menu_Image.ID; //現在ID変更
                        Instantiate(weaponObj[i],this.gameObject.transform); //武器オブジェクト生成
                    }
                }
                load_flg = false;
            }
        }
    }
}
