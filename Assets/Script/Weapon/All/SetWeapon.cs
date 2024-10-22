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

    private ChangeHolderImage menu_Image;  //weaponImage�ɂ��Ă�changeHolder�X�N���v�g
    private WeaponInfo[] info;             //WeaponInfo�X�N���v�g
    private WeaponManager.WeaponID now_id; //���݂̕���ID
    private bool load_flg = false;


    private void Start()
    {
        //�e��R���|�[�l���g�擾
        menu_Image = imageObj.GetComponent<ChangeHolderImage>();
        info = new WeaponInfo[weaponObj.Length];
        for (int i = 0; i < weaponObj.Length; i++)
        {
            info[i] = weaponObj[i].GetComponent<WeaponInfo>();
        }
    }

    private void Update()
    {
        if (menuObj.activeSelf) //���j���[�I�u�W�F�N�gON�̎�
        {
            // ���ׂĂ̎q�I�u�W�F�N�g���擾
            foreach (Transform n in this.gameObject.transform)
            {
                GameObject.Destroy(n.gameObject); //�q�I�u�W�F�N�g�폜
            }
            load_flg = true;
        }
        else
        {
            if (load_flg) //�t���O�N�����Ă���
            {
                for (int i = 0; i < weaponObj.Length; i++)
                {
                    //ID����v���Ă�����
                    if (menu_Image.ID == info[i].ID)
                    {
                        now_id = menu_Image.ID; //����ID�ύX
                        Instantiate(weaponObj[i],this.gameObject.transform); //����I�u�W�F�N�g����
                    }
                }
                load_flg = false;
            }
        }
    }
}
