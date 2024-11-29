using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] LineRenderer lineRenderer;


    private void LateUpdate()
    {
        DrawCirle(100, player.AttackRange);
    }

    void DrawCirle(int steps, float radius)
    {
        lineRenderer.positionCount = steps;

        for (int i = 0; i < steps; i++)
        {
            float progress = (float) i / (steps - 1);

            float currentRadian = progress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = player.transform.position + new Vector3(x, .1f, y);

            lineRenderer.SetPosition(i, currentPosition);
        }
    }
}
