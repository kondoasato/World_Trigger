using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWeapon : MonoBehaviour
{
    [SerializeField]
    [Header("Weapon�I�u�W�F�N�g(Prefab)")] private GameObject[] weaponObj;
    [SerializeField]
    [Header("WeaponImage�I�u�W�F�N�g")] private GameObject imageObj;
    [SerializeField]
    [Header("Menu�I�u�W�F�N�g")] private GameObject menuObj;

    private ChangeHolderImage menu_Image;
    private bool load_flg = false;

    private void Start()
    {
        menu_Image = imageObj.GetComponent<ChangeHolderImage>();
    }

    private void Update()
    {
        if (menuObj.activeSelf) //���j���[�I�u�W�F�N�g�I�t�̎�
        {
            load_flg = true;
        }
        else
        {
            if (load_flg) //�t���O�N�����Ă���
            {
                for (int i = 0; i < weaponObj.Length; i++)
                {
                }
                load_flg = false;
            }
        }
    }
}
