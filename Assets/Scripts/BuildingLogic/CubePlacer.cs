using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacer : MonoBehaviour
{

    public Material CubeMaterial;

    private Grid grid;

    private BuildingTimer buildingTimer;

    [SerializeField]
    private List<Vector3> hitPoints;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        buildingTimer = gameObject.AddComponent<BuildingTimer>();
        hitPoints = new List<Vector3>();
    }

    private bool toPlace = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hitInfo))
            {
                hitPoints.Add(hitInfo.point);
                PlaceCubeNear(hitInfo.point);
                toPlace = true;
            }
            
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
            var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
            finalPosition.y = 0.7f;

            GameObject placedCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            placedCube.GetComponent<MeshRenderer>().material = CubeMaterial;

            placedCube.transform.position = finalPosition;

        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }
}