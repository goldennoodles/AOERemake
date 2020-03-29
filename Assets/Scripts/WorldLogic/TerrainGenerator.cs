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



    public void GenerateTerrainDebugMode(Transform coordLocation, ObjectType type) {
        //retrieve All Locations
        if(!retreivedCoordsFromGridMaker.Contains(coordLocation)){
            retreivedCoordsFromGridMaker.Add(coordLocation);
        }
        
        retreivedCoordsFromGridMaker.Remove(null);
        //Loop through Locations And Spawn Prefab.

        Debug.Log(objectType);
        for (int td = 0; td < calculateTerrainDensity; td++)
        {
            if(type == ObjectType.Wood){
                GenerateTrees(findAndRemoveSpawnedLocation(retreivedCoordsFromGridMaker));
            } else if (type == ObjectType.Stone){
                GenerateRocks(findAndRemoveSpawnedLocation(retreivedCoordsFromGridMaker));
            } else if(type == ObjectType.Empty){
                break;
            }
        }
    }

    int randomLocationFromList;
    private Vector3 findAndRemoveSpawnedLocation (List<Transform> pos) {
        randomLocationFromList = Random.Range(0, pos.Count);

        Vector3 newTransform = new Vector3(pos[randomLocationFromList].position.x,
        pos[randomLocationFromList].position.y,
        pos[randomLocationFromList].position.z);

        retreivedCoordsFromGridMaker.Remove(pos[randomLocationFromList]);
        return grid.GetNearestPointOnGrid(newTransform);

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
