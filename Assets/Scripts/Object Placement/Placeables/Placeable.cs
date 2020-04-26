using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placeable :MonoBehaviour {

    #region Variables

    [SerializeField]
    private Texture2D _image;
    private GameObject _gameObject;

    #endregion

    #region Getters and Setters

    public Texture2D Image {
        get { return this._image; }
        set { this._image = value; }
    }

    #endregion

    #region Main Functionalities

    private void Start () {
        Setup();
    }

    #endregion

    #region Auxiliar Functionalities

    private void Setup () {
        _gameObject = this.gameObject;
    }

    #endregion
}
