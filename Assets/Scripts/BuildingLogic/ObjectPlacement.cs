using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    //in progress...

    //Strategy is to find the center of the square of each chunk and store it in a array or list

    //1. Get the Grid [OK]
    //2. Relate Grid to Chunk [OK]
    //3. Onclick -> find Grid index and chunk related
    //4. Grid index should point to a Building

    //Future Problems:
    //How to handle changes in the environment?
    //How to handle different levels in Y axis?

    //Improvements:
    //Dictionary <Chunk, List<PlacementPositions>> -> Placement position : [Building building + Vector3 position]
    //if Building == null, the position is free


    //Variables
    Dictionary<Chunk, List<Vector3>> placementGrid = new Dictionary<Chunk, List<Vector3>>();
    List<GameObject> visualRepresentation = new List<GameObject>();

    private List<Vector3> hitPoints = new List<Vector3>();

    //Getters and Setters


    //Main Functionalities
    private void Awake () {
    }

    private void Update () {
        //Just for test
        if (Input.GetKeyDown(KeyCode.T)) {
            ShowPlacementGrid();
        }

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo)) {
                hitPoints.Add(hitInfo.point);
                PlaceBuildingNear(hitInfo.point);
            }
        }

    }

    private void PlaceBuildingNear(Vector3 clickPoint) {
        Vector3 finalPosition = GetNearestPointInPlacementGrid(clickPoint);
        GameObject placedCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        placedCube.transform.position = finalPosition;
    }

    public void AddToPlacementGrid (Mesh mesh, Chunk chunk, Transform chunkTransform) {

        if (placementGrid.ContainsKey(chunk)) return;

        List<Vector3> centerPoints = new List<Vector3>();
        Vector3 firstPoint = new Vector3();
        Vector3 lastPoint = new Vector3();

        //For every square (4 vertices)
        for (int i = 0; i < mesh.vertices.Length; i += 4) {
            firstPoint = mesh.vertices [ i ];//1
            lastPoint = mesh.vertices [ i + 2 ];//3
            Vector3 centerPoint = ( ( chunkTransform.transform.TransformPoint(firstPoint) + chunkTransform.transform.TransformPoint(lastPoint) ) / 2 );
            centerPoints.Add(centerPoint);
        }
        placementGrid.Add(chunk, centerPoints);

    }

    //Auxiliar Functionalities

    private Vector3 GetNearestPointInPlacementGrid (Vector3 position, Chunk chunk = null) {

        Vector3 shortestPosition = Vector3.positiveInfinity;
        float shortestDistance = float.PositiveInfinity;
        float currentDistance;

        //Search in all chunks of the list
        if (chunk == null) {

            Vector3 currentShortestPosition = Vector3.positiveInfinity;
            foreach (KeyValuePair<Chunk, List<Vector3>> currentChunk in placementGrid) {
                currentShortestPosition = GetNearestPointInPoints(currentChunk.Value, position);
                currentDistance = Vector3.Magnitude(currentShortestPosition - position);
                if (currentDistance < shortestDistance) {
                    shortestDistance = currentDistance;
                    shortestPosition = currentShortestPosition;
                }
            }

        } else { //Search directly in the chunk
            shortestPosition = GetNearestPointInChunk(chunk, position);
        }

        return shortestPosition;

    }


    private Vector3 GetNearestPointInChunk(Chunk chunk, Vector3 position) {

        Vector3 shortestPosition = Vector3.positiveInfinity;

        List<Vector3> points;
        if (placementGrid.TryGetValue(chunk, out points)) 
            shortestPosition = GetNearestPointInPoints(points, position);

        return shortestPosition;

    }

    private Vector3 GetNearestPointInPoints(List<Vector3> points, Vector3 position) {

        float shortestDistance = float.PositiveInfinity;
        float currentDistance;
        Vector3 shortestPosition = Vector3.positiveInfinity;

        for (int i = 0; i < points.Count; i++) {
            currentDistance = Vector3.Magnitude(points [ i ] - position);
            if (currentDistance < shortestDistance) {
                shortestDistance = currentDistance;
                shortestPosition = points [ i ];
            }
        }

        return shortestPosition;

    }


    private void ShowPlacementGrid() {

        for (int i = 0; i < visualRepresentation.Count; i++)
            Object.Destroy(visualRepresentation [ i ]);
        visualRepresentation.Clear();

        foreach (KeyValuePair<Chunk, List<Vector3>> x in placementGrid) {
            for (int i = 0; i < x.Value.Count; i++) {
                GameObject centerPoint = GameObject.CreatePrimitive(PrimitiveType.Plane);
                centerPoint.transform.localScale = new Vector3(0.01f, 0.1f, 0.01f);
                centerPoint.transform.position = x.Value [ i ];
                centerPoint.transform.SetParent(this.transform);
                visualRepresentation.Add(centerPoint);
            }
        }

    }
    
}
