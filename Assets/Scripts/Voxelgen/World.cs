using UnityEngine;
using System.Collections;

/**
  *   Will implement the tileHolder(chunk) holderLater.
  *   @author : Rus Kuzmin
**/
class Tile
{
    public GameObject createdTile;
    public float creationTime;

    public Tile(GameObject t, float gt)
    {
        createdTile = t;
        creationTime = gt;
    }
}
public class World : MonoBehaviour
{
    [Header("World Settings")]
    public GameObject chunk;
    public Chunk[,,] chunks;
    public int chunkSize = 32;
    public byte[,,] data;
    public int worldX = 16;
    public int worldY = 16;
    public int worldZ = 16;
    [Header("Terrain Settings")]
    [Range(0, 20)]
    public int treeDensity = 10;    //Default = 10;
    [Range(0, 20)]
    public int rockDensity = 10;    //Default = 10;
    public GameObject playerTransform;
    private Vector3 startPos;
    //private Hashtable generatedTile = new Hashtable();
    private float updatedTime;

    // Use this for initialization
    private void Start()
    {
        startPos = Vector3.zero;
        updatedTime = Time.realtimeSinceStartup;

        data = new byte[worldX, worldY, worldZ];


        for (int x = 0; x < worldX; x++)
        {
            for (int z = 0; z < worldZ; z++)
            {

                for (int y = 0; y < worldY; y++)
                {
                    if (y <= 8)
                    {
                        data[x, y, z] = 1;
                    }
                }
            }
        }

        chunks = new Chunk[Mathf.FloorToInt(worldX / chunkSize),

        Mathf.FloorToInt(worldY / chunkSize), Mathf.FloorToInt(worldZ / chunkSize)];

        playerTransform.transform.position = spawnPlayerInCenter;
    }
    void Update()
    {
        LoadChunks(playerTransform.transform.position, 32, 40);
    }

    private Vector3 spawnPlayerInCenter {
        get {
            int worldCentre = (worldX + worldZ) / chunkSize;
            Vector3 centerPos = new Vector3(
                worldCentre, 2.3f, worldCentre
            );
            return centerPos;
        }
    }

    private void GenColumn(int x, int z)
    {
        for (int y = 0; y < chunks.GetLength(1); y++)
        {
            GameObject newChunk = Instantiate(chunk, new Vector3(x * chunkSize - 0.5f,
                0, z * chunkSize - 0.5f), new Quaternion(0, 0, 0, 0)) as GameObject;

            newChunk.transform.SetParent(this.transform);

            //Tile t = new Tile(newChunk, updatedTime);

            chunks[x, y, z] = newChunk.GetComponent<Chunk>() as Chunk;
            chunks[x, y, z].worldGO = gameObject;
            chunks[x, y, z].chunkX = x * chunkSize;
            chunks[x, y, z].chunkY = y * chunkSize;
            chunks[x, y, z].chunkZ = z * chunkSize;
        }
    }

    private void UnloadColumn(int x, int z)
    {
        for (int y = 0; y < chunks.GetLength(1); y++)
        {
            GameObject.Destroy(chunks[x, y, z].gameObject);
        }
    }

    public void LoadChunks(Vector3 playerPos, float distToLoad, float distToUnload)
    {
        for (int x = 0; x < chunks.GetLength(0); x++)
        {
            for (int z = 0; z < chunks.GetLength(2); z++)
            {
                float dist = Vector2.Distance(new Vector2(x * chunkSize,
                z * chunkSize), new Vector2(playerPos.x, playerPos.z));

                if (dist < distToLoad)
                {
                    if (chunks[x, 0, z] == null)
                    {
                        GenColumn(x, z);
                    }
                }
                else if (dist > distToUnload)
                {
                    if (chunks[x, 0, z] != null)
                    {
                        UnloadColumn(x, z);
                    }
                }


            }
        }
    }

    public byte Block(int x, int y, int z)
    {
        if (x >= worldX || x < 0 || y >= worldY || y < 0 || z >= worldZ || z < 0)
        {
            return (byte)1;
        }
        return data[x, y, z];
    }
}