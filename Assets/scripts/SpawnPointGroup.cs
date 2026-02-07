using UnityEngine;

public class SpawnPointGroup : MonoBehaviour
{
    public Transform[] points;

    private int index = 0;

    public Transform GetNext()
    {
        if (points.Length == 0)
            return null;

        Transform t = points[index];
        index = (index + 1) % points.Length;
        return t;
    }
}
