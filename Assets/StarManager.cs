using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class StarManager : MonoBehaviour
{
    TransformAccessArray transforms;
    OrbitJob orbitJob;
    
    JobHandle orbitHandle;

    GameObject star;
    public int starCount;
    public int maxRadius;

    void OnDisable()
    {
        orbitHandle.Complete();
        transforms.Dispose();
    }

    void Start()
    {
        transforms = new TransformAccessArray(0, -1);
        AddStars(starCount);

    }

    // Update is called once per frame
    void Update()
    {
        orbitHandle.Complete();

        orbitJob = new OrbitJob() { speed = 5, deltaTime = Time.deltaTime };

        orbitHandle = orbitJob.Schedule(transforms);

        JobHandle.ScheduleBatchedJobs();
    }
    
    void AddStars(int stars)
    {
        orbitHandle.Complete();

        transforms.capacity = transforms.length + stars;

        for(int i = 0; i < stars; i++)
        {
            float x = UnityEngine.Random.Range(-100, 100);
            float y = UnityEngine.Random.Range(-100, 100);

            Vector2 unclamped = new Vector2(x, y);
            Vector2 clamped = Vector2.ClampMagnitude(unclamped, maxRadius);

            Vector3 pos = new Vector3(clamped.x, 0, clamped.y);
            Quaternion rot = Quaternion.Euler(0, 0, 0);

            var go = Instantiate(star, pos, rot);

            transforms.Add(go.transform);
        }
    }
}
