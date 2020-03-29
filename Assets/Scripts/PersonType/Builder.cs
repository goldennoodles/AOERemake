using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    Grid grid;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        StartCoroutine(BuilderBrain());
    }

    private IEnumerator BuilderBrain()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.value * 1f);

            float RandomPositionXRange = Random.Range(0, grid.Gridx);
            float RandomPositionYRange = 0;
            float RandomPositionZRange = Random.Range(0, grid.Gridz);

            Vector3 localTarget;
            localTarget.x = (Random.value * 2 - 1) * RandomPositionXRange;
            localTarget.y = (Random.value * 2 - 1) * RandomPositionYRange;
            localTarget.z = (Random.value * 2 - 1) * RandomPositionZRange;

            Vector3 targetPosition = transform.position + localTarget;

            while (transform.position != targetPosition)
            {
                yield return null;

                var velocity = Vector3.zero;
                var smoothedMotion = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, (4 * Time.deltaTime));
                transform.position = Vector3.MoveTowards(transform.position, smoothedMotion, 4 * Time.deltaTime);
            }
        }

    }
}
