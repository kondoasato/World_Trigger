using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : WeaponManager
{
    [SerializeField]
    [Header("ID")] private WeaponID id;
    [SerializeField]
    [Header("����g���I����")] private float consumption_trion;

    public WeaponID ID { get { return id; } set { id = value; } }
}
