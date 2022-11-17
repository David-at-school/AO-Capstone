using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ParkBuilder : MonoBehaviour
{
    [Range(2, 500)] public int length = 2;
    [Range(1, 500)] public int width = 1;
    [SerializeField] float scale = 1;
    [SerializeField] bool useHeight = false;
    [SerializeField] Transform container;
    [SerializeField] List<GameObject> usableObjects = new List<GameObject>();
    int[,] parts;

    int currentX = 0;
    int currentY = 0;
    int currentHeight = 0;

    [SerializeField] List<GameObject> possibleObjects = new List<GameObject>();

    void Start()
    {
        //DestroyChildren();
        //ResetParts();
        
    }

    void Update()
    {

    }

    private void OnValidate()
    {
        transform.localScale = Vector3.one * scale;
    }

    private List<GameObject> GetPossibleParts(int y, int x)
    {
        List<GameObject> southParts = usableObjects[parts[y, x - 1]].GetComponent<ParkComponent>().northUsableObjects;
        List<GameObject> eastParts = usableObjects[parts[y - 1, x]].GetComponent<ParkComponent>().westUsableObjects;
        List<GameObject> northeastParts = usableObjects[parts[y - 1, x + 1]].GetComponent<ParkComponent>().southwestUsableObjects;
        List<GameObject> endParts = usableObjects;
        if (x == length - 2)
        {
            List<GameObject> northParts = usableObjects[0].GetComponent<ParkComponent>().southUsableObjects;
            endParts = endParts.Intersect(northParts).ToList();
        }
        if (y == width - 2)
        {
            List<GameObject> westParts = usableObjects[0].GetComponent<ParkComponent>().eastUsableObjects;
            endParts = endParts.Intersect(westParts).ToList();
        }
        List<GameObject> overlap = southParts.Intersect(eastParts).Intersect(northeastParts).Intersect(endParts).ToList();
        possibleObjects = overlap;
        return overlap;
    }

    public GameObject ChoosePart(int y, int x)
    {
        //if border, make plane
        if (y == 0 || y == width - 1 || x == 0 || x == length - 1)
        {
            parts[y, x] = 0;
        }
        else
        {
            List<GameObject> overlap = GetPossibleParts(y, x);

            if (overlap.Count > 0)
            {
                int rng = Random.Range(0, overlap.Count);
                GameObject go = overlap[rng];

                int part = usableObjects.IndexOf(go, 0);
                parts[y, x] = part;
            }
            else
            {
                parts[y, x] = 0;
            }
        }
        return usableObjects[parts[y, x]];
    }

    public void ResetParts()
    {
        DestroyChildren();
        parts = new int[width, length];

        currentX = 0;
        currentY = 0;
        currentHeight = 0;

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

        for (int y = 0; y < width; y++)
        {
        float height = 0;
            for (int x = 0; x < length; x++)
            {

                GameObject go = ChoosePart(y, x);

                int heightMod = go.GetComponent<ParkComponent>().heightModifier;


                Vector3 partPosition;

                if (heightMod < 0)
                {
                    height += heightMod;
                }

                if (useHeight)
                {
                    partPosition = new Vector3(x * scale, height * scale, y * scale);
                }
                else
                {
                    partPosition = new Vector3(x * scale, 0, y * scale);
                }

                if (heightMod > 0)
                {
                    height += heightMod;
                }

                //go.transform.localScale = Vector3.one * scale;
                Instantiate(go, partPosition + transform.position, Quaternion.identity, transform);
            }
        }
    }

    public void BuildPart()
    {
        GameObject go = ChoosePart(currentY, currentX);

        int heightMod = go.GetComponent<ParkComponent>().heightModifier;

        Vector3 partPosition;

        if (heightMod < 0)
        {
            currentHeight += heightMod;
        }

        if (useHeight)
        {
            partPosition = new Vector3(currentX * scale, currentHeight * scale, currentY * scale);
        }
        else
        {
            partPosition = new Vector3(currentX * scale, 0, currentY * scale);
        }

        if (heightMod > 0)
        {
            currentHeight += heightMod;
        }

        currentX++;

        if (currentX == length)
        {
            currentY++;
            currentX = 0;
            currentHeight = 0;
            if (currentY == width)
            {
                currentY = 0;
            }
        }

        /*if (currentY != 0 && currentY < width - 1 && currentX != 0 && currentX < length - 1)
        {
            possibleObjects = GetPossibleParts(currentY, currentX);
        }*/

        Debug.Log("X: " + currentX + ", Y: " + currentY);

        Instantiate(go, partPosition, Quaternion.identity, transform);
    }

    public void DestroyChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
