using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class StarSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation star, ref StarComponent orbit) =>
        {
            star.Value.x = Mathf.Sin(((float)Time.ElapsedTime) * orbit.speed + orbit.noise) * orbit.radius;
            star.Value.z = Mathf.Cos(((float)Time.ElapsedTime) * orbit.speed + orbit.noise) * orbit.radius;
        });
    }
}
