using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private string GroundTag = "Ground"; //Ground�^�O
    private string SlopeTag = "Slope"; //Slope�^�O
    private bool isEnter = false;
    private bool isStay = false;
    private bool isExit = false;
    private bool isOn = false;

    /// <summary>
    /// �ڒn�����Ԃ�
    /// </summary>
    public bool IsGround()
    {
        if (isEnter || isStay)
        {
            isOn = true;
        }
        else if (isExit) { isOn = false; }

        isEnter = false;
        isStay = false;
        isExit = false;

        return isOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isGround = (other.tag == GroundTag);
        bool isSlope = (other.tag == SlopeTag);

        if (isGround || isSlope)
        {
            isEnter = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        bool isGround = (other.tag == GroundTag);
        bool isSlope = (other.tag == SlopeTag);

        if (isGround || isSlope)
        {
            isStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool isGround = (other.tag == GroundTag);
        bool isSlope = (other.tag == SlopeTag);

        if (isGround || isSlope)
        {
            isExit = true;
        }
    }
}
