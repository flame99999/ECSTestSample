using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Entities;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public bool useECS;
    public float moveSpeed = 2;
    private float upperBounds = 20;
    private float leftBounds = -18;
    private float rightBounds = 18;
    private float bottomBounds = -6;


    public float UpperBounds => upperBounds;
    public float BottomBounds => bottomBounds;
    public float LeftBounds => leftBounds;
    public float RightBounds => rightBounds;
    


    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetSystem();
    }

   

    private void SetSystem()
    {
        World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<ObjControlSystem>().Enabled = false;
        if (useECS)
        {
            World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<ObjControlSystem>().Enabled = true;
        }
    }
}