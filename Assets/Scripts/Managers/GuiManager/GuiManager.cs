using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    //Variables

    [SerializeField]
    List<GuiSection> guiSections = new List<GuiSection>();

    //Getters and Setters

    //Main Functionalities

    private void Start () {
        Setup();
        //DisableAllSections();
    }

    public void EnableSectionByGuiName(string guiName) {
        GuiSection foundGuiSection = guiSections.Find(x => x.GuiName == guiName);
        if (foundGuiSection != null)
            foundGuiSection.Enable();
    }

    public void DisableSectionByGuiName(string guiName) {
        GuiSection foundGuiSection = guiSections.Find(x => x.GuiName == guiName);
        if (foundGuiSection != null)
            foundGuiSection.Disable();
    }

    public void DisableAllSections () {
        foreach (GuiSection guiSection in guiSections) {
            guiSection.Disable();
        }
    }


    public void EnableAllSections () {
        foreach (GuiSection guiSection in guiSections) {
            guiSection.Enable();
        }
    }



    //Auxiliar Functionalities
    private void Setup () {
        guiSections.AddRange(GameObject.FindObjectsOfType<GuiSection>());        
    }

}
