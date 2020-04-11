using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPool : MonoBehaviour
{
    public GameObject[] treePrefabs;
    public GameObject[] rockPrefabs;
    //public GameObject[] Misc;
    private static int treeCount = 1000;
    private static int rockCount = 150;
    private static GameObject[] trees;
    private static GameObject[] rocks;
    void Start()
    {
        trees = new GameObject[treeCount];
        for(int i = 0; i < treeCount; i++) {
            int treeIndex = Random.Range(0, treePrefabs.Length);
            trees[i] = Instantiate(treePrefabs[treeIndex], 
                Vector3.zero, 
                Quaternion.identity) as GameObject;
            trees[i].transform.SetParent(this.transform);
            trees[i].SetActive(false);
        }

        rocks = new GameObject[rockCount];
        for(int i = 0; i < rockCount; i++) {
            int rockIndex = Random.Range(0, rockPrefabs.Length);
            rocks[i] = Instantiate(rockPrefabs[rockIndex], 
                Vector3.zero,
                Quaternion.identity) as GameObject;
            rocks[i].transform.SetParent(this.transform);
            rocks[i].SetActive(false);
        }
    }
    public static GameObject getTrees () {
        for (int i = 0; i < treeCount; i++)
        {
            if(!trees[i].activeSelf) {
                return trees[i];
            }
        }
        return null;
    }
    public static GameObject getRocks () {
        for (int i = 0; i < rockCount; i++)
        {
            if(!rocks[i].activeSelf) {
                return rocks[i];
            }
        }
        return null;
    }
}
