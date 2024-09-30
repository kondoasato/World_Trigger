using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region//�C���X�y�N�^�[�Őݒ�
    Vector3 latestPos;
    [SerializeField]
    [Header("�ēI�u�W�F�N�g")] private GameObject directorObj;
    [SerializeField]
    [Header("�ʏ펞�̃X�s�[�h")] private float normalSpeed;
    [SerializeField]
    [Header("�_�b�V�����̃X�s�[�h")] private float sprintSpeed;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    [Header("�ڒn����")] private GameObject groundObj;
    [SerializeField]
    [Header("�W�����v")] private float jump;
    [SerializeField]
    [Header("�d�͂̑傫��")] private float gravity;
    [SerializeField]
    [Header("�W�����v�����x")] private AnimationCurve jumpCurve;
    #endregion

    #region//�v���C�x�[�g�ϐ�
    private Animator anim;                             //�A�j���[�^�[
    private GroundCheck ground;                        //
    //private Slider_IncreaseAndDecrease stamina_slider; //slider�o�[�����X�N���v�g
    private Vector3 move_forward;
    private Vector3 move_right;
    private bool isGround = false;                     //�ڒn����
    private Vector3 startPos;                          //�����ʒu
    private Vector3 moveDirection = Vector3.zero;      //�����x�N�g��
    private Vector3 movingVelocity = Vector3.zero;     //�����x�N�g��
    private Vector3 cameraForward = Vector3.zero;      //�J����
    private bool isJump = false;                       //�W�����v���Ă��邩
    private float jumpTime = 0.0f;                     //�W�����v����
    private bool isBound = false;                      //�������ł��邩
    private float boundSpeed = 0.0f;                   //������ԃX�s�[�h
    private float boundTime = 0.0f;                    //������Ԏ���
    private float boundX = 0.0f;                       //������Ԏ���x�������̕␳
    private float boundZ = 0.0f;                       //������Ԏ���z�������̕␳
    private float animDamptime = 0.1f;                 //�A�j���[�V�����_���v�^�C��
    private float max_Stamina = 10.0f;                  //�X�^�~�i�ő�l
    private float now_Stamina = 0.0f;                  //���݂̃X�^�~�i
    private float increase_Stamina = 0.1f;             //�X�^�~�i������
    private float increase_noStamina = 0.05f;          //�X�^�~�i�؂�̎��̑�����
    private float decrease_Stamina = 0.1f;             //�X�^�~�i������
    private bool isSprint = false;                     //�X�v�����g���t���O
    private bool isNo_Stamina = false;                 //�X�^�~�i�g���؂�t���O
    #endregion

    private void Start()
    {
        //�e��R���|�[�l���g�擾
        rb = GetComponent<Rigidbody>();
        ground = groundObj.GetComponent<GroundCheck>();
        //anim = GetComponent<Animator>();

        //�}�E�X�J�[�\�����\���ɂ��A�ʒu���Œ�
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;



        //�X�^�~�i�ݒ�
        now_Stamina = max_Stamina;

        //�e��ϐ�������
        startPos = transform.position;
        move_forward = transform.forward;
        move_right = transform.right;
    }

    /// <summary>
    /// �����̌v�Z
    /// </summary>
    /// <returns>�����x�N�g��</returns>
    private void GetDirection()
    {
        //�ړ����x���擾
        float speed = normalSpeed;
        float sprint = Input.GetAxis("Sprint");

        isSprint = false;

        if (now_Stamina > 0.0f && !isNo_Stamina) //�X�^�~�i�������
        {
            if (sprint > 0.0f) //���͂������
            {
                //�X�s�[�h�ݒ�
                speed = sprintSpeed;
                //�X�^�~�i����
                now_Stamina -= decrease_Stamina;

                isSprint = true;
            }
        }

        // �J�����̌�������ɂ������ʕ����̃x�N�g��
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �O�㍶�E�̓��́iWASD�L�[�j����A�ړ��̂��߂̃x�N�g�����v�Z
        // Input.GetAxis("Vertical") �͑O��iWS�L�[�j�̓��͒l
        // Input.GetAxis("Horizontal") �͍��E�iAD�L�[�j�̓��͒l
        Vector3 moveZ = move_forward * Input.GetAxis("Vertical") * speed;  //�@�O��i�J������j�@ 
        Vector3 moveX = move_right * Input.GetAxis("Horizontal") * speed; // ���E�i�J������j

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
    /// �X�^�~�i����
    /// </summary>
    private void IncreaseStamina()
    {
        if (now_Stamina <= 0) //�X�^�~�i��0�ȉ��Ȃ�
        {
            isNo_Stamina = true;
        }

        if (isNo_Stamina) //�X�^�~�i�؂�
        {
            now_Stamina += increase_noStamina;
            if (now_Stamina >= max_Stamina)
            {
                isNo_Stamina = false;
            }
        }
        else
        {
            if (!isSprint && now_Stamina < max_Stamina) //sprint������Ȃ� ���A�X�^�~�i���ő傶��Ȃ��Ƃ�
            {
                now_Stamina += increase_Stamina;
            }
        }

        //stamina_slider.IsNo_value = isNo_Stamina;
    }

    /// <summary>
    /// �W�����v����
    /// </summary>
    private void Jump()
    {
        float moveY = Input.GetAxis("Jump") * jump; //��

        // isGrounded �͒n�ʂɂ��邩�ǂ����𔻒肵�܂�
        // �n�ʂɂ���Ƃ��̓W�����v���\��
        if (isGround)
        {
            if (moveY > 0)
            {
                isJump = true;
                moveDirection.y = jump;

            }
        }

        //�W�����v��
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

        // �d�͂���������
        moveDirection.y -= gravity * Time.deltaTime;
    }

    /// <summary>
    /// ���͂̍X�V
    /// </summary>
    private void Update()
    {
        //�ڒn����𓾂�
        isGround = ground.IsGround();
        //anim.SetBool("isGround", isGround);
        //anim.SetBool("isNoSprint", isNo_Stamina);
        //�X�^�~�i�o�[�ɒl���Z�b�g
        //stamina_slider.SetValue(max_Stamina, now_Stamina);
    }

    /// <summary>
    /// �����Ƃ��̍X�V
    /// </summary>
    private void FixedUpdate()
    {
        moveDirection = new Vector3(0, 0, 0);

        GetDirection();
        Jump();
        IncreaseStamina();

        //�����x�N�g��
        Vector3 x_zVector = new Vector3(moveDirection.x, 0, moveDirection.z);

        // �ړ��̃A�j���[�V����
        //anim.SetFloat("MoveSpeed", (x_zVector).magnitude, animDamptime, Time.deltaTime);
        //anim.SetFloat("JumpSpeed", moveDirection.y);

        // �v���C���[�̌�������͂̌����ɕύX�@
        transform.LookAt(transform.position + x_zVector);

        movingVelocity = moveDirection;

        rb.velocity = movingVelocity;
    }
}
