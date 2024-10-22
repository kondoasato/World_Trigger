using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogetuActiveProcess : MonoBehaviour
{
    //アクセス用変数
    public static KogetuActiveProcess instance;

    private enum State
    {
        None,
        start,
        motion,
        end
    }

    private Vector3 firstPos;        //初期位置
    private State nowstate = State.None;
    private bool active_flg = false; //発動フラグ

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
        }
    }
}
