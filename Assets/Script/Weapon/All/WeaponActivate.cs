using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActivate : WeaponInfo
{
    [SerializeField]
    [Header("WeaponHolder�I�u�W�F�N�g")] private GameObject holderObj;
    private WeaponHolder holder;

    private void Start()
    {
        holder = holderObj.GetComponent<WeaponHolder>();
    }

    /// <summary>
    /// ���픭��
    /// </summary>
    /// <returns>����g���I����</returns>
    public float Active()
    {
        //ID�擾
        ID = holder.GetID();

        //������ʔ����������N��
        WeaponActiveProcess();

        return consumption_trion; //����g���I���ʂ�Ԃ�
    }

    /// <summary>
    /// �e����̌��ʔ�������
    /// </summary>
    private void WeaponActiveProcess()
    {
        switch(ID) //����ID���Ƃɏ���
        {
            case WeaponID.None:
                break;
            case WeaponID.Kogetu:
                KogetuActiveProcess.instance.Process();
                break;
            case WeaponID.Gun:
                break;
            case WeaponID.Asteroid:
                break;
            case WeaponID.Shield:
                break;
        }
    }
}
