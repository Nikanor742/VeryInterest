using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private bool randomPath;
    private List<Vector3> randomizedPath;

    [ShowIf(nameof(randomPath))] [SerializeField] private int pathPointCount;
    [ShowIf(nameof(randomPath))] [SerializeField] private float minimalPositionX;
    [ShowIf(nameof(randomPath))] [SerializeField] private float maximalPositionX;
    [ShowIf(nameof(randomPath))] [SerializeField] private float maximalStepZ;
    [ShowIf(nameof(randomPath))] [SerializeField] private float moveTime;
    [ShowIf(nameof(randomPath))] [SerializeField] private int loop;
    [ShowIf(nameof(randomPath))] [SerializeField] private int smoothPath;
    

    private Network network;
    private Settings settings;
    private MovingObject movingObject;
    
    private bool active;
    

    private void Start()
    {
        Init();
    }

    private void RandomizePath()
    {
        randomizedPath = new List<Vector3>();
        for (int i = 0; i < pathPointCount; i++)
        {
            if (i != 0)
            {
                randomizedPath.Add(new Vector3(
                    Random.Range(minimalPositionX, maximalPositionX),
                    0,
                    randomizedPath[i - 1].z + Random.Range(0, maximalStepZ)));
            }
            else
            {
                randomizedPath.Add(new Vector3(
                    Random.Range(minimalPositionX,maximalPositionX),
                    0,
                    0));
            }
        }
    }
    private void Init()
    {
        movingObject = FindObjectOfType<MovingObject>();

        if (randomPath)
        {
            if (movingObject != null)
            {
                RandomizePath();
                active = true;
            }
        }
        else
        {
            network = FindObjectOfType<Network>();
            if (movingObject != null && network != null)
            {
                network.OnJSONDownloaded += DataDownloaded;
            }
            else
            {
                Debug.LogError("InitError: MovingObject or Network not found");
            }
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && active)
        {
            if (randomPath)
            {
                movingObject.Move(randomizedPath.ToArray(), moveTime, loop, smoothPath);
            }
            else
            {
                movingObject.Move(settings.objectPath.ToArray(), settings.moveTime, settings.loop, settings.smoothPath);
            }
        } 
    }
    private void DataDownloaded(string json)
    {
        settings = JsonHelper.GetObjectFromJson<Settings>(json);
        active = true;
    }
    

    private void OnDestroy()
    {
        if(network != null)
            network.OnJSONDownloaded -= DataDownloaded;
    }
}


