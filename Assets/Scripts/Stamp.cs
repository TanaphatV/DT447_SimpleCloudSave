using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    public Shape shape;
    // Start is called before the first frame update
    public void Init()
    {
        if (shape == Shape.Square)
            Instantiate(StampManager.instance.squarePrefab, transform);
        else if (shape == Shape.Triangle)
            Instantiate(StampManager.instance.trianglePrefab, transform);

    }

    public enum Shape
    {
        Square,
        Triangle
    }

    public StampSaveNode GetSaveNode()
    {
        return new StampSaveNode(shape, transform.position);
    }
}
[System.Serializable]
public class StampSaveNode
{
    public Stamp.Shape shape;
    public Vector3 position;
    public StampSaveNode(Stamp.Shape shape,Vector3 position)
    {
        this.shape = shape;
        this.position = position;
    }
}
