using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiSectionPlaceableIcon : MonoBehaviour
{
    //Variables

    [SerializeField]
    Placeable placeable;

    [SerializeField]
    RawImage image;

    ObjectPlacement objectPlacement;

    //Getters and Setters

    //Main Functionalitites
    public void OnClick() {
        objectPlacement.SetSelectedPlaceableObject( this.placeable );
    }

    //Auxiliar Functionalities

    private void Start() {
        Setup();
        objectPlacement = GameObject.FindObjectOfType<ObjectPlacement>();
    }

    void Setup() {
        image.texture = placeable.Image;
    }
    
}
