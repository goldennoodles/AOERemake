using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiSection : MonoBehaviour
{
    //Variables
    [SerializeField]
    private string guiName;
    [SerializeField]
    private bool isEnabled;

    //Getters and Setters
    public string GuiName {
        get { return guiName; }
        set { guiName = value; }
    }

    public bool IsEnabled {
        get { return isEnabled; }
    }

    //Main Functionalities
    public void Enable () {
        isEnabled = true;
        this.gameObject.SetActive(true);
    }

    public void Disable() {
        isEnabled = false;
        this.gameObject.SetActive(false);
    }


    //Auxiliar Functionalities
}
