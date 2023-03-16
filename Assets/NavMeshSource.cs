using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshSource : MonoBehaviour
{
    // Make a build source for a box in local space
    public NavMeshBuildSource BoxSource10x10()
    {
        var src = new NavMeshBuildSource();
        src.transform = transform.localToWorldMatrix;
        src.shape = NavMeshBuildSourceShape.Box;
        src.size = new Vector3(10.0f, 0.1f, 10.0f);
        return src;
    }
}
