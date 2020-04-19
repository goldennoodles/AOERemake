using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiSectionHUD : GuiSection
{
    //Variables

    PlayerMockup player;

    [Header("UI Controls")]
    [SerializeField]
    float backgroundOffset = 2f;
    [SerializeField]
    RectTransform playerHpBar;
    [SerializeField]
    RectTransform playerHpBarBackground;

    private float fraction; //Value between 0 and 1

    //Getters and Setters

    //Main Functionalities

    private void Start () {
        Setup();
    }

    private void Update () {

        //Health Points (HP)
        playerHpBar.sizeDelta = new Vector2(player.MaxHp, playerHpBar.sizeDelta.y);
        playerHpBarBackground.sizeDelta = new Vector2(player.MaxHp + backgroundOffset, playerHpBarBackground.sizeDelta.y);
        fraction = player.CurrentHp / player.MaxHp;
        playerHpBar.localScale = new Vector3(fraction, 1, 1);
    }


    //Auxiliar Functionalities

    private void Setup () {
        player = FindObjectOfType<PlayerMockup>();
    }
}
