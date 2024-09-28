using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    [Header("�ʏ펞�̃X�s�[�h")] private float normalSpeed;
    [SerializeField]
    [Header("�_�b�V�����̃X�s�[�h")] private float sprintSpeed;

    private Rigidbody rb;
    private Vector3 cameraForward = Vector3.zero;      //�J����
    private Vector3 moveDirection = Vector3.zero;      //�����x�N�g��
    private Vector3 movingVelocity = Vector3.zero;     //�����x�N�g��

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // �J�����̌�������ɂ������ʕ����̃x�N�g��
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        float speed = normalSpeed;

        // �O�㍶�E�̓��́iWASD�L�[�j����A�ړ��̂��߂̃x�N�g�����v�Z
        // Input.GetAxis("Vertical") �͑O��iWS�L�[�j�̓��͒l
        // Input.GetAxis("Horizontal") �͍��E�iAD�L�[�j�̓��͒l
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed;  //�@�O��i�J������j�@ 
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed; // ���E�i�J������j

        moveDirection = moveX + moveZ;

        movingVelocity = moveDirection;

        rb.velocity = movingVelocity;
    }
}
