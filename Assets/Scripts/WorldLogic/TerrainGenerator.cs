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
    private List<Transform> retreivedCoordsFromGridMaker = new List<Transform>();
    private Grid grid;
    private GameObject terrainHolder;
    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        objectType = new ObjectType();
        terrainHolder = new GameObject("TerrainHolder");
        //terrainPrefabs = new List<GameObject>();
    }

    public void GenerateTerrainDebugMode(List<Transform> coordLocation) {
        //retrieve All Locations
        retreivedCoordsFromGridMaker = coordLocation;
        //Loop through Locations And Spawn Prefab.

        for (int i = 0; i < calculateTerrainDensity(); i++)
        {
             GenerateTrees(findAndRemoveSpawnedLocation(retreivedCoordsFromGridMaker));
             GenerateRocks(findAndRemoveSpawnedLocation(retreivedCoordsFromGridMaker));
        }
    }

    //Find an random GeneratedSpace, and puts a prefab.
    public IEnumerator GenerateTerrain (List<Transform> coordLocation) {
        //retrieve All Locations
        retreivedCoordsFromGridMaker = coordLocation;
        //Loop through Locations And Spawn Prefab.

        for (int i = 0; i < calculateTerrainDensity(); i++)
        {
            yield return new WaitForSeconds(0.2f);
             GenerateTrees(findAndRemoveSpawnedLocation(retreivedCoordsFromGridMaker));
             GenerateRocks(findAndRemoveSpawnedLocation(retreivedCoordsFromGridMaker));
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
    private int calculateTerrainDensity() {

        int calucalteDensity = (grid.Gridx * grid.Gridz) / Random.Range(22, 27);
        Debug.Log("DensityCount: " + calucalteDensity); 
        return calucalteDensity;
    }

    private void GenerateTrees (Vector3 spawnPos) {
        int randomSelection = Random.Range(0, TreePrefabs.Count);
        GameObject createdTerrain = Instantiate(TreePrefabs[randomSelection], 
            spawnPos,
            Quaternion.identity)as GameObject;
        createdTerrain.transform.SetParent(terrainHolder.transform);
        ObjectType woodObjectType = ObjectType.Wood;
    }

    private void GenerateRocks (Vector3 spawnPos) {
        int randomSelection = Random.Range(0, StonePrefabs.Count);
        GameObject createdTerrain = Instantiate(StonePrefabs[randomSelection], 
            spawnPos,
            Quaternion.identity)as GameObject;
        createdTerrain.transform.SetParent(terrainHolder.transform);
        ObjectType woodObjectType = ObjectType.Stone;
    }
}
