using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    [Header("カメラが追尾するオブジェクト")] private GameObject targetObj;
    [SerializeField]
    [Header("追従するスピード")] private float followSpeed;
    private Vector3 diff; //カメラとプレイヤーの距離

    void Start()
    {
        diff = targetObj.transform.position - this.transform.position; //カメラとプレイヤーの初期の距離を指定
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(this.transform.position, 
            targetObj.transform.position - diff, Time.deltaTime * followSpeed); //線形補間関数によるカメラの移動
    }
}
