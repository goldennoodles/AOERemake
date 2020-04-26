using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiSection :MonoBehaviour {
    #region Variables

    [SerializeField]
    private string _guiName;
    [SerializeField]
    private bool _isEnabled;

    #endregion

    #region Getters and Setters

    public string GuiName {
        get { return _guiName; }
        set { _guiName = value; }
    }

    public bool IsEnabled {
        get { return _isEnabled; }
    }

    #endregion

    #region Main Functionalities

    public void Enable () {
        _isEnabled = true;
        this.gameObject.SetActive( true );
    }

    public void Disable () {
        _isEnabled = false;
        this.gameObject.SetActive( false );
    }

    #endregion

    #region Auxiliar Functionalities

    #endregion
}
