using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement :MonoBehaviour {
    //in progress...

    //1. Get the Grid [OK]
    //2. Relate Grid to Chunk [OK]
    //3. Onclick -> find Grid index and chunk related [OK]
    //4. Grid index should point to a Building [OK]

    //Future Problems:
    //How to handle changes in the environment?
    //How to handle different levels in Y axis?

    #region Variables

    private Dictionary<Chunk, List<PlaceableCell>> _placementGrid = new Dictionary<Chunk, List<PlaceableCell>>();
    private List<GameObject> _visualRepresentation = new List<GameObject>();
    [SerializeField] private List<Vector3> _hitPoints = new List<Vector3>();

    [SerializeField]
    private Placeable _selectedPlaceableObject;
    private GameObject _selectedPleacableGameObject;

    #endregion

    #region Getters and Setters

    #endregion

    #region Main Functionalities

    private void Update () {

        if (Input.GetKeyDown( KeyCode.T )) {
            ShowPlacementGrid();
        }

        if (_selectedPlaceableObject != null) {

            Vector3 mousePosition3D = Utils.GetMousePosition3D();
            if (mousePosition3D != Vector3.positiveInfinity) {
                MoveSelectedPlaceableObject( mousePosition3D );
                if (Input.GetMouseButtonDown( 0 )) {
                    _hitPoints.Add( mousePosition3D );
                    PlaceSelectedPlaceableObject( mousePosition3D );
                }
            }

        }

    }

    public void SetSelectedPlaceableObject (Placeable placeableSelected) {
        _selectedPleacableGameObject = Instantiate( placeableSelected.gameObject );
        _selectedPlaceableObject = _selectedPleacableGameObject.GetComponent<Placeable>();
    }

    private void MoveSelectedPlaceableObject (Vector3 mousePoint) {

        PlaceableCell cellToPlace = GetNearestPlaceableCell( mousePoint );
        if (cellToPlace == null) return;
        if (cellToPlace.IsFree()) {
            _selectedPleacableGameObject.transform.position = cellToPlace.Position;
        }

    }

    private void PlaceSelectedPlaceableObject (Vector3 clickPoint) {

        PlaceableCell cellToPlace = GetNearestPlaceableCell( clickPoint );

        if (cellToPlace.IsFree()) {

            cellToPlace.AddPlaceable( _selectedPlaceableObject );
            _selectedPlaceableObject = null;
            _selectedPleacableGameObject.transform.position = cellToPlace.Position;
            //_selectedPleacableGameObject.transform.SetParent( cellToPlace to place chunk transform );

        }

    }

    public void AddToPlacementGrid (Mesh mesh, Chunk chunk, Transform chunkTransform) {

        if (_placementGrid.ContainsKey( chunk )) return;

        List<Vector3> centerPoints = new List<Vector3>();
        Vector3 firstPoint = new Vector3();
        Vector3 lastPoint = new Vector3();
        List<PlaceableCell> placeableCells = new List<PlaceableCell>();

        //For every square (4 vertices) store the center point
        int length = mesh.vertices.Length;
        for (int i = 0; i < length; i += 4) {
            firstPoint = mesh.vertices [ i ];//1
            lastPoint = mesh.vertices [ i + 2 ];//3
            Vector3 centerPoint = ( ( chunkTransform.transform.TransformPoint( firstPoint ) + chunkTransform.transform.TransformPoint( lastPoint ) ) / 2 );
            centerPoints.Add( centerPoint );
            PlaceableCell cell = new PlaceableCell( centerPoint, null, chunk );
            placeableCells.Add( cell );
        }

        _placementGrid.Add( chunk, placeableCells );

    }
    #endregion

    #region Auxiliar Functionalities

    private void Start () {
        Setup();
    }

    private void Setup () {
        _selectedPlaceableObject = null;
    }

    private PlaceableCell GetNearestPlaceableCell (Vector3 position, Chunk chunk = null) {

        PlaceableCell nearestPlaceableCell = null;
        float shortestDistance = float.PositiveInfinity;
        float currentDistance;

        //Search in all chunks of the list
        if (chunk == null) {

            PlaceableCell currentNearestPlaceableCell = null;
            foreach (KeyValuePair<Chunk, List<PlaceableCell>> currentChunk in _placementGrid) {
                currentNearestPlaceableCell = GetNearestPlaceableCell( currentChunk.Value, position );

                //Debug.Log( currentNearestPlaceableCell );

                currentDistance = Vector3.Magnitude( currentNearestPlaceableCell.Position - position );
                if (currentDistance < shortestDistance) {
                    shortestDistance = currentDistance;
                    nearestPlaceableCell = currentNearestPlaceableCell;
                }
            }

        } else { //Search directly in the chunk
            nearestPlaceableCell = GetNearestPlaceableCell( chunk, position );
        }
        return nearestPlaceableCell;

    }

    private PlaceableCell GetNearestPlaceableCell (List<PlaceableCell> placeableCells, Vector3 position) {

        float shortestDistance = float.PositiveInfinity;
        float currentDistance;
        PlaceableCell nearestPlaceableCell = null;

        Debug.Log( placeableCells );

        int length = placeableCells.Count;
        for (int i = 0; i < length; i++) {
            currentDistance = Vector3.Magnitude( placeableCells [ i ].Position - position );
            if (currentDistance == float.PositiveInfinity && nearestPlaceableCell == null) {
                nearestPlaceableCell = placeableCells [ i ];
            }
            if (currentDistance < shortestDistance) {
                shortestDistance = currentDistance;
                nearestPlaceableCell = placeableCells [ i ];
            }
        }

        return nearestPlaceableCell;

    }

    private PlaceableCell GetNearestPlaceableCell (Chunk chunk, Vector3 position) {

        PlaceableCell nearestCell = null;

        List<PlaceableCell> placeableCells;
        if (_placementGrid.TryGetValue( chunk, out placeableCells ))
            nearestCell = GetNearestPlaceableCell( placeableCells, position );

        return nearestCell;

    }


    private void ShowPlacementGrid () {
        int length = _visualRepresentation.Count;
        for (int i = 0; i < length; i++)
            Object.Destroy( _visualRepresentation [ i ] );
        _visualRepresentation.Clear();

        foreach (KeyValuePair<Chunk, List<PlaceableCell>> x in _placementGrid) {
            length = x.Value.Count;
            for (int i = 0; i < length; i++) {
                GameObject centerPoint = GameObject.CreatePrimitive( PrimitiveType.Plane );
                if (x.Value [ i ].IsFree() == false) {
                    centerPoint.transform.localScale = new Vector3( 0.1f, 0.1f, 0.1f );
                } else {
                    centerPoint.transform.localScale = new Vector3( 0.01f, 0.1f, 0.01f );
                }

                centerPoint.transform.position = x.Value [ i ].Position;
                centerPoint.transform.SetParent( this.transform );
                _visualRepresentation.Add( centerPoint );
            }
        }

    }
    #endregion

}
