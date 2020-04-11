using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    public List<GameObject> treeCollection = new List<GameObject>();
    public List<GameObject> rockCollection = new List<GameObject>();
    private int heightScale = 4;
    private float detailScale = 4.2f; 

    private void noiseGenereation(Vector3[] verts, int v)
    {
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

        for (int v = 0; v < verts.Length; v++)
        {
            noiseGenereation(verts, v);

            // The pure random function will not work on limitless or cheated terrain as the pos is not saved.. 
            // Hashtable or Disctionary MAybe? Or another perlin noise?
            if (verts[v].y > .4f && Random.Range(0, 100) < 10)
            {
                GameObject getTerrainTrees = TerrainPool.getTrees();
                
                if (getTerrainTrees != null)
                {
                    Vector3 treePos = new Vector3(verts[v].x + this.transform.position.x,
                                                verts[v].y + this.transform.position.y,
                                                verts[v].z + this.transform.position.z);

                    getTerrainTrees.transform.position = treePos;
                    getTerrainTrees.SetActive(true);
                    getTerrainTrees.transform.SetParent(this.transform);

                    treeCollection.Add(getTerrainTrees);
                }
            }

            if (verts[v].y > 0.2f && verts[v].y < 0.3f && Random.Range(0, 100) < 10)
            {
                GameObject getTerrainRocks = TerrainPool.getRocks();

                if (getTerrainRocks != null)
                {
                    Vector3 rockPos = new Vector3(verts[v].x + this.transform.position.x,
                                                verts[v].y + this.transform.position.y,
                                                verts[v].z + this.transform.position.z);

                    getTerrainRocks.transform.position = rockPos;
                    getTerrainRocks.SetActive(true);
                    getTerrainRocks.transform.SetParent(this.transform);

                    rockCollection.Add(getTerrainRocks);
                }
            }
        }

        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        this.gameObject.AddComponent<MeshCollider>();
    }

    private void OnDestroy()
    {
        foreach(GameObject tree in treeCollection) {
            tree.SetActive(false);
        }
        foreach(GameObject rock in rockCollection){
            rock.SetActive(false);
        }
    }
}
