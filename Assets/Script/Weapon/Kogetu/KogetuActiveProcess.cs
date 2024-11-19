using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogetuActiveProcess : MonoBehaviour
{
    //�A�N�Z�X�p�ϐ�
    public static KogetuActiveProcess instance;

    [SerializeField]
    [Header("��������")] private float ac_time;
    [SerializeField]
    [Header("��]�p�x")] private float angle;

    private enum State
    {
        None,
        start,
        motion,
        end
    }

    private Vector3 firstPos;            //�����ʒu
    private Quaternion firstRot;            //����rotation
    private State nowstate = State.None; //
    private bool active_flg = false;     //�����t���O
    private float ac_count = 0;          //�����J�E���g


    private void Start()
    {
        //�C���X�^���X������
        instance = this;

        //�����ʒu�o�^
        firstPos = transform.localPosition;

        firstRot = transform.localRotation;
    }

    /// <summary>
    /// ��������
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
            case State.start: //���ʔ���
                //�ړ�
                transform.localPosition = new Vector3(firstPos.x,firstPos.y,0.5f);

                nowstate = State.motion; //��Ԉڍs

                break;

            case State.motion: //������

                //���ԃJ�E���g
                ac_count += Time.deltaTime;
                //���ʏI������
                if(ac_count > ac_time)
                {
                    transform.localRotation = firstRot;
                    nowstate = State.end; //��Ԉڍs
                }
                else
                {
                    //�I�u�W�F�N�gx����]
                    Quaternion rot = Quaternion.AngleAxis(angle * ac_count, new Vector3(1.0f, 0.0f, 0.0f));
                    transform.localRotation = rot;
                }
                break;

            case State.end: //�I��
                //�����ʒu�Ɉړ�
                transform.localPosition = Vector3.zero;
                //�J�E���g������
                ac_count = 0;
                nowstate = State.None;
                break;
        }
    }
}
