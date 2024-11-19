using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponManager;

public class UseWeapon : MonoBehaviour
{
    private WeaponActivate active; //WeaponActivateスクリプト
    private WeaponManager.WeaponID now_id; //現在装備中の武器ID

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
