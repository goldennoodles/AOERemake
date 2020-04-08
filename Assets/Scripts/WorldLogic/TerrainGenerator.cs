using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("TerrainSettings")]
    public int TreeDensity;
    public int StoneDensity;
    public int FoliageDensity; 

    public List<GameObject> TreePrefabs;
    public List<GameObject> StonePrefabs;
    
    // Private Fields
    private ObjectType objectType;
    public List<Transform> retreivedCoordsFromGridMaker = new List<Transform>();
    private Grid grid;
    private GameObject terrainHolder;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        objectType = new ObjectType();
        terrainHolder = new GameObject("TerrainHolder");
        //terrainPrefabs = new List<GameObject>();
    }

    private List<Transform> getTilePositionFromGird () {
        List<Transform> tileCount = new List<Transform>();

        Hashtable girdTable = grid.generatedTile;

        foreach(DictionaryEntry tl in girdTable) {
            Tile tmpTile = (Tile)tl.Value;
            tileCount.Add(tmpTile.createdTile.transform);
        }

        return tileCount;


    }

    public void GenerateTerrainDebugMode(ObjectType type) {


        Debug.Log(objectType);

            if(type == ObjectType.Wood){
                GenerateTrees(findAndRemoveSpawnedLocation(getTilePositionFromGird()));
            } else if (type == ObjectType.Stone){
                GenerateRocks(findAndRemoveSpawnedLocation(getTilePositionFromGird()));
            } else if(type == ObjectType.Empty){
                return;
            }
    }

    private Vector3 findAndRemoveSpawnedLocation (List<Transform> pos) {

        int randomIndex = Random.Range(0, pos.Count);

        Vector3 randomPosition = pos[randomIndex].transform.position;

        pos.RemoveAt(randomIndex);

        return randomPosition;


    }

    //This will be done waaaaaaay in the future to make my life easier.
    private int calculateTerrainDensity {
        get{
            int calucalteDensity = (grid.Gridx * grid.Gridz) / Random.Range(22, 27);
            Debug.Log("DensityCount: " + calucalteDensity); 
            return calucalteDensity;
        }
    }

    private void GenerateTrees (Vector3 spawnPos) {
        int randomSelection = Random.Range(0, TreePrefabs.Count);
        GameObject createdTerrain = Instantiate(TreePrefabs[randomSelection], 
            spawnPos,
            Quaternion.identity)as GameObject;
        createdTerrain.transform.SetParent(terrainHolder.transform);
    }

    private void GenerateRocks (Vector3 spawnPos) {
        int randomSelection = Random.Range(0, StonePrefabs.Count);
        GameObject createdTerrain = Instantiate(StonePrefabs[randomSelection], 
            spawnPos,
            Quaternion.identity)as GameObject;
        createdTerrain.transform.SetParent(terrainHolder.transform);
    }

}
