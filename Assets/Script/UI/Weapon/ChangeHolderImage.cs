using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHolderImage : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponImageオブジェクト")] private GameObject[] imageObj;

    private WeaponInfo[] weaponInfo;       //WeaponInfoスクリプト
    private WeaponManager.WeaponID now_id; //現在ID保持変数
    private Image[] weapon_image;
    private Image this_image;

    public WeaponManager.WeaponID ID { get { return now_id; } }

    private void Start()
    {
        //コンポーネント取得
        weaponInfo = new WeaponInfo[imageObj.Length];
        weapon_image = new Image[imageObj.Length];
        for (int i = 0; i < weaponInfo.Length; i++)
        {
            weaponInfo[i] = imageObj[i].GetComponent<WeaponInfo>();
            weapon_image[i] = imageObj[i].GetComponent<Image>();
        }
        this_image = this.gameObject.GetComponent<Image>();

        now_id = WeaponManager.WeaponID.None;
    }

    /// <summary>
    /// ホルダーImage変更
    /// </summary>
    /// <param name="weaponInfo"></param>
    public void ChangeImage(WeaponManager.WeaponID temp_id)
    {
        now_id = temp_id;
        for (int i = 0; i < imageObj.Length; i++)
        {
            if (temp_id == this.weaponInfo[i].ID)
            {
                this_image.color = weapon_image[i].color;
            }
        }
    }
}
