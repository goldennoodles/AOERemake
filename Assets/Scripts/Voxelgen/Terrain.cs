using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{

    int heightScale = 3;
    float detailScale = 4f;
    public GameObject tree;

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] verts = mesh.vertices;

        // Later will implement better perlin noise but for now, this will work.
        for(int v = 0; v < verts.Length; v++) {
            verts[v].y = Mathf.PerlinNoise((verts[v].x + this.transform.position.x) / detailScale,
                                            (verts[v].z + this.transform.position.z) / detailScale) * heightScale;
        
            if(verts[v].y > 4f) {
                Vector3 treePos = new Vector3(verts[v].x + this.transform.position.x,
                                                verts[v].y,
                                                verts[v].z + this.transform.position.z);

                GameObject genTree = Instantiate(tree,
                 treePos, 
                 Quaternion.identity) as GameObject;
            }
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
