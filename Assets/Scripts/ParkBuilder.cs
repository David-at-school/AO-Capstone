using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ParkBuilder : MonoBehaviour
{
    [Range(2, 50)] public int length = 2;
    [Range(1, 50)] public int width = 1;
    [SerializeField] Transform container;
    [SerializeField] List<GameObject> usableObjects = new List<GameObject>();
    int[,] parts;


    void Start()
    {

    }

    void Update()
    {

    }

    public GameObject ChoosePart(int x, int y)
    {
        //if border, make plane
        if (x == 0 || x == width - 1 || y == 0 || y == length - 1)
        {
            parts[x, y] = 0;
        }
        else
        {
            List<GameObject> southParts = usableObjects[parts[x, y - 1]].GetComponent<ParkComponent>().northUsableObjects;
            List<GameObject> eastParts = usableObjects[parts[x - 1, y]].GetComponent<ParkComponent>().westUsableObjects;
            List<GameObject> endParts = usableObjects;
            if (y == length - 2)
            {
                List<GameObject> northParts = usableObjects[parts[x, y - 1]].GetComponent<ParkComponent>().southUsableObjects;
                endParts = endParts.Intersect(northParts).ToList();
            }
            if (x == width - 2)
            {
                List<GameObject> westParts = usableObjects[parts[x, y - 1]].GetComponent<ParkComponent>().eastUsableObjects;
                endParts = endParts.Intersect(westParts).ToList();
            }
            List<GameObject> overlap = southParts.Intersect(eastParts).Intersect(endParts).ToList();

            if (overlap.Count > 0)
            {
                int rng = Random.Range(0, overlap.Count);
                GameObject go = overlap[rng];

                int part = usableObjects.IndexOf(go, 0);
                parts[x, y] = part;
            }
            else
            {
                parts[x, y] = 0;
            }
        }
        return usableObjects[parts[x, y]];
    }

    public void ResetParts()
    {
        parts = new int[width, length];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                parts[i, j] = -1;
            }
        }
    }

    public void BuildPark()
    {
        DestroyChildren();
        ResetParts();

        for (int i = 0; i < width; i++)
        {
            int height = 0;
            for (int j = 0; j < length; j++)
            {

                GameObject go = ChoosePart(i, j);

                int heightMod = go.GetComponent<ParkComponent>().heightModifier;

                if (heightMod < 0)
                {
                    height += heightMod;
                }
                Vector3 partPosition = new Vector3(j * 1, 0, i * 1);
                //Vector3 partPosition = new Vector3(j * 1, heightMod, i * 1);
                if (heightMod > 0)
                {
                    height += heightMod;
                }

                Instantiate(go, partPosition, Quaternion.identity, transform);
            }
        }
    }

    public void DestroyChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
