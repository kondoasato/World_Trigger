using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenuManager : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponImage�I�u�W�F�N�g")] private Line[] lines;

    private UI_ActiveSwitch activeSwitch; //UI_ActiveSwitch�X�N���v�g
    private int sel_x;                    //���ݑI��ԍ�(x��)
    private int sel_y;                    //���ݑI��ԍ�(y��)
    private bool cool_t_flg = false;      //�N�[���^�C���t���O
    private float cool_t = 0.0f;          //�N�[���^�C���v��
    private float cool_t_constant = 0.5f; //�N�[���^�C��


    /// <summary>
    /// �l�����Z
    /// </summary>
    enum Operator
    {
        add,      //�����Z
        sub,      //�����Z
        multiply, //�|���Z
        div       //����Z
    }

    private void Start()
    {
        //�R���|�[�l���g�擾
        activeSwitch = GetComponent<UI_ActiveSwitch>();

        //�I��ԍ�������
        sel_x = 0;
        sel_y = 0;
    }

    /// <summary>
    /// ���̓N�[���^�C������
    /// </summary>
    /// <returns>true:�N�[���^�C���I��,false:�N�[���^�C����</returns>
    private bool CoolTime()
    {
        bool tempflg = false;
        cool_t += Time.deltaTime;
        if (cool_t > 0.35f)
        {
            cool_t = 0.0f;
            tempflg = true;
        }

        return tempflg;
    }

    /// <summary>
    /// �I�𐧌�
    /// </summary>
    /// <param name="now_num">���ݑI��ԍ�</param>
    /// <param name="constant">�ړ��萔</param>
    /// <param name="op">���Z�q</param>
    private int Sel_Limit(int now_num, Operator op)
    {
        int temp = now_num;

        //���Z����
        switch (op) 
        {
            case Operator.add:
                now_num++;
                break;
            case Operator.sub:
                now_num--;
                break;
        }

        //�I��͈͐���
        if (now_num >= 0 && now_num < lines.Length)
        {
            //�͈͂𒴂����猳�̒l�ŕԂ�
            temp = now_num;
        }

        return temp;
    }

    private void Update()
    {
        if (activeSwitch.Active)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            float decision = Input.GetAxis("Decision");

            if (cool_t_flg)
            {
                //�ړ�����
                //�E
                if (horizontal > 0) { sel_x = Sel_Limit(sel_x, Operator.add); cool_t_flg = false; }
                //��
                else if (horizontal < 0) { sel_x = Sel_Limit(sel_x, Operator.sub); cool_t_flg = false; }
                //��
                else if (vertical > 0) { sel_y = Sel_Limit(sel_y, Operator.sub); cool_t_flg = false; }
                //��
                else if (vertical < 0) { sel_y = Sel_Limit(sel_y, Operator.add); cool_t_flg = false; }

                //����(SPACE�L�[)����
                if (decision > 0)
                {
                }
            }
            //���̓N�[���^�C��
            else
            {
                if (CoolTime()) { cool_t_flg = true; }
            }
        }
        else
        {
            sel_x = 0;
            sel_y = 0; return; } //UI�N�����ĂȂ������珈���I��
    }
}

[System.Serializable]
/// <summary>
/// Line�N���X
/// </summary>
public class Line
{
    [SerializeField]
    private GameObject[] weaponObj;

    public GameObject[] Weapon { get { return weaponObj; } }
    public int Tile_Length { get { return weaponObj.Length; } }
}