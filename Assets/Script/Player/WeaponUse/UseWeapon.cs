using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponManager;

public class UseWeapon : MonoBehaviour
{
    [SerializeField]
    [Header("weaponオブジェクト")] private GameObject weapon;
    private WeaponActivate active;

    private void Start()
    {
        active = weapon.GetComponent<WeaponActivate>();
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
