using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;


public class OrbitJob : IJobParallelForTransform
{
    public float speed;
    public float deltaTime;
    
    public void Execute(int index, TransformAccess transform)
    {
        Vector3 pos = transform.position;

        pos += speed * deltaTime * (transform.rotation * new Vector3(0, 0, 1));

        transform.position = pos;
    }
}
