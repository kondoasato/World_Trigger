using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogetuActiveProcess : MonoBehaviour
{
    //�A�N�Z�X�p�ϐ�
    public static KogetuActiveProcess instance;

    [SerializeField]
    [Header("��������")] private float ac_time;

    private enum State
    {
        None,
        start,
        motion,
        end
    }

    private Vector3 firstPos;            //�����ʒu
    private State nowstate = State.None; //
    private bool active_flg = false;     //�����t���O
    private float ac_count = 0;          //�����J�E���g

    private void Start()
    {
        //�C���X�^���X������
        instance = this;

        //�����ʒu�o�^
        firstPos = transform.position;
    }

    /// <summary>
    /// ��������
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
            case State.start: //���ʔ���
                //�ړ�
                transform.position = firstPos;
                transform.position = new Vector3(firstPos.x, firstPos.y, firstPos.z + 0.5f);
                nowstate = State.motion; //��Ԉڍs
                break;

            case State.motion: //������
                Debug.Log("t"); //�������ĂȂ�
                //���ԃJ�E���g
                ac_count += Time.deltaTime;
                //���ʏI������
                if(ac_count > ac_time)
                {
                    nowstate = State.end; //��Ԉڍs
                }
                else
                {
                    //�I�u�W�F�N�gx����]
                    Quaternion rot = Quaternion.AngleAxis(10.0f, new Vector3(1.0f, 0.0f, 0.0f));
                    transform.rotation = rot;
                }
                break;

            case State.end: //�I��
                //�����ʒu�Ɉړ�
                transform.position = firstPos;
                //�J�E���g������
                ac_count = 0;
                nowstate = State.None;
                break;
        }
    }
}
