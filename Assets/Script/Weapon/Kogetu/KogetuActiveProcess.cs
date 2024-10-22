using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogetuActiveProcess : MonoBehaviour
{
    //�A�N�Z�X�p�ϐ�
    public static KogetuActiveProcess instance;

    private enum State
    {
        None,
        start,
        motion,
        end
    }

    private Vector3 firstPos;        //�����ʒu
    private State nowstate = State.None;
    private bool active_flg = false; //�����t���O

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
        }
    }
}
