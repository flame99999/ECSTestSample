using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;

public class ObjControlSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var deltaTime = Time.DeltaTime;
        var upperBounds = GameManager.Instance.UpperBounds;
        var lowerBounds = GameManager.Instance.BottomBounds;

        var jobHandle = Entities.ForEach((ref Translation trans, ref ObjData objData) =>
        {
            trans.Value += new float3(0f, 0f, objData.speed * deltaTime);

            if (trans.Value.z >= upperBounds)
            {
                trans.Value.z = lowerBounds;
            }
        }).Schedule(inputDeps);

        return jobHandle;
    }
}