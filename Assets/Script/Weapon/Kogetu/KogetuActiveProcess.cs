using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogetuActiveProcess : MonoBehaviour
{
    //アクセス用変数
    public static KogetuActiveProcess instance;

    [SerializeField]
    [Header("発動時間")] private float ac_time;
    [SerializeField]
    [Header("回転角度")] private float angle;

    private enum State
    {
        None,
        start,
        motion,
        end
    }

    private Vector3 firstPos;            //初期位置
    private Quaternion firstRot;            //初期rotation
    private State nowstate = State.None; //
    private bool active_flg = false;     //発動フラグ
    private float ac_count = 0;          //発動カウント


    private void Start()
    {
        //インスタンス初期化
        instance = this;

        //初期位置登録
        firstPos = transform.localPosition;

        firstRot = transform.localRotation;
    }

    /// <summary>
    /// 発動処理
    /// </summary>
    public void Process()
    {
        active_flg = true;
    }

    public void SetPos(Vector3 pos)
    {
        firstPos = pos;
    }

    private void Update()
    {
        if (active_flg)
        {
            nowstate = State.start;
            active_flg = false;
        }

        switch (nowstate)
        {
            case State.start: //効果発動
                //移動
                transform.localPosition = new Vector3(firstPos.x,firstPos.y,0.5f);

                nowstate = State.motion; //状態移行

                break;

            case State.motion: //発動中

                //時間カウント
                ac_count += Time.deltaTime;
                //効果終了判定
                if(ac_count > ac_time)
                {
                    transform.localRotation = firstRot;
                    nowstate = State.end; //状態移行
                }
                else
                {
                    //オブジェクトx軸回転
                    Quaternion rot = Quaternion.AngleAxis(angle * ac_count, new Vector3(1.0f, 0.0f, 0.0f));
                    transform.localRotation = rot;
                }
                break;

            case State.end: //終了
                //初期位置に移動
                transform.localPosition = Vector3.zero;
                //カウント初期化
                ac_count = 0;
                nowstate = State.None;
                break;
        }
    }
}
