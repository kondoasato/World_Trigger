using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActiveSwitch : MonoBehaviour
{
    [SerializeField]
    [Header("UI�I�u�W�F�N�g")] private GameObject UIObj;
    [SerializeField]
    [Header("�������(true:Active,false:��Active)")] private bool first_active;

    private bool isWait = false;    //�N�[���^�C���t���O
    private float waitTime = 0.0f;  //���̓N�[���^�C��
    private float cooltime = 0.2f; //�N�[���^�C��

    public bool Active { get { return UIObj.activeSelf; } }

    private void Start()
    {
        UIObj.SetActive(first_active);
    }

    private void Update()
    {
        if (isWait) //�N�[���^�C����
        {
            if(waitTime > cooltime) //�N�[���^�C���𒴂�����
            {
                waitTime = 0.0f;
                isWait = false;
            }
            else
            { waitTime += Time.deltaTime; }
        }
        else
        {
            if (UIObj.activeSelf) //�I�u�W�F�N�g�N����
            {
                Time.timeScale = 0.0f;
                if (Input.GetKey(KeyCode.Tab))
                {
                    UIObj.SetActive(false); //�I�u�W�F�N�g�I�t
                    Time.timeScale = 1.0f;
                    isWait = true; //�N�[���^�C���N��
                }
            }
            else //�I�u�W�F�N�g��N����
            {
                if (Input.GetKey(KeyCode.Tab))
                {
                    UIObj.SetActive(true); //�I�u�W�F�N�g�N��
                    isWait = true; //�N�[���^�C���N��
                }
            }
        }
    }
}
