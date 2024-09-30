using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    [Header("�J�������ǔ�����I�u�W�F�N�g")] private GameObject targetObj;
    [SerializeField]
    [Header("�Ǐ]����X�s�[�h")] private float followSpeed;
    private Vector3 diff; //�J�����ƃv���C���[�̋���

    void Start()
    {
        diff = targetObj.transform.position - this.transform.position; //�J�����ƃv���C���[�̏����̋������w��
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(this.transform.position, 
            targetObj.transform.position - diff, Time.deltaTime * followSpeed); //���`��Ԋ֐��ɂ��J�����̈ړ�
    }
}
