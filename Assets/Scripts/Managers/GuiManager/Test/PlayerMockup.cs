using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMockup :MonoBehaviour {
    //This class is generate only for testing purpose of the GUIManager
    //and should be replaced soon

    #region Variables

    float maxHpPossibleInGame = 700;
    [SerializeField]
    private float _currentHp;
    [SerializeField]
    private float _maxHp;

    #endregion

    #region Getters and Setters

    public float CurrentHp {
        get { return _currentHp; }
        set {
            _currentHp = value;
            if (_currentHp > _maxHp)
                _currentHp = _maxHp;
            else if (_currentHp < 0)
                _currentHp = 0;
        }
    }

    public float MaxHp {
        get { return _maxHp; }
        set {
            _maxHp = value;
            if (_currentHp > _maxHp)
                _currentHp = _maxHp;
            if (_maxHp < 1) {
                _currentHp = 1;
                _maxHp = 1;
            }
            if (_maxHp > maxHpPossibleInGame)
                _maxHp = maxHpPossibleInGame;
        }
    }

    #endregion
}
