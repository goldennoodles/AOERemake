using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTimer : MonoBehaviour
{



    // Will need to make this as a custom, instantiable class in order to avoid conflicts later.
    //TODO: This needs to be revorked because its NOT WORKING!

    bool placeItem = false;
    public float waitTime  = 3f;

    public void Update () {
        if(placeItem) {
         waitTime -= Time.deltaTime;
         Debug.Log(Mathf.RoundToInt(waitTime));
         if(waitTime <= 0) {
             Debug.Log("TRUE");
             placeItem = true;
         } else {
             Debug.Log("FALSE");
             placeItem = false;
         }
        }
   }

}
