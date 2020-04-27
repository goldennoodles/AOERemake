using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiSectionPlaceables :GuiSection {

    #region Variables

    [SerializeField]
    private List<Placeable> _placeableList;
    [SerializeField]
    private GameObject _placeableIconPrefab;

    #endregion

    #region Getters and Setters

    public List<Placeable> PlaceableList {
        get { return _placeableList; }
        set { _placeableList = value; }
    }

    #endregion


    #region Main Functionalities

    private void Start () {
        Setup();
    }

    #endregion

    #region Auxiliar Functionalities

    private void Setup () {
        //throw new NotImplementedException();
        //Test();

        foreach (Placeable placeable in _placeableList) {
            GameObject icon = Instantiate( _placeableIconPrefab );
            icon.transform.SetParent( this.gameObject.transform );
            //icon.GetComponent<GuiSectionPlaceableIcon>().Placeable = placeable;
        }
    }

    private void Test () {
        _placeableList.Add( new Placeable() );
        _placeableList.Add( new Placeable() );
    }

    #endregion



}
