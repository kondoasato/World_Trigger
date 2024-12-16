using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInfo : MonoBehaviour
{
    private List<int> active_code = new List<int>(); //アクティブコード配列

    private void Start()
    {
        //コード初期化
        //for (int i = 0; i < active_code.Length; i++) { active_code[i] = 0; }
    }

    /// <summary>
    /// コード発行
    /// </summary>
    /// <returns>発行するコード</returns>
    public int Code_issued() 
    {
        int temp = 0;

        //発行するコード作成
        temp = Code_creation();

        return temp; 
    }

    /// <summary>
    /// コード作成
    /// </summary>
    private int Code_creation()
    {
        int code_index = active_code.Count;
        int temp = 0;

        //ほかのコードがあれば
        if (code_index > 0)
        {
            for (int i = 0; i < code_index; i++)
            {
                if ((i + 1) != active_code[i])
                {
                    //コード追加
                    active_code.Add(i + 1);
                    //Listを昇順にソート
                    active_code.Sort(); 
                    //作成コード代入
                    temp = i + 1;
                    break;
                }
            }
        }
        //コードが一つもなかったら
        else
        {
            //コード追加
            active_code.Add(code_index + 1);
            //Listを昇順にソート
            active_code.Sort();
            //作成コード代入
            temp = code_index + 1;
        }

        return temp;
    }
}
