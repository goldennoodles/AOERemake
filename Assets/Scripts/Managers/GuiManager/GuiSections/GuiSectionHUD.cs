using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiSectionHUD :GuiSection {
    #region Variables

    private PlayerMockup _player;

    [Header( "UI Controls" )]
    [SerializeField]
    private float _backgroundOffset = 2f;
    [SerializeField]
    private RectTransform _playerHpBar;
    [SerializeField]
    private RectTransform _playerHpBarBackground;

    private float fraction; //Value between 0 and 1 

    #endregion

    #region Getters and Setters

    #endregion

    #region Main Functionalities

    private void Start () {
        Setup();
    }

    private void Update () {

        //Health Points (HP)
        _playerHpBar.sizeDelta = new Vector2( _player.MaxHp, _playerHpBar.sizeDelta.y );
        _playerHpBarBackground.sizeDelta = new Vector2( _player.MaxHp + _backgroundOffset, _playerHpBarBackground.sizeDelta.y );
        fraction = _player.CurrentHp / _player.MaxHp;
        _playerHpBar.localScale = new Vector3( fraction, 1, 1 );
    }

    #endregion

    #region Auxiliar Functionalities

    private void Setup () {
        _player = FindObjectOfType<PlayerMockup>();
    }

    #endregion
}
