using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] NavMeshSurface surface;

    private void Awake()
    {
        surface.BuildNavMesh();
    }

    public void OnInit()
    {
        surface.BuildNavMesh();
    }
}
