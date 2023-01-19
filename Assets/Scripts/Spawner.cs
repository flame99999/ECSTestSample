using System;
using TMPro;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int initialAmount;
    [SerializeField] private float maxHeight = 1;
    [SerializeField] private Mesh objMesh;
    [SerializeField] private Material objMat;
    [SerializeField] private GameObject gameObjectPrefab;
    [SerializeField] private TextMeshProUGUI currentAmountText;

    private int totalSpawned;
    private EntityManager entityManager;
    private World defaultWorld;
    private EntityArchetype archetype;
    private Entity unitEntityPrefab;

    private void Start()
    {
        if (GameManager.Instance.useECS)
        {
            SetupECS();
        }

        SpawnUnit(initialAmount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnUnit(initialAmount);
        }
    }
    private void SetupECS()
    {
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;
        archetype = entityManager.CreateArchetype
        (
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(ObjData),
            typeof(Scale),
            typeof(RenderBounds)
        );
    }

    public void SpawnUnit(int numToSpawn)
    {
        totalSpawned += numToSpawn;
        currentAmountText.text = $"Amount: {totalSpawned}";
        
        if (GameManager.Instance.useECS)
        {
            SpawnECS(numToSpawn);
        }
        else
        {
            SpawnGameObject(numToSpawn);
        }
    }

    private void SpawnGameObject(int numToSpawn)
    {
        for (var i = 0; i < numToSpawn; i++)
        {
            var instance = Instantiate(gameObjectPrefab, transform);
            instance.transform.position = GetRandomPosition();
            instance.transform.localScale = new float3(UnityEngine.Random.Range(0.1f, 1));
        }
    }

    private void SpawnECS(int numToSpawn)
    {
        for (var i = 0; i < numToSpawn; i++)
        {
            var newEntity = entityManager.CreateEntity(archetype);

            entityManager.AddComponentData(newEntity, new Translation {Value = GetRandomPosition()});
            entityManager.AddComponentData(newEntity, new Scale {Value = UnityEngine.Random.Range(0.1f, 1)});

            entityManager.AddSharedComponentData(newEntity, new RenderMesh
            {
                mesh = objMesh,
                material = objMat
            });

            entityManager.AddComponentData(newEntity, new ObjData {speed = GameManager.Instance.moveSpeed});
        }
    }

    private float3 GetRandomPosition()
    {
        var randomX = UnityEngine.Random.Range(GameManager.Instance.LeftBounds, GameManager.Instance.RightBounds);
        var randomY = UnityEngine.Random.Range(-1f, 1f) * maxHeight;
        var randomZ = UnityEngine.Random.Range(GameManager.Instance.UpperBounds, GameManager.Instance.BottomBounds);
        return new float3(randomX, randomY, randomZ);
    }
}