using System.Collections.Generic;
using UnityEngine;

public class UILevel : MonoBehaviour
{
    public Canvas canvas;
    public Camera MainCamera;
    public GameObject TargetIndicatorPrefab;

    Pool targetIndicatorPool;

    public void SetPool()
    {
        targetIndicatorPool = MiniPool.GetPool<Pool>(PoolType.TargetIndicator);
    }

    public List<TargetIndicator> ActiveIndicators()
    {
        List<TargetIndicator> newList = new List<TargetIndicator>();
        for (int i = 0; i < targetIndicatorPool.Active.Count; i++)
        {
            newList.Add((TargetIndicator)targetIndicatorPool.Active[i]);
        }

        return newList;
    }

    public void AddTargetIndicator(Character target)
    {
        TargetIndicator indicator = MiniPool.Spawn<TargetIndicator>(PoolType.TargetIndicator, canvas.transform.position, Quaternion.identity);
        if (indicator != null)
        {
            indicator.OnInit(target, MainCamera, canvas);
        }
    }

    public TargetIndicator GetTargetIndicator(GameObject target)
    {
        TargetIndicator ind = null;
        foreach (GameUnit unit in targetIndicatorPool.Active)
        {
            TargetIndicator indicator = (TargetIndicator)unit;
            if (indicator != null)
            {
                if (indicator.Target == target)
                {
                    ind = indicator;
                    break;
                }
            }
        }
        return ind;
    }

    public void RemoveTargetIndicator(GameObject target)
    {
        foreach (GameUnit unit in targetIndicatorPool.Active)
        {
            TargetIndicator indicator = (TargetIndicator)unit;
            if (indicator != null)
            {
                if (indicator.Target == target)
                {
                    indicator.OnDespawn();
                    MiniPool.Despawn(indicator);
                    break;
                }
            }
        }
    }

    public void RemoveAllIndicator()
    {
        foreach (GameUnit unit in targetIndicatorPool.Active)
        {
            MiniPool.Despawn(unit);
        }
    }


    #region IndicatorManager

    public int maxOutOfSightActive = 5;

    private List<TargetIndicator> activeOutOfSightIndicators = new();

    public void UpdateAllIndicators()
    {
        List<TargetIndicator> indicators = ActiveIndicators();

        int outOfSightCount = 0;

        foreach (TargetIndicator indicator in indicators)
        {
            if (!indicator.outOfSight)
            {
                indicator.TurnOnIndicator();
                if (activeOutOfSightIndicators.Contains(indicator))
                {
                    activeOutOfSightIndicators.Remove(indicator);
                }
            }
            else
            {
                if (outOfSightCount < maxOutOfSightActive)
                {
                    indicator.TurnOnIndicator();
                    outOfSightCount++;

                    if (!activeOutOfSightIndicators.Contains(indicator))
                    {
                        activeOutOfSightIndicators.Add(indicator);
                    }
                }
                else
                {
                    indicator.TurnOffIndicator();
                }
            }
        }

        // Ensure the limit is maintained after updating all indicators
        ManageOutOfSightLimit();
    }

    // This method can be called during playtime when an indicator's state changes (e.g., triggered by event)
    public void OnIndicatorStateChanged(TargetIndicator indicator)
    {
        if (!indicator.outOfSight)
        {
            indicator.TurnOnIndicator();
            if (activeOutOfSightIndicators.Contains(indicator))
            {
                activeOutOfSightIndicators.Remove(indicator);
            }
        }
        else
        {
            if (activeOutOfSightIndicators.Contains(indicator))
            {
                indicator.TurnOnIndicator();
            }
            else
            {
                if (activeOutOfSightIndicators.Count < maxOutOfSightActive)
                {
                    indicator.TurnOnIndicator();
                    activeOutOfSightIndicators.Add(indicator);
                }
                else
                {
                    indicator.TurnOffIndicator();
                }
            }
        }

        ManageOutOfSightLimit();
    }

    private void ManageOutOfSightLimit()
    {
        if (activeOutOfSightIndicators.Count > maxOutOfSightActive)
        {
            int excess = activeOutOfSightIndicators.Count - maxOutOfSightActive;
            for (int i = 0; i < excess; i++)
            {
                TargetIndicator indicator = activeOutOfSightIndicators[i];
                indicator.TurnOffIndicator();
                activeOutOfSightIndicators.RemoveAt(i);
            }
        }
    }

    #endregion
}
