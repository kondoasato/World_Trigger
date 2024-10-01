using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActiveSwitch : MonoBehaviour
{
    [SerializeField]
    [Header("UIオブジェクト")] private GameObject UIObj;
    [SerializeField]
    [Header("初期状態(true:Active,false:非Active)")] private bool first_active;

    private bool isWait = false;    //クールタイムフラグ
    private float waitTime = 0.0f;  //入力クールタイム
    private float cooltime = 0.2f; //クールタイム

    public bool Active { get { return UIObj.activeSelf; } }

    private void Start()
    {
        UIObj.SetActive(first_active);
    }

    private void Update()
    {
        if (isWait) //クールタイム中
        {
            if(waitTime > cooltime) //クールタイムを超えたら
            {
                waitTime = 0.0f;
                isWait = false;
            }
            else
            { waitTime += Time.deltaTime; }
        }
        else
        {
            if (UIObj.activeSelf) //オブジェクト起動時
            {
                Time.timeScale = 0.0f;
                if (Input.GetKey(KeyCode.Tab))
                {
                    UIObj.SetActive(false); //オブジェクトオフ
                    Time.timeScale = 1.0f;
                    isWait = true; //クールタイム起動
                }
            }
            else //オブジェクト非起動時
            {
                if (Input.GetKey(KeyCode.Tab))
                {
                    UIObj.SetActive(true); //オブジェクト起動
                    isWait = true; //クールタイム起動
                }
            }
        }
    }
}
