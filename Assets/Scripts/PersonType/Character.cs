using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Character {
    public enum characterType
    {
        Builder,
        Forager,
        Lumberjack
    }
    private characterType _characterType;
    private List<GameObject> _builderCollection = new List<GameObject>();
    public Character (characterType _chrType, GameObject _builder) {
        this._characterType = _chrType;
        this._builderCollection.Add(_builder);
    }
    
    public Vector3 CharacterGoTo (Vector3 posNow, Vector3 posToGo) {

        //doSomething.

        return Vector3.zero;
    }



}