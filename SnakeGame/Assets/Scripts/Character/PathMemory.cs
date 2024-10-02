using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMemory : MonoBehaviour
{
    public class PathNode
    {
        public Vector3 position;

        public PathNode(Vector3 position)
        {
            this.position = position;
        }
    }

    public List<PathNode> pathList= new List<PathNode>();

    private void FixedUpdate()
    {
        UpdatePathList();
    }
    public void UpdatePathList()
    {
        pathList.Add(new PathNode(transform.position));
    }

    public void ClearPathList()
    {

        pathList.Clear();
        pathList.Add(new PathNode(transform.position));

    }

}
