using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
  *   @author : Rus Kuzmin
**/
public class TerrainGen : MonoBehaviour
{
    public List<GameObject> treeCollection = new List<GameObject>();
    public List<GameObject> rockCollection = new List<GameObject>();
    public List<GameObject> cloudCollection = new List<GameObject>();

    private int heightScale = 4;
    private float detailScale = 4.2f;

    private World world;
    private Transform terrainHolder;

    private void noiseGenereation(Vector3[] verts, int v)
    {
        int a;
        float noiseSmoothing = 2.2f;

        int perl = PerlinNoise(
            (verts[v].x + this.transform.position.x),
            (verts[v].y + this.transform.position.y),
            (verts[v].z + this.transform.position.z),
            10, 3 ,1.2f);

        perl += PerlinNoise(            
            (verts[v].x + this.transform.position.x),
            (verts[v].y + this.transform.position.y ),
            (verts[v].z + this.transform.position.z),
            20, 4, 0) + 10;

        perl += PerlinNoise(            
            (verts[v].x + this.transform.position.x),
            (verts[v].y + this.transform.position.y),
            (verts[v].z + this.transform.position.z),
            50, 2, 0) + 1;

        float noiseIndex = Noise.GetNoise((verts[v].x + this.transform.position.x),
            (verts[v].y + this.transform.position.y),
            (verts[v].z + this.transform.position.z) * 1);

        a = perl += (int)noiseIndex;

        verts[v].y = (a / noiseSmoothing);

        // verts[v].y = (noiseIndex / noiseSmoothing) * 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        world = FindObjectOfType<World>() as World;
        terrainHolder = FindObjectOfType<TerrainPool>().transform;
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;

        Vector3[] verts = mesh.vertices;

        for (int v = 0; v < verts.Length; v++)
        {
            noiseGenereation(verts, v);

            // The pure random function will not work on limitless or cheated terrain as the pos is not saved.. 
            // Hashtable or Disctionary MAybe? Or another perlin noise?
            if (verts[v].y > .4f && Random.Range(0, 100) <= world.treeDensity)
            {
                GameObject getTerrainTrees = TerrainPool.getTrees();

                if (getTerrainTrees != null && world.treeDensity != 0)
                {
                    Vector3 treePos = new Vector3(verts[v].x + this.transform.position.x,
                                                verts[v].y + this.transform.position.y,
                                                verts[v].z + this.transform.position.z);

                    getTerrainTrees.transform.position = treePos;
                    getTerrainTrees.SetActive(true);
                    getTerrainTrees.transform.SetParent(terrainHolder);

                    treeCollection.Add(getTerrainTrees);
                }
            }

            if (verts[v].y > 0.2f && verts[v].y < 0.3f && Random.Range(0, 100) <= world.rockDensity)
            {
                GameObject getTerrainRocks = TerrainPool.getRocks();

                if (getTerrainRocks != null && world.rockDensity != 0)
                {
                    Vector3 rockPos = new Vector3(verts[v].x + this.transform.position.x,
                                                verts[v].y + this.transform.position.y,
                                                verts[v].z + this.transform.position.z);

                    getTerrainRocks.transform.position = rockPos;
                    getTerrainRocks.SetActive(true);
                    getTerrainRocks.transform.SetParent(terrainHolder);

                    rockCollection.Add(getTerrainRocks);
                }
            }
            if(verts[v].y > .4f && Random.Range(0, 100) <= world.cloudDensity) {
                GameObject getTerrainClouds = TerrainPool.getClouds();
                if (getTerrainClouds != null && world.cloudDensity != 0)
                {
                    int rndIndex = Random.Range(0, verts.Length - 1);
                    Vector3 cloudPos = new Vector3(verts[rndIndex].x + this.transform.position.x,
                                                (verts[v].y + this.transform.position.y) * 3,
                                                verts[rndIndex].z + this.transform.position.z);

                    getTerrainClouds.transform.position = cloudPos;
                    getTerrainClouds.SetActive(true);
                    getTerrainClouds.transform.SetParent(terrainHolder);

                    cloudCollection.Add(getTerrainClouds);
                }

            }
        }

        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        this.gameObject.AddComponent<MeshCollider>();
    }

    int PerlinNoise(float x, float y, float z, float scale, float height, float power)
    {
        float rValue;
        rValue = Noise.GetNoise(((double)x) / scale, ((double)y) / scale, ((double)z) / scale);
        rValue *= height;

        if (power != 0)
        {
            rValue = Mathf.Pow(rValue, power);
        }

        return (int)rValue;
    }

    private void OnDestroy()
    {
        foreach (GameObject tree in treeCollection)
        {
            tree.SetActive(false);
        }
        foreach (GameObject rock in rockCollection)
        {
            rock.SetActive(false);
        }
        foreach (GameObject cloud in cloudCollection) {
            cloud.SetActive(false);
        }
    }
}
