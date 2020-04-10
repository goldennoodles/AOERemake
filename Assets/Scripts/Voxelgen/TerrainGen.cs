using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    // 3
    int heightScale = 4;
    // 4f
    float detailScale = 4.2f;
    public GameObject[] Trees;
    public GameObject[] Rocks;
    public GameObject[] Misc;
    public List<GameObject> treeCollection = new List<GameObject>();
    public List<GameObject> rockCollection = new List<GameObject>();

    private void noiseGenereation (Vector3[] verts, int v) {
        float noiseSmoothing = 3f; 

            float noiseIndex = Noise.GetNoise((verts[v].x + this.transform.position.x) / noiseSmoothing, 
                (verts[v].y + this.transform.position.y * noiseSmoothing),
                (verts[v].z + this.transform.position.z) / detailScale) * heightScale;

                verts[v].y = (noiseIndex / noiseSmoothing) * 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] verts = mesh.vertices;
        int rndTreeIndex = Random.Range(0, Trees.Length);
        int rndRockIndex = Random.Range(0, Rocks.Length);
        // Later will implement better perlin noise but for now, this will work.
        for(int v = 0; v < verts.Length; v++) {

            noiseGenereation(verts, v);

            /* Old noise generation for reference */

            // verts[v].y = Mathf.PerlinNoise((verts[v].x + this.transform.position.x) / detailScale,
            //                                 (verts[v].z + this.transform.position.z) / detailScale) * heightScale;
        

            // if(verts[v].y > .4f) {
            //     Vector3 treePos = new Vector3(verts[v].x + this.transform.position.x,
            //                                     verts[v].y + this.transform.position.y,
            //                                     verts[v].z + this.transform.position.z);

            //     GameObject genTree = Instantiate(Trees[rndTreeIndex],
            //     treePos, 
            //     Quaternion.identity) as GameObject;

            //     genTree.transform.SetParent(this.transform);

            //     treeCollection.Add(genTree);
            // }

            // if(verts[v].y > 0.2f && verts[v].y < 0.3f) {
            //     Vector3 rockPos = new Vector3(verts[v].x + this.transform.position.x,
            //                                     verts[v].y + this.transform.position.y,
            //                                     verts[v].z + this.transform.position.z);

            //     GameObject genRock = Instantiate(Rocks[rndRockIndex],
            //     rockPos, 
            //     Quaternion.identity) as GameObject;

            //     genRock.transform.SetParent(this.transform);

            //     rockCollection.Add(genRock);                
            // }
        }

        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.AddComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
