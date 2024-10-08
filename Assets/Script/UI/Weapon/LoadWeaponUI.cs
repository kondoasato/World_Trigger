using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWeaponUI : MonoBehaviour
{
    [SerializeField]
    [Header("WeaponImageオブジェクト")] private GameObject imageObj;

    private ChangeHolderImage menu_Image;
    private ChangeHolderImage game_Image;

    private void Start()
    {
        menu_Image = imageObj.GetComponent<ChangeHolderImage>();
        game_Image = GetComponent<ChangeHolderImage>();
    }

    private void Update()
    {
    }
}
