using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkComponent : MonoBehaviour
{
    public int heightModifier = 0;
    public List<GameObject> northUsableObjects = new List<GameObject>();
    public List<GameObject> eastUsableObjects = new List<GameObject>();
    public List<GameObject> southUsableObjects = new List<GameObject>();
    public List<GameObject> westUsableObjects = new List<GameObject>();
    public List<GameObject> southwestUsableObjects = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
