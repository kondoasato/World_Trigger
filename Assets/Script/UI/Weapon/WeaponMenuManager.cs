using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenuManager : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponImageオブジェクト")] private GameObject[] weaponObj;

    private UI_ActiveSwitch activeSwitch;
    private int now_select; //現在選択番号

    private void Start()
    {
        //コンポーネント取得
        activeSwitch = GetComponent<UI_ActiveSwitch>();
        //選択番号初期化
        now_select = 0;
    }

    private void Update()
    {
        if (activeSwitch.Active)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
        }
        else { return; } //UI起動してなかったら処理終了
    }
}
