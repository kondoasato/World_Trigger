using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    [Header("通常時のスピード")] private float normalSpeed;
    [SerializeField]
    [Header("ダッシュ時のスピード")] private float sprintSpeed;

    private Rigidbody rb;
    private Vector3 cameraForward = Vector3.zero;      //カメラ
    private Vector3 moveDirection = Vector3.zero;      //方向ベクトル
    private Vector3 movingVelocity = Vector3.zero;     //速さベクトル

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // カメラの向きを基準にした正面方向のベクトル
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        float speed = normalSpeed;

        // 前後左右の入力（WASDキー）から、移動のためのベクトルを計算
        // Input.GetAxis("Vertical") は前後（WSキー）の入力値
        // Input.GetAxis("Horizontal") は左右（ADキー）の入力値
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed;  //　前後（カメラ基準）　 
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed; // 左右（カメラ基準）

        moveDirection = moveX + moveZ;

        movingVelocity = moveDirection;

        rb.velocity = movingVelocity;
    }
}
