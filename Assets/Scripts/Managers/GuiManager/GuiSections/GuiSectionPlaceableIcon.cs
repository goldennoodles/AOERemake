using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiSectionPlaceableIcon :MonoBehaviour {

    #region Variables

    [SerializeField]
    private Placeable _placeable;

    [SerializeField]
    private RawImage _image;

    ObjectPlacement objectPlacement;

    #endregion


    #region Getters and Setters

    public Placeable Placeable {
        get {
            return this._placeable;
        } set {
            this._placeable = value;
            this._image.texture = value.Image;
        }
    }

    public RawImage Image {
        get {
            return this._image;
        } set {
            this._image = value;
        }
    }

    #endregion


    #region Main Functionalitites

    public void OnClick () {
        objectPlacement.SetSelectedPlaceableObject( this._placeable );
    }

    #endregion

    #region Auxiliar Functionalities

    private void Start () {
        Setup();
        objectPlacement = GameObject.FindObjectOfType<ObjectPlacement>();
    }

    void Setup () {
        //this._image.texture = this._placeable.Image;
    }

    #endregion
}
