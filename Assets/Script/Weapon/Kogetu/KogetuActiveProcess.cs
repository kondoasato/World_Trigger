using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogetuActiveProcess : MonoBehaviour
{
    //アクセス用変数
    public static KogetuActiveProcess instance;

    [SerializeField]
    [Header("発動時間")] private float ac_time;

    private enum State
    {
        None,
        start,
        motion,
        end
    }

    private Vector3 firstPos;            //初期位置
    private State nowstate = State.None; //
    private bool active_flg = false;     //発動フラグ
    private float ac_count = 0;          //発動カウント

    private void Start()
    {
        //インスタンス初期化
        instance = this;

        //初期位置登録
        firstPos = transform.position;
    }

    /// <summary>
    /// 発動処理
    /// </summary>
    public void Process()
    {
        active_flg = true;
    }

    private void Update()
    {
        if (active_flg)
        {
            nowstate = State.start;
        }

        switch (nowstate)
        {
            case State.start: //効果発動
                //移動
                transform.position = firstPos;
                transform.position = new Vector3(firstPos.x, firstPos.y, firstPos.z + 0.5f);
                nowstate = State.motion; //状態移行
                break;

            case State.motion: //発動中
                Debug.Log("t"); //ここ来てない
                //時間カウント
                ac_count += Time.deltaTime;
                //効果終了判定
                if(ac_count > ac_time)
                {
                    nowstate = State.end; //状態移行
                }
                else
                {
                    //オブジェクトx軸回転
                    Quaternion rot = Quaternion.AngleAxis(10.0f, new Vector3(1.0f, 0.0f, 0.0f));
                    transform.rotation = rot;
                }
                break;

            case State.end: //終了
                //初期位置に移動
                transform.position = firstPos;
                //カウント初期化
                ac_count = 0;
                nowstate = State.None;
                break;
        }
    }
}
