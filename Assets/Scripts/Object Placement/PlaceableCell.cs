using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableCell {
    #region Variables

    [SerializeField]
    private Chunk _chunk;

    [SerializeField]
    private Vector3 _position;

    [SerializeField]
    private Placeable _placeable;

    #endregion

    #region Getters and Setters

    public Chunk Chunk {
        get { return this._chunk; }
        set { this._chunk = value; }
    }

    public Vector3 Position {
        get { return this._position; }
        set { this._position = value; }
    }

    public Placeable Placeable {
        get { return this._placeable; }
        set { this._placeable = value; }
    }

    #endregion

    #region Main Functionalities

    public PlaceableCell (Vector3 position, Placeable placeable, Chunk chunk) {
        this.Position = position;
        this.Placeable = placeable;
        this.Chunk = chunk;
    }

    public bool IsFree () {
        return ( this.Placeable == null ) ? true : false;
    }

    public void AddPlaceable (Placeable newPlaceable) {
        this.Placeable = newPlaceable;
        newPlaceable.transform.position = this.Position;
        newPlaceable.gameObject.transform.SetParent( this.Chunk.transform );
    }

    public void RemovePlaceable () {
        this.Placeable = null;
    }

    #endregion

    #region Auxiliar Functionalities

    #endregion

}

