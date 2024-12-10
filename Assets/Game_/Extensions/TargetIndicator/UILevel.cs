using System.Collections.Generic;
using UnityEngine;

public class UILevel : MonoBehaviour
{
    public Canvas canvas;
    public Camera MainCamera;
    public GameObject TargetIndicatorPrefab;

    List<TargetIndicator> ActiveIndicators = new();


    public void AddTargetIndicator(Character target)
    {
        TargetIndicator indicator = MiniPool.Spawn<TargetIndicator>(PoolType.TargetIndicator, canvas.transform.position, Quaternion.identity);
        if (indicator != null)
        {
            indicator.OnInit(target, MainCamera, canvas);
            ActiveIndicators.Add(indicator);
        }
    }

    public TargetIndicator GetTargetIndicator(Character target)
    {
        for (int i = 0; i < ActiveIndicators.Count; i++)
        {
            if (ActiveIndicators[i].Target.Equals(target))
            {
                return ActiveIndicators[i];
            }
        }
        return null;
    }

    public void RemoveTargetIndicator(Character target)
    {
        TargetIndicator indicator = GetTargetIndicator(target);

        if (indicator != null)
        {
            MiniPool.Despawn(indicator);
            ActiveIndicators.Remove(indicator);
        }
        else
        {
            Debug.LogWarning("can't find targetIndicator");
        }
    }

    public void RemoveAllIndicator()
    {
        foreach (TargetIndicator ind in ActiveIndicators)
        {
            MiniPool.Despawn(ind);
        }
    }
}
