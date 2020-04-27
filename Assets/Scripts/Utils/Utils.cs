using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 GetMousePosition3D() {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            return hit.point;
        } else {
            return Vector3.positiveInfinity;
        }
    }
}
