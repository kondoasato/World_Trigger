using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenuManager : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponImageホルダーオブジェクト")] private GameObject[] holderObj;
    [SerializeField]
    [Header("ホルダーのフレームカラー")] private Color flameColor;
    [SerializeField]
    [Header("WeaponImageオブジェクト")] private Line[] lines;
    [SerializeField]
    [Header("SelectImageオブジェクト")] private GameObject selectImgObj;

    private UI_ActiveSwitch activeSwitch; //UI_ActiveSwitchスクリプト
    private Outline[] outline;
    private int sel_x;                    //現在選択番号(x軸)
    private int sel_y;                    //現在選択番号(y軸)
    private bool cool_t_flg = false;      //クールタイムフラグ
    private float cool_t = 0.0f;          //クールタイム計測
    private float cool_t_constant = 0.5f; //クールタイム
    private bool select_flg = false;      //ホルダーと武器選択画面切り替えフラグ

    /// <summary>
    /// 四則演算
    /// </summary>
    enum Operator
    {
        add,      //足し算
        sub,      //引き算
        multiply, //掛け算
        div       //割り算
    }

    private void Start()
    {
        //コンポーネント取得
        activeSwitch = GetComponent<UI_ActiveSwitch>();

        for (int i = 0; i < holderObj.Length; i++)
        {
            outline[i] = holderObj[i].GetComponent<Outline>();
        }

        //選択番号初期化
        sel_x = 0;
        sel_y = 0;

        selectImgObj.transform.position = lines[sel_y].Weapon[sel_x].transform.position;
    }

    /// <summary>
    /// 入力クールタイム処理
    /// </summary>
    /// <returns>true:クールタイム終了,false:クールタイム中</returns>
    private bool CoolTime()
    {
        bool tempflg = false;
        cool_t++;
        if (cool_t > 50f)
        {
            cool_t = 0.0f;
            tempflg = true;
        }

        return tempflg;
    }

    /// <summary>
    /// 選択制限
    /// </summary>
    /// <param name="now_num">現在選択番号</param>
    /// <param name="constant">移動定数</param>
    /// <param name="op">演算子</param>
    private int Sel_Limit(int now_num, Operator op)
    {
        int temp = now_num;

        //演算処理
        switch (op) 
        {
            case Operator.add:
                now_num++;
                break;
            case Operator.sub:
                now_num--;
                break;
        }

        //選択範囲制限
        if (now_num >= 0 && now_num < lines[0].Tile_Length)
        {
            //範囲を超えたら元の値で返す
            temp = now_num;
        }

        return temp;
    }

    /// <summary>
    /// ホルダー選択
    /// </summary>
    private void SelectHolder()
    {
        //移動入力
        //右
        if (Input.GetKey(KeyCode.D)) { sel_x = Sel_Limit(sel_x, Operator.add); cool_t_flg = false; }
        //左
        else if (Input.GetKey(KeyCode.A)) { sel_x = Sel_Limit(sel_x, Operator.sub); cool_t_flg = false; }

        //枠の色変更
        for (int i = 0; i < outline.Length; i++)
        {
            outline[i].effectColor = Color.black;
        }
        outline[sel_x].effectColor = flameColor;

        //決定(SPACEキー)入力
        if (Input.GetKey(KeyCode.Space))
        {
            select_flg = true;
        }
    }

    /// <summary>
    /// 武器をセット
    /// </summary>
    private void SetWeapon()
    {
        //移動入力
        //右
        if (Input.GetKey(KeyCode.D)) { sel_x = Sel_Limit(sel_x, Operator.add); cool_t_flg = false; }
        //左
        else if (Input.GetKey(KeyCode.A)) { sel_x = Sel_Limit(sel_x, Operator.sub); cool_t_flg = false; }
        //上
        else if (Input.GetKey(KeyCode.W)) { sel_y = Sel_Limit(sel_y, Operator.sub); cool_t_flg = false; }
        //下
        else if (Input.GetKey(KeyCode.S)) { sel_y = Sel_Limit(sel_y, Operator.add); cool_t_flg = false; }

        //選択している場所に移動
        selectImgObj.transform.position = lines[sel_y].Weapon[sel_x].transform.position;

        //決定(SPACEキー)入力
        if (Input.GetKey(KeyCode.Space))
        {

            select_flg = false;
        }

        //escキーで戻る
        if (Input.GetKey(KeyCode.Escape)) { select_flg = false; }

    }

    private void Update()
    {
        if (activeSwitch.Active)
        {
            if (cool_t_flg)
            {
                if(select_flg)
                {
                    SetWeapon();
                }
                else
                {
                    SelectHolder();
                }
            }
            //入力クールタイム
            else
            {
                if (CoolTime()) { cool_t_flg = true; }
            }
        }
        else
        {
            sel_x = 0;
            sel_y = 0; return; } //UI起動してなかったら処理終了
    }
}

[System.Serializable]
/// <summary>
/// Lineクラス
/// </summary>
public class Line
{
    [SerializeField]
    private GameObject[] weaponObj;

    public GameObject[] Weapon { get { return weaponObj; } }
    public int Tile_Length { get { return weaponObj.Length; } }
}