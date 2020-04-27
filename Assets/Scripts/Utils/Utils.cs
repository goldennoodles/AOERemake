using UnityEngine;

public static class Utils {

    public static T GetPrefabByName<T> ( string name ) where T : class {
        Debug.Log( "Prefabs/" + name );
        return Resources.Load( "Prefabs/" + name, typeof( T ) ) as T;
    }

    public static Vector3 GetMousePosition3D () {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        RaycastHit hit;
        if (Physics.Raycast( ray, out hit )) {
            return hit.point;
        } else {
            return Vector3.positiveInfinity;
        }
    }
}
