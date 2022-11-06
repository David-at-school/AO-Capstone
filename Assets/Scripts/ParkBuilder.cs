using System.Collections;
using System.Collections.Generic;
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

    public void ChooseParts()
    {
        parts = new int[width, length];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == length - 1)
                {
                    parts[i, j] = 0;
                }
                else
                {
                    if (parts[i, j - 1] == 20)
                    {
                        if (parts[i, j + 1] != 0)
                        {
                            if (Random.value < 0.5f)
                            {
                                parts[i, j] = 4;
                            }
                            else
                            {
                                parts[i, j] = 17;
                            }
                        }
                        else
                        {
                            parts[i, j] = 17;
                        }
                    }
                }
            }
        }
    }

    public void BuildPark()
    {
        DestroyChildren();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Vector3 partPosition = new Vector3(j * 1, 0, i * 1);

                if (i == 0 || i == width - 1 || j == 0 || j == length - 1)
                {
                    Instantiate(usableObjects[0], partPosition, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(usableObjects[Random.Range(0, usableObjects.Count)], partPosition, Quaternion.identity, transform);
                }
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
