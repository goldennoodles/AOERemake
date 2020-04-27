using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiSectionPlaceables :GuiSection {

    #region Variables

    [SerializeField]
    private List<String> _placeableList;
    [SerializeField]
    private GameObject _placeableIconPrefab;

    #endregion

    #region Getters and Setters

    public List<String> PlaceableList {
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

        foreach (String placeable in _placeableList) {
            GameObject icon = Instantiate( _placeableIconPrefab );
            Placeable newPlaceable = Instantiate( 
                                            Utils.GetPrefabByName<Placeable>("Placeables/"+ placeable ), 
                                            new Vector3(-100, -100, -100),
                                            Quaternion.identity );
            icon.transform.SetParent( this.gameObject.transform );
            print( newPlaceable.Image );
            icon.GetComponent<GuiSectionPlaceableIcon>().Placeable = newPlaceable;
        }
    }

    private void Test () {
        _placeableList.Add( "House" );
    }

    #endregion



}
