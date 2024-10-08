using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHolderImage : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponImage�I�u�W�F�N�g")] private GameObject[] imageObj;

    private WeaponInfo[] weaponInfo; //WeaponInfo�X�N���v�g
    private WeaponInfo info;         //����weaponinfo�ێ��ϐ�
    private Image[] weapon_image;
    private Image this_image;

    private void Start()
    {
        //�R���|�[�l���g�擾
        weaponInfo = new WeaponInfo[imageObj.Length];
        weapon_image = new Image[imageObj.Length];
        for (int i = 0; i < weaponInfo.Length; i++)
        {
            weaponInfo[i] = imageObj[i].GetComponent<WeaponInfo>();
            weapon_image[i] = imageObj[i].GetComponent<Image>();
        }
        this_image = GetComponent<Image>();
    }

    /// <summary>
    /// �z���_�[Image�ύX
    /// </summary>
    /// <param name="weaponInfo"></param>
    public void ChangeImage(WeaponInfo weaponInfo)
    {
        for (int i = 0; i < imageObj.Length; i++)
        {
            if (weaponInfo.ID == this.weaponInfo[i].ID)
            {
                this_image.color = weapon_image[i].color;
            }
        }
    }
}
