using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInfo : MonoBehaviour
{
    private List<int> active_code = new List<int>(); //�A�N�e�B�u�R�[�h�z��

    private void Start()
    {
        //�R�[�h������
        //for (int i = 0; i < active_code.Length; i++) { active_code[i] = 0; }
    }

    /// <summary>
    /// �R�[�h���s
    /// </summary>
    /// <returns>���s����R�[�h</returns>
    public int Code_issued() 
    {
        int temp = 0;

        //���s����R�[�h�쐬
        temp = Code_creation();

        return temp; 
    }

    /// <summary>
    /// �R�[�h�쐬
    /// </summary>
    private int Code_creation()
    {
        int code_index = active_code.Count;
        int temp = 0;

        //�ق��̃R�[�h�������
        if (code_index > 0)
        {
            for (int i = 0; i < code_index; i++)
            {
                if ((i + 1) != active_code[i])
                {
                    //�R�[�h�ǉ�
                    active_code.Add(i + 1);
                    //List�������Ƀ\�[�g
                    active_code.Sort(); 
                    //�쐬�R�[�h���
                    temp = i + 1;
                    break;
                }
            }
        }
        //�R�[�h������Ȃ�������
        else
        {
            //�R�[�h�ǉ�
            active_code.Add(code_index + 1);
            //List�������Ƀ\�[�g
            active_code.Sort();
            //�쐬�R�[�h���
            temp = code_index + 1;
        }

        return temp;
    }
}
