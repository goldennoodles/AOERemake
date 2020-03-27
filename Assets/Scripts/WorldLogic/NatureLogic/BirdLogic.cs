using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdLogic : MonoBehaviour
{

    [Range(0, 20)]
    public float FlySpeed;

    public int MaxWaitTime = 4;

    private Grid grid;

    // Update is called once per frame
    public void Start()
    {
        grid = FindObjectOfType<Grid>();
        StartCoroutine(BirdBrain());
    }
    public IEnumerator BirdBrain()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.value * MaxWaitTime);

            float RandomPositionXRange = Random.Range(0, grid.Gridx);
            float RandomPositionYRange = 0;
            float RandomPositionZRange = Random.Range(0, grid.Gridz);



            Vector3 localTarget;
            localTarget.x = (Random.value * 2 - 1) * RandomPositionXRange;
            localTarget.y = (Random.value * 2 - 1) * RandomPositionYRange;
            localTarget.z = (Random.value * 2 - 1) * RandomPositionZRange;

            float clampXAxis = Mathf.Clamp(localTarget.x, 0, grid.Gridx * grid.Gridz);
            float clampZAxis = Mathf.Clamp(localTarget.z, 0, grid.Gridz * grid.Gridx);

            Vector3 targetPosition = transform.position + localTarget;

            targetPosition.x = clampXAxis;
            targetPosition.z = clampZAxis;

            Quaternion currentRotation = this.transform.rotation;

            while (transform.position != targetPosition)
            {
                yield return null;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, FlySpeed * Time.deltaTime);
            }
        }
    }
    private Vector3 faceMovementLocation () {
        return Vector3.zero;
    }

}
