using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponManager;

public class UseWeapon : MonoBehaviour
{
    private WeaponActivate active; //WeaponActivate�X�N���v�g
    private WeaponManager.WeaponID now_id; //���ݑ������̕���ID

    private void Start()
    {
        active = GetComponent<WeaponActivate>();
    }

    private void Update()
    {
        float fire = Input.GetAxis("Fire1");
        if(fire > 0 )
        {
            active.Active();
        }
    }
}
