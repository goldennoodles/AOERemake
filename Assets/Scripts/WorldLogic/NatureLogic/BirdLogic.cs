using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdLogic : MonoBehaviour
{

    [Range(0, 20)]
    public float FlySpeed;

    public int MaxWaitTime = 4;

    private Grid grid;

    Vector3 targetPosition;

    // Update is called once per frame
    public void Start()
    {
        grid = FindObjectOfType<Grid>();
        StartCoroutine(BirdBrain());
    }

    private void Update () {
            var targetRotation = Quaternion.LookRotation(targetPosition - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, 4 * Time.deltaTime);
    }

    public IEnumerator BirdBrain()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.value * MaxWaitTime);

            float RandomPositionXRange = Random.Range(0, grid.Gridx);
            float RandomPositionYRange = 0;
            float RandomPositionZRange = Random.Range(0, grid.Gridz);



            float perlinNoise = Mathf.PerlinNoise(3, 5);


            Vector3 localTarget;
            localTarget.x = (Random.value * 2 - 1) * RandomPositionXRange;
            localTarget.y = (Random.value * 2 - 1) * RandomPositionYRange;
            localTarget.z = (Random.value * 2 - 1) * RandomPositionZRange;

            float clampXAxis = Mathf.Clamp(localTarget.x, 0, grid.Gridx * grid.Gridz);
            float clampZAxis = Mathf.Clamp(localTarget.z, 0, grid.Gridz * grid.Gridx);



            targetPosition = transform.position + localTarget;

            targetPosition.x = clampXAxis;
            targetPosition.z = clampZAxis;

            while (transform.position != targetPosition)
            {
                yield return null;
                var velocity = Vector3.zero;
                var smoothedMotion = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, (FlySpeed * Time.deltaTime));
                transform.position = Vector3.MoveTowards(transform.position, smoothedMotion, FlySpeed * Time.deltaTime);
            }
        }
    }

    private Vector3 faceMovementLocation()
    {
        return Vector3.zero;
    }

}
