using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int Gridx, Gridz;

    public Material GridTileMaterial;

    public float GridSpacing = 1f;

    [SerializeField]
    private float size = 1f;

    public List<Transform> gridCoords = new List<Transform>();

    private TerrainGenerator terrainGenerator;

    public Camera mainCamera;

    public GameObject tile;

    private int tileId;

    
    IEnumerator GenerateWorldButSuperCool() {


        for (int x = 0; x < Gridx; x++)
        {
            for (int z = 0; z < Gridz; z++)
            {
                tile.transform.name = "GroundTile: " + tileId;

                yield return new WaitForSeconds(0.005f);

                Vector3 gridPos = new Vector3(x * GridSpacing, -0.3f, z * GridSpacing);

                GameObject gridTile = Instantiate(tile, gridPos += AddNoiseOnAngle(0,30), Quaternion.identity) as GameObject;
                gridTile.GetComponent<MeshRenderer>().material = GridTileMaterial;
                gridTile.transform.SetParent(this.transform);
                tileId++;
                gridCoords.Add(gridTile.transform);

            }
        }

        terrainGenerator.GenerateTerrain(gridCoords);
    }

    private void Start()
    {

        terrainGenerator = FindObjectOfType<TerrainGenerator>();
        mainCamera = FindObjectOfType<Camera>();

        Vector3 mainCameraPos = mainCamera.transform.position;
        mainCameraPos = GetCenterPointOnGrid();
        mainCameraPos.x = mainCameraPos.x + -30f;
        mainCameraPos.z = mainCameraPos.z + -30f;
        mainCameraPos.y = 40f;
        mainCamera.transform.position = mainCameraPos;

        StartCoroutine(GenerateWorldButSuperCool());

        // GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // for (int x = 0; x < Gridx; x++)
        // {
        //     for (int z = 0; z < Gridz; z++)
        //     {

        //         Vector3 gridPos = new Vector3(x * GridSpacing, -0.3f, z * GridSpacing);

        //         GameObject gridTile = Instantiate(tile, gridPos += AddNoiseOnAngle(0,30), Quaternion.identity) as GameObject;
        //         gridTile.GetComponent<MeshRenderer>().material = GridTileMaterial;
        //         gridTile.transform.SetParent(this.transform);

        //         gridCoords.Add(gridTile.transform);

        //     }
        // }

        // terrainGenerator.GenerateTerrain(gridCoords);

    }


    Vector3 AddNoiseOnAngle(float min, float max)
    {
        // Find random angle between min & max inclusive
        float xNoise = Random.Range(min, max);
        float yNoise = Random.Range(min, max);
        float zNoise = Random.Range(min, max);

        // Convert Angle to Vector3
        Vector3 noise = new Vector3(
        // Will comment out until I can figure out how to clean this up.
        //   Mathf.Sin(2 * Mathf.PI * xNoise / 360),
        //   Mathf.Sin(2 * Mathf.PI * yNoise / 360),
        //   Mathf.Sin(2 * Mathf.PI * zNoise / 360)

        0,
        Mathf.Sin(2 * Mathf.PI * yNoise / 360),
        0
        );
        return noise;
    }


    public Vector3 GetCenterPointOnGrid()
    {

        int center = Gridx / Gridz;

        Vector3 result = new Vector3(
            (float)center * Gridx,
            (float)1,
            (float)center * Gridz);

        return result / 2;

    }


    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }
}