using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampManager : MonoBehaviour
{
    public GameObject squarePrefab;
    public GameObject trianglePrefab;
    [SerializeField] Stamp stampPrefab;
    public static StampManager instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("Error: instance is null");
            }
            return _instance;
        }
    }
    static StampManager _instance;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  CreateStamp(Stamp.Shape shape, Vector3 position)
    {
        Stamp stamp = Instantiate(stampPrefab);
        stamp.shape = shape;
        stamp.transform.position = position;
        stamp.Init();
    }

    public void LoadStamp(StampSaveNode node)
    {
        CreateStamp(node.shape, node.position);
    }
}
