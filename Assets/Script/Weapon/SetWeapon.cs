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

    private ChangeHolderImage menu_Image;
    private bool load_flg = false;

    private void Start()
    {
        menu_Image = imageObj.GetComponent<ChangeHolderImage>();
    }

    private void Update()
    {
        if (menuObj.activeSelf) //メニューオブジェクトオフの時
        {
            load_flg = true;
        }
        else
        {
            if (load_flg) //フラグ起動してたら
            {
                for (int i = 0; i < weaponObj.Length; i++)
                {
                }
                load_flg = false;
            }
        }
    }
}
