using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableCell
{
    //Variables
    [SerializeField]
    private Vector3 position;

    [SerializeField]
    private Placeable placeable;

    //Getters and Setters

    public Vector3 Position {
        get { return this.position; }
        set { this.position = value; }
    }

    public Placeable Placeable
    {
        get { return this.placeable; }
        set { this.placeable = value; }
    }

    //Constructor

    public PlaceableCell(Vector3 position, Placeable placeable)
    {
        this.position = position;
        this.placeable = placeable;
    }

    //Main Functionalities

    public bool IsFree(){
        return (this.placeable == null) ? true : false;
    }

    public void AddPlaceable(Placeable newPlaceable) {
        this.Placeable = newPlaceable;
    }

    public void RemovePlaceable() {
        this.Placeable = null;
    }
    
}
