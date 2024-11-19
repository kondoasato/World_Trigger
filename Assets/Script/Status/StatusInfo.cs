using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInfo : MonoBehaviour
{
    [SerializeField]
    [Header("HP")] private int hp;


    /// <summary>
    /// HPプロパティ
    /// </summary>
    public int Hp {  get { return hp; } set { hp = value; } }

    private void Update()
    {
        //Debug.Log(hp);
    }
}
