using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region//インスペクターで設定
    Vector3 latestPos;
    [SerializeField]
    [Header("監督オブジェクト")] private GameObject directorObj;
    [SerializeField]
    [Header("通常時のスピード")] private float normalSpeed;
    [SerializeField]
    [Header("ダッシュ時のスピード")] private float sprintSpeed;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    [Header("接地判定")] private GameObject groundObj;
    [SerializeField]
    [Header("ジャンプ")] private float jump;
    [SerializeField]
    [Header("重力の大きさ")] private float gravity;
    [SerializeField]
    [Header("ジャンプ加速度")] private AnimationCurve jumpCurve;
    #endregion

    #region//プライベート変数
    private Animator anim;                             //アニメーター
    private GroundCheck ground;                        //
    //private Slider_IncreaseAndDecrease stamina_slider; //sliderバー増減スクリプト
    private Vector3 move_forward;
    private Vector3 move_right;
    private bool isGround = false;                     //接地判定
    private Vector3 startPos;                          //初期位置
    private Vector3 moveDirection = Vector3.zero;      //方向ベクトル
    private Vector3 movingVelocity = Vector3.zero;     //速さベクトル
    private Vector3 cameraForward = Vector3.zero;      //カメラ
    private bool isJump = false;                       //ジャンプしているか
    private float jumpTime = 0.0f;                     //ジャンプ時間
    private bool isBound = false;                      //吹き飛んでいるか
    private float boundSpeed = 0.0f;                   //吹き飛ぶスピード
    private float boundTime = 0.0f;                    //吹き飛ぶ時間
    private float boundX = 0.0f;                       //吹き飛ぶ時のx軸方向の補正
    private float boundZ = 0.0f;                       //吹き飛ぶ時のz軸方向の補正
    private float animDamptime = 0.1f;                 //アニメーションダンプタイム
    private float max_Stamina = 10.0f;                  //スタミナ最大値
    private float now_Stamina = 0.0f;                  //現在のスタミナ
    private float increase_Stamina = 0.1f;             //スタミナ増加量
    private float increase_noStamina = 0.05f;          //スタミナ切れの時の増加量
    private float decrease_Stamina = 0.1f;             //スタミナ減少量
    private bool isSprint = false;                     //スプリント中フラグ
    private bool isNo_Stamina = false;                 //スタミナ使い切りフラグ
    #endregion

    private void Start()
    {
        //各種コンポーネント取得
        rb = GetComponent<Rigidbody>();
        ground = groundObj.GetComponent<GroundCheck>();
        //anim = GetComponent<Animator>();

        //マウスカーソルを非表示にし、位置を固定
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;



        //スタミナ設定
        now_Stamina = max_Stamina;

        //各種変数初期化
        startPos = transform.position;
        move_forward = transform.forward;
        move_right = transform.right;
    }

    /// <summary>
    /// 方向の計算
    /// </summary>
    /// <returns>方向ベクトル</returns>
    private void GetDirection()
    {
        //移動速度を取得
        float speed = normalSpeed;
        float sprint = Input.GetAxis("Sprint");

        isSprint = false;

        if (now_Stamina > 0.0f && !isNo_Stamina) //スタミナがあれば
        {
            if (sprint > 0.0f) //入力があれば
            {
                //スピード設定
                speed = sprintSpeed;
                //スタミナ減少
                now_Stamina -= decrease_Stamina;

                isSprint = true;
            }
        }

        // カメラの向きを基準にした正面方向のベクトル
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 前後左右の入力（WASDキー）から、移動のためのベクトルを計算
        // Input.GetAxis("Vertical") は前後（WSキー）の入力値
        // Input.GetAxis("Horizontal") は左右（ADキー）の入力値
        Vector3 moveZ = move_forward * Input.GetAxis("Vertical") * speed;  //　前後（カメラ基準）　 
        Vector3 moveX = move_right * Input.GetAxis("Horizontal") * speed; // 左右（カメラ基準）

        if (!isGround)
        {
            moveDirection = (moveZ + moveX) / 4.0f;
        }
        else
        {
            moveDirection = moveZ + moveX;
        }
    }

    /// <summary>
    /// スタミナ増加
    /// </summary>
    private void IncreaseStamina()
    {
        if (now_Stamina <= 0) //スタミナが0以下なら
        {
            isNo_Stamina = true;
        }

        if (isNo_Stamina) //スタミナ切れ
        {
            now_Stamina += increase_noStamina;
            if (now_Stamina >= max_Stamina)
            {
                isNo_Stamina = false;
            }
        }
        else
        {
            if (!isSprint && now_Stamina < max_Stamina) //sprint中じゃない かつ、スタミナが最大じゃないとき
            {
                now_Stamina += increase_Stamina;
            }
        }

        //stamina_slider.IsNo_value = isNo_Stamina;
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    private void Jump()
    {
        float moveY = Input.GetAxis("Jump") * jump; //上

        // isGrounded は地面にいるかどうかを判定します
        // 地面にいるときはジャンプを可能に
        if (isGround)
        {
            if (moveY > 0)
            {
                isJump = true;
                moveDirection.y = jump;

            }
        }

        //ジャンプ中
        if (isJump)
        {
            jumpTime += Time.deltaTime;
            if (jumpTime < 0.4f)
            {
                moveDirection.y = jump * jumpCurve.Evaluate(jumpTime);
            }
            else
            {
                jumpTime = 0.0f;
                isJump = false;
            }
        }

        // 重力を効かせる
        moveDirection.y -= gravity * Time.deltaTime;
    }

    /// <summary>
    /// 入力の更新
    /// </summary>
    private void Update()
    {
        //接地判定を得る
        isGround = ground.IsGround();
        //anim.SetBool("isGround", isGround);
        //anim.SetBool("isNoSprint", isNo_Stamina);
        //スタミナバーに値をセット
        //stamina_slider.SetValue(max_Stamina, now_Stamina);
    }

    /// <summary>
    /// 速さとかの更新
    /// </summary>
    private void FixedUpdate()
    {
        moveDirection = new Vector3(0, 0, 0);

        GetDirection();
        Jump();
        IncreaseStamina();

        //方向ベクトル
        Vector3 x_zVector = new Vector3(moveDirection.x, 0, moveDirection.z);

        // 移動のアニメーション
        //anim.SetFloat("MoveSpeed", (x_zVector).magnitude, animDamptime, Time.deltaTime);
        //anim.SetFloat("JumpSpeed", moveDirection.y);

        // プレイヤーの向きを入力の向きに変更　
        transform.LookAt(transform.position + x_zVector);

        movingVelocity = moveDirection;

        rb.velocity = movingVelocity;
    }
}
