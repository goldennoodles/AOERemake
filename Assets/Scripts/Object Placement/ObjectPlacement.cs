using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    //in progress...

    //Strategy is to find the center of the square of each chunk and store it in a array or list

    //1. Get the Grid [OK]
    //2. Relate Grid to Chunk [OK]
    //3. Onclick -> find Grid index and chunk related [OK]
    //4. Grid index should point to a Building [OK]

    //Future Problems:
    //How to handle changes in the environment?
    //How to handle different levels in Y axis?


    //Variables
    Dictionary<Chunk, List<PlaceableCell>> placementGrid = new Dictionary<Chunk, List<PlaceableCell>>();
    List<GameObject> visualRepresentation = new List<GameObject>();

    private List<Vector3> hitPoints = new List<Vector3>();

    [SerializeField]
    Placeable selectedPlaceableObject;
    GameObject selectedPleacableGameObject;

    //Getters and Setters



    //Main Functionalities

    private void Update () {

        //Just for test
        if (Input.GetKeyDown(KeyCode.T)) {
            ShowPlacementGrid();
        }

        if (selectedPlaceableObject != null) {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            if (Physics.Raycast( ray, out hitInfo )) {
                hitPoints.Add( hitInfo.point );
                MoveSelectedPlaceableObject( hitInfo.point );
            }

            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("Placed");
                PlaceSelectedPlaceableObject( hitInfo.point );
            }
        }

    }

    public void SetSelectedPlaceableObject(Placeable placeableSelected) {
        selectedPlaceableObject = placeableSelected;
        selectedPleacableGameObject = Instantiate( selectedPlaceableObject.gameObject );
    }


    private void MoveSelectedPlaceableObject( Vector3 mousePoint ) {

        PlaceableCell cellToPlace = GetNearestPlaceableCell( mousePoint );

        if (cellToPlace.IsFree()) {
            selectedPleacableGameObject.transform.position = cellToPlace.Position;
        }

    }

    private void PlaceSelectedPlaceableObject(Vector3 clickPoint) {

        PlaceableCell cellToPlace = GetNearestPlaceableCell(clickPoint);

        if (cellToPlace.IsFree()) {

            selectedPleacableGameObject.transform.position = cellToPlace.Position;
            cellToPlace.AddPlaceable( selectedPleacableGameObject.GetComponent<Placeable>() );
            selectedPlaceableObject = null;

        }
        
    }

    public void AddToPlacementGrid (Mesh mesh, Chunk chunk, Transform chunkTransform) {

        if (placementGrid.ContainsKey(chunk)) return;

        List<Vector3> centerPoints = new List<Vector3>();
        Vector3 firstPoint = new Vector3();
        Vector3 lastPoint = new Vector3();
        List<PlaceableCell> placeableCells = new List<PlaceableCell>();

        //For every square (4 vertices) store the center point
        for (int i = 0; i < mesh.vertices.Length; i += 4) {
            firstPoint = mesh.vertices [ i ];//1
            lastPoint = mesh.vertices [ i + 2 ];//3
            Vector3 centerPoint = ( ( chunkTransform.transform.TransformPoint(firstPoint) + chunkTransform.transform.TransformPoint(lastPoint) ) / 2 );
            centerPoints.Add(centerPoint);
            PlaceableCell cell = new PlaceableCell(centerPoint, null);
            placeableCells.Add(cell);
        }
        
        placementGrid.Add(chunk, placeableCells);

    }



    //Auxiliar Functionalities

    private void Start() {
        Setup();
    }

    private void Setup() {
        selectedPlaceableObject = null;
    }

    private PlaceableCell GetNearestPlaceableCell(Vector3 position, Chunk chunk = null){

        PlaceableCell nearestPlaceableCell = null;
        float shortestDistance = float.PositiveInfinity;
        float currentDistance;

        //Search in all chunks of the list
        if (chunk == null) {

            PlaceableCell currentNearestPlaceableCell = null;
            foreach (KeyValuePair<Chunk, List<PlaceableCell>> currentChunk in placementGrid) {
                currentNearestPlaceableCell = GetNearestPlaceableCell(currentChunk.Value, position);
                currentDistance = Vector3.Magnitude(currentNearestPlaceableCell.Position - position);
                if (currentDistance < shortestDistance) {
                    shortestDistance = currentDistance;
                    nearestPlaceableCell = currentNearestPlaceableCell;
                }
            }

        }
        else
        { //Search directly in the chunk
            nearestPlaceableCell = GetNearestPlaceableCell(chunk, position);
        }
        return nearestPlaceableCell;

    }

    private PlaceableCell GetNearestPlaceableCell(List<PlaceableCell> placeableCells, Vector3 position) {

        float shortestDistance = float.PositiveInfinity;
        float currentDistance;
        PlaceableCell nearestPlaceableCell = null;

        for (int i = 0; i < placeableCells.Count; i++) {
            currentDistance = Vector3.Magnitude(placeableCells[i].Position - position);
            if (currentDistance < shortestDistance) {
                shortestDistance = currentDistance;
                nearestPlaceableCell = placeableCells[i];
            }
        }

        return nearestPlaceableCell;

    }

    private PlaceableCell GetNearestPlaceableCell(Chunk chunk, Vector3 position) {

        PlaceableCell nearestCell = null;

        List<PlaceableCell> placeableCells;
        if (placementGrid.TryGetValue(chunk, out placeableCells))
            nearestCell = GetNearestPlaceableCell(placeableCells, position);

        return nearestCell;

    }


    private void ShowPlacementGrid() {

        for (int i = 0; i < visualRepresentation.Count; i++)
            Object.Destroy(visualRepresentation [ i ]);
        visualRepresentation.Clear();

        foreach (KeyValuePair<Chunk, List<PlaceableCell>> x in placementGrid) {
            for (int i = 0; i < x.Value.Count; i++) {
                GameObject centerPoint = GameObject.CreatePrimitive(PrimitiveType.Plane);
                if (x.Value[i].IsFree() == false) {
                    centerPoint.transform.localScale = new Vector3( 0.1f, 0.1f, 0.1f );
                }
                else {
                    centerPoint.transform.localScale = new Vector3( 0.01f, 0.1f, 0.01f );
                }
                
                centerPoint.transform.position = x.Value [ i ].Position;
                centerPoint.transform.SetParent(this.transform);
                visualRepresentation.Add(centerPoint);
            }
        }

    }
    
}
