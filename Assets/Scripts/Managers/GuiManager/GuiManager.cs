using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager :MonoBehaviour {

    #region Variables

    [SerializeField]
    private List<GuiSection> _guiSections = new List<GuiSection>();

    #endregion

    #region Getters and Setters

    #endregion

    #region Main Functionalities

    private void Start () {
        Setup();
        //DisableAllSections();
    }

    public void EnableSectionByGuiName (string guiName) {
        GuiSection foundGuiSection = _guiSections.Find( x => x.GuiName == guiName );
        if (foundGuiSection != null)
            foundGuiSection.Enable();
    }

    public void DisableSectionByGuiName (string guiName) {
        GuiSection foundGuiSection = _guiSections.Find( x => x.GuiName == guiName );
        if (foundGuiSection != null)
            foundGuiSection.Disable();
        else
            Debug.Log( "guiName: " + guiName + " not found" );
    }

    public void DisableAllSections () {
        foreach (GuiSection guiSection in _guiSections) {
            guiSection.Disable();
        }
    }


    public void EnableAllSections () {
        foreach (GuiSection guiSection in _guiSections) {
            guiSection.Enable();
        }
    }

    #endregion

    #region Auxiliar Functionalities

    private void Setup () {
        _guiSections.AddRange( GameObject.FindObjectsOfType<GuiSection>() );
    }

    #endregion
}
