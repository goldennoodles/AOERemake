using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placeable : MonoBehaviour
{
    //Variables

    [SerializeField]
    private Texture2D image;

    private GameObject gameObject;


    //Getters and Setters

    public Texture2D Image {
        get {
            return this.image;
        }
        set {
            this.image = value;
        }
    }


    //Main Functionalities

    private void Start() {
        Setup();
    }

    //Auxiliar Functionalities

    private void Setup() {
        gameObject = this.gameObject;
    }
}
