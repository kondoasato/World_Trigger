using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActivate : WeaponInfo
{
    [SerializeField]
    [Header("WeaponHolderオブジェクト")] private GameObject holderObj;
    private WeaponHolder holder;

    private void Start()
    {
        holder = holderObj.GetComponent<WeaponHolder>();
    }

    /// <summary>
    /// 武器発動
    /// </summary>
    /// <returns>消費トリオン量</returns>
    public float Active()
    {
        //ID取得
        ID = holder.GetID();

        //武器効果発動処理を起動
        WeaponActiveProcess();

        return consumption_trion; //消費トリオン量を返す
    }

    /// <summary>
    /// 各武器の効果発動処理
    /// </summary>
    private void WeaponActiveProcess()
    {
        switch(ID) //武器IDごとに処理
        {
            case WeaponID.None:
                break;
            case WeaponID.Kogetu:
                KogetuActiveProcess.instance.Process();
                break;
            case WeaponID.Gun:
                break;
            case WeaponID.Asteroid:
                break;
            case WeaponID.Shield:
                break;
        }
    }
}
