using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenuManager : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponImage�I�u�W�F�N�g")] private GameObject[] weaponObj;

    private UI_ActiveSwitch activeSwitch;
    private int now_select; //���ݑI��ԍ�

    private void Start()
    {
        //�R���|�[�l���g�擾
        activeSwitch = GetComponent<UI_ActiveSwitch>();
        //�I��ԍ�������
        now_select = 0;
    }

    private void Update()
    {
        if (activeSwitch.Active)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
        }
        else { return; } //UI�N�����ĂȂ������珈���I��
    }
}
