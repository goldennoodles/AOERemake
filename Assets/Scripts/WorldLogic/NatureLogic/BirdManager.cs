using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{

    public GameObject bridFlock;

    private int flockCount;
    private Grid grid;
    // Start is called before the first frame update
    private void Awake() {
        grid = FindObjectOfType<Grid>();
        
    }

    private void Start () {
        for(int fc = 0; fc < CaluclateFlockCountBasedOnMapSize(); fc ++) {
            GameObject createdFlocks = Instantiate(bridFlock,
            randomLocationBasedOnCentre(), 
            Quaternion.identity) as GameObject;

            createdFlocks.AddComponent<BirdLogic>();
        }
    }

    private Vector3 randomLocationBasedOnCentre() {
        Vector3 centrePos = grid.GetCenterPointOnGrid();

        return new Vector3(
            centrePos.x = centrePos.x + Random.Range(0, 20),
            centrePos.y = 20f,
            centrePos.z = centrePos.z + Random.Range(0,30)
        );
    }

    private int CaluclateFlockCountBasedOnMapSize () {
        
        int calculateMapSize = grid.Gridx * grid.Gridz;

        if(calculateMapSize >= 10 && calculateMapSize <= 19){
            flockCount = 1;
        } else if (calculateMapSize >= 20 && calculateMapSize <= 29){
            flockCount = 2;
        } else if (calculateMapSize >= 30) {
            flockCount = 4;
        }

        return flockCount;
    }
}
