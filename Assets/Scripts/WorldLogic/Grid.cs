using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Tile
{
    public GameObject createdTile;
    public float creationTime;

    public ObjectType objectType;

    public bool isTileEmpty;

    public Tile(GameObject t, float gt, ObjectType objt)
    {
        createdTile = t;
        creationTime = gt;
        objectType = objt;
    }
}
public class Grid : MonoBehaviour
{
    public int Gridx, Gridz;
    public Material GridTileMaterial;
    public Camera mainCamera;
    public GameObject tilePrefab;
    public GameObject playerTransform;

    [SerializeField]
    private int tileSize = 1;
    private TerrainGenerator terrainGenerator;
    private Vector3 startPos;
    public Hashtable generatedTile = new Hashtable();

    private void Start()
    {
        terrainGenerator = FindObjectOfType<TerrainGenerator>();
        mainCamera = FindObjectOfType<Camera>();

        Vector3 mainCameraPos = mainCamera.transform.position;
        //mainCameraPos = GetCenterPointOnGrid();
        mainCameraPos.x = playerTransform.transform.position.x + -30f;
        mainCameraPos.z = playerTransform.transform.position.z + -30f;
        mainCameraPos.y = 40f;
        mainCamera.transform.position = mainCameraPos;

        startPos = Vector3.zero;
        float updatedTime = Time.realtimeSinceStartup;

        GenerateWorld(updatedTime);
    }


    private void Update() {
        StartCoroutine(generateLimitlessTerrain());
    }

    private void GenerateWorld(float ct)
    {
        for (int x = -Gridx; x < Gridx; x++)
        {
            for (int z = -Gridz; z < Gridz; z++)
            {

                Vector3 pos = new Vector3((x * tileSize+startPos.x),
                AddNoiseOnAngle(0,22).y,
                (z * tileSize+startPos.z));

                string tileName = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)pos.z).ToString();
                GameObject t = (GameObject)Instantiate(tilePrefab, pos, Quaternion.identity);
                t.transform.SetParent(this.transform);
                t.name = tileName;
                t.GetComponent<MeshRenderer>().material = GridTileMaterial;

                Tile tile = new Tile(t, ct, tileType);
                Debug.Log(tileType);
                generatedTile.Add(tileName, tile);


            }
        }
        
        terrainGenerator.GenerateTerrainDebugMode(tileType);

    }

    private IEnumerator generateLimitlessTerrain() {

        int xMove = (int)(playerTransform.transform.position.x - startPos.x);
        int zMove = (int)(playerTransform.transform.position.z - startPos.z);

        float upTime = Time.realtimeSinceStartup;

        if(Mathf.Abs(xMove) >= tileSize || Mathf.Abs(zMove) >= tileSize ){
            int playerX = (int)(Mathf.Floor(playerTransform.transform.position.x/tileSize) * tileSize);
            int playerZ = (int)(Mathf.Floor(playerTransform.transform.position.z/tileSize) * tileSize);

            for (int x = -Gridx; x < Gridx; x++)
            {
                for (int z = -Gridz; z < Gridz; z++)
                {
                    Vector3 pos = new Vector3((x * tileSize + playerX),
                    AddNoiseOnAngle(0,22).y,
                    (z * tileSize + playerZ));

                    string tileName = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)pos.z).ToString();

                    if(!generatedTile.ContainsKey(tileName)) {

                        GameObject t = (GameObject)Instantiate(tilePrefab, pos, Quaternion.identity);
                        t.name = tileName;
                        t.transform.SetParent(this.transform);
                        t.GetComponent<MeshRenderer>().material = GridTileMaterial;


                        Tile tile = new Tile(t, upTime, tileType);
                        Debug.Log(tileType);
                        generatedTile.Add(tileName, tile);

                    } else {
                        (generatedTile[tileName] as Tile).creationTime = upTime;
                    }
                }
            }

            Hashtable newTerrain = new Hashtable();

            foreach(Tile tls in generatedTile.Values){
                if(tls.creationTime != upTime){
                    generatedTile.Remove(tls.createdTile);
                    GameObject.Destroy(tls.createdTile);
                } else {
                    newTerrain.Add(tls.createdTile.name, tls);
                    }
                }
                yield return new WaitForSeconds(0.0f);

                generatedTile = newTerrain;
                terrainGenerator.GenerateTerrainDebugMode(tileType);
                startPos = playerTransform.transform.position;
            }
        }


    private ObjectType tileType {
        get{
           return (ObjectType)Random.Range(0, 3);
        }
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

        int xCount = Mathf.RoundToInt(position.x / 1);
        int yCount = Mathf.RoundToInt(position.y / 1);
        int zCount = Mathf.RoundToInt(position.z / 1);

        Vector3 result = new Vector3(
            (float)xCount * 1,
            (float)yCount * 1,
            (float)zCount * 1);

        result += transform.position;

        return result;
    }

    public int SimpleGridTotalCalculation
    {
        get
        {
            return Gridx + Gridz;
        }
    }
}