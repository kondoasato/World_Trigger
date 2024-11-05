using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWeaponUI : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponImageオブジェクト")] private GameObject imageObj;
    [SerializeField]
    [Header("Menuオブジェクト")] private GameObject menuObj;

    private ChangeHolderImage menu_Image;
    private ChangeHolderImage game_Image;
    private bool load_flg = false;

    private void Start()
    {
        menu_Image = imageObj.GetComponent<ChangeHolderImage>();
        game_Image = this.gameObject.GetComponent<ChangeHolderImage>();
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

                game_Image.ChangeImage(menu_Image.ID); //ゲームUI変更
                load_flg = false;
            }
        }
    }
}
