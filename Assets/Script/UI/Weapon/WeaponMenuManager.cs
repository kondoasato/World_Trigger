using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenuManager : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponImage�z���_�[�I�u�W�F�N�g")] private GameObject[] holderObj;
    [SerializeField]
    [Header("�z���_�[�̃t���[���J���[")] private Color flameColor;
    [SerializeField]
    [Header("WeaponImage�I�u�W�F�N�g")] private Line[] lines;
    [SerializeField]
    [Header("SelectImage�I�u�W�F�N�g")] private GameObject selectImgObj;

    private UI_ActiveSwitch activeSwitch; //UI_ActiveSwitch�X�N���v�g
    private Outline[] outline;
    private int sel_x;                    //���ݑI��ԍ�(x��)
    private int sel_y;                    //���ݑI��ԍ�(y��)
    private bool cool_t_flg = false;      //�N�[���^�C���t���O
    private float cool_t = 0.0f;          //�N�[���^�C���v��
    private float cool_t_constant = 0.5f; //�N�[���^�C��
    private bool select_flg = false;      //�z���_�[�ƕ���I����ʐ؂�ւ��t���O

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

        for (int i = 0; i < holderObj.Length; i++)
        {
            outline[i] = holderObj[i].GetComponent<Outline>();
        }

        //�I��ԍ�������
        sel_x = 0;
        sel_y = 0;

        selectImgObj.transform.position = lines[sel_y].Weapon[sel_x].transform.position;
    }

    /// <summary>
    /// ���̓N�[���^�C������
    /// </summary>
    /// <returns>true:�N�[���^�C���I��,false:�N�[���^�C����</returns>
    private bool CoolTime()
    {
        bool tempflg = false;
        cool_t++;
        if (cool_t > 50f)
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
        if (now_num >= 0 && now_num < lines[0].Tile_Length)
        {
            //�͈͂𒴂����猳�̒l�ŕԂ�
            temp = now_num;
        }

        return temp;
    }

    /// <summary>
    /// �z���_�[�I��
    /// </summary>
    private void SelectHolder()
    {
        //�ړ�����
        //�E
        if (Input.GetKey(KeyCode.D)) { sel_x = Sel_Limit(sel_x, Operator.add); cool_t_flg = false; }
        //��
        else if (Input.GetKey(KeyCode.A)) { sel_x = Sel_Limit(sel_x, Operator.sub); cool_t_flg = false; }

        //�g�̐F�ύX
        for (int i = 0; i < outline.Length; i++)
        {
            outline[i].effectColor = Color.black;
        }
        outline[sel_x].effectColor = flameColor;

        //����(SPACE�L�[)����
        if (Input.GetKey(KeyCode.Space))
        {
            select_flg = true;
        }
    }

    /// <summary>
    /// ������Z�b�g
    /// </summary>
    private void SetWeapon()
    {
        //�ړ�����
        //�E
        if (Input.GetKey(KeyCode.D)) { sel_x = Sel_Limit(sel_x, Operator.add); cool_t_flg = false; }
        //��
        else if (Input.GetKey(KeyCode.A)) { sel_x = Sel_Limit(sel_x, Operator.sub); cool_t_flg = false; }
        //��
        else if (Input.GetKey(KeyCode.W)) { sel_y = Sel_Limit(sel_y, Operator.sub); cool_t_flg = false; }
        //��
        else if (Input.GetKey(KeyCode.S)) { sel_y = Sel_Limit(sel_y, Operator.add); cool_t_flg = false; }

        //�I�����Ă���ꏊ�Ɉړ�
        selectImgObj.transform.position = lines[sel_y].Weapon[sel_x].transform.position;

        //����(SPACE�L�[)����
        if (Input.GetKey(KeyCode.Space))
        {

            select_flg = false;
        }

        //esc�L�[�Ŗ߂�
        if (Input.GetKey(KeyCode.Escape)) { select_flg = false; }

    }

    private void Update()
    {
        if (activeSwitch.Active)
        {
            if (cool_t_flg)
            {
                if(select_flg)
                {
                    SetWeapon();
                }
                else
                {
                    SelectHolder();
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