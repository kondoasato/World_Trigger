using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenuManager : MonoBehaviour
{
    #region//�v���C�x�[�g�ϐ�(�C���X�y�N�^�[�Őݒ�)
    [SerializeField]
    [Header("WeaponImage�z���_�[�I�u�W�F�N�g")] private GameObject[] holderObj;
    [SerializeField]
    [Header("�z���_�[�̑I�𒆃t���[���J���[")] private Color sel_flameColor;
    [SerializeField]
    [Header("�z���_�[�̑I�����t���[���J���[")] private Color done_flameColor;
    [SerializeField]
    [Header("WeaponImage�I�u�W�F�N�g")] private Line[] lines;
    [SerializeField]
    [Header("SelectImage�I�u�W�F�N�g")] private GameObject selectImgObj;
    #endregion

    #region//�v���C�x�[�g�ϐ�
    private UI_ActiveSwitch activeSwitch;  //UI_ActiveSwitch�X�N���v�g
    private ChangeHolderImage[] changeImage; //ChangeHolderImage�X�N���v�g
    private Outline[] outline;             //Outline�R���|�[�l���g
    private Image[] image;                 //Image�R���|�[�l���g 
    private WeaponInfo[][] weaponInfo;       //WeaponInfo�X�N���v�g
    private int holder_num;                //�z���_�[�I��ԍ�
    private int sel_x;                     //���ݑI��ԍ�(x��)
    private int sel_y;                     //���ݑI��ԍ�(y��)
    private bool cool_t_flg = false;       //�N�[���^�C���t���O
    private float cool_t = 0.0f;           //�N�[���^�C���v��
    private float cool_t_constant = 0.5f;  //�N�[���^�C��
    private bool select_flg = false;       //�z���_�[�ƕ���I����ʐ؂�ւ��t���O
    #endregion

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
        //�e��R���|�[�l���g�擾-----------------------------------------------
        activeSwitch = GetComponent<UI_ActiveSwitch>();

        changeImage = new ChangeHolderImage[holderObj.Length];
        outline = new Outline[holderObj.Length];
        image = new Image[holderObj.Length];
        for (int i = 0; i < holderObj.Length; i++)
        {
            changeImage[i] = holderObj[i].GetComponent<ChangeHolderImage>();
            outline[i] = holderObj[i].GetComponent<Outline>();
            image[i] = holderObj[i].GetComponent<Image>();
        }

        weaponInfo = new WeaponInfo[lines.Length][];
        for(int i = 0;i < lines.Length;i++)
        {
            lines[i].GetComponet_Info();
            weaponInfo[i] = new WeaponInfo[lines[i].Weapon_Length];
            weaponInfo[i] = lines[i].WeaponInfos;
        }
        //---------------------------------------------------------------------

        //�I��ԍ�������
        holder_num = 0;
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
    /// <param name="limit_num">�ő�l</param>
    /// <param name="op">���Z�q</param>
    private int Sel_Limit(int now_num, int limit_num, Operator op)
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
        if (now_num >= 0 && now_num < limit_num)
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
        if (Input.GetKey(KeyCode.D)) { holder_num = Sel_Limit(holder_num,outline.Length, Operator.add); cool_t_flg = false; }
        //��
        else if (Input.GetKey(KeyCode.A)) { holder_num = Sel_Limit(holder_num, outline.Length, Operator.sub); cool_t_flg = false; }

        //�g�̐F�ύX
        for (int i = 0; i < outline.Length; i++)
        {
            outline[i].effectColor = Color.black;
        }
        outline[holder_num].effectColor = sel_flameColor;

        //����(SPACE�L�[)����
        if (Input.GetKey(KeyCode.Space))
        {
            outline[holder_num].effectColor = done_flameColor;
            cool_t_flg = false;
            //����Z�b�g�Ɉڍs
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
        if (Input.GetKey(KeyCode.D)) { sel_x = Sel_Limit(sel_x, lines[0].Weapon_Length, Operator.add); cool_t_flg = false; }
        //��
        else if (Input.GetKey(KeyCode.A)) { sel_x = Sel_Limit(sel_x, lines[0].Weapon_Length, Operator.sub); cool_t_flg = false; }
        //��
        else if (Input.GetKey(KeyCode.W)) { sel_y = Sel_Limit(sel_y, lines.Length, Operator.sub); cool_t_flg = false; }
        //��
        else if (Input.GetKey(KeyCode.S)) { sel_y = Sel_Limit(sel_y, lines.Length, Operator.add); cool_t_flg = false; }

        //�I�����Ă���ꏊ�Ɉړ�
        selectImgObj.transform.position = lines[sel_y].Weapon[sel_x].transform.position;

        //����(SPACE�L�[)����
        if (Input.GetKey(KeyCode.Space))
        {
            //�z���_�[�摜�ύX
            changeImage[holder_num].ChangeImage(weaponInfo[sel_y][sel_x].ID);
            cool_t_flg = false;
            select_flg = false;
        }

        //esc�L�[�Ŗ߂�
        if (Input.GetKey(KeyCode.Escape)) { select_flg = false; }

    }

    /// <summary>
    /// �X�V����
    /// </summary>
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

    private WeaponInfo[] weaponInfo; //WeaponInfo�X�N���v�g

    public GameObject[] Weapon { get { return weaponObj; } }
    public int Weapon_Length { get { return weaponObj.Length; } }

    /// <summary>
    /// weaponInfo�v���p�e�B
    /// </summary>
    public WeaponInfo[] WeaponInfos { get { return weaponInfo; } }

    /// <summary>
    /// WeaponInfo�R���|�[�l���g�擾
    /// </summary>
    public void GetComponet_Info()
    {
        //�R���|�[�l���g�擾
        weaponInfo = new WeaponInfo[weaponObj.Length];
        for (int i = 0; i < weaponInfo.Length; i++)
        {
            weaponInfo[i] = weaponObj[i].GetComponent<WeaponInfo>();
        }
    }
}