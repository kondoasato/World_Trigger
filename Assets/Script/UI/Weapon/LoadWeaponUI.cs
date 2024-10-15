using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWeaponUI : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponImage�I�u�W�F�N�g")] private GameObject imageObj;
    [SerializeField]
    [Header("Menu�I�u�W�F�N�g")] private GameObject menuObj;

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
        if (menuObj.activeSelf) //���j���[�I�u�W�F�N�g�I�t�̎�
        {
            load_flg = true;
        }
        else
        {
            if (load_flg) //�t���O�N�����Ă���
            {

                game_Image.ChangeImage(menu_Image.ID); //�Q�[��UI�ύX
                load_flg = false;
            }
        }
    }
}
