using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInfo : MonoBehaviour
{
    [SerializeField]
    [Header("HP")] private int hp;


    /// <summary>
    /// HP�v���p�e�B
    /// </summary>
    public int Hp {  get { return hp; } set { hp = value; } }

    private void Update()
    {
        //Debug.Log(hp);
    }
}
