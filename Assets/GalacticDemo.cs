using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class GalacticDemo : MonoBehaviour
{
    [SerializeField] int starCount = 5;
    [SerializeField] int maxOrbit = 5;
    [SerializeField] float noise = 15;
    [SerializeField] float orbitSpeed;
    [SerializeField] Mesh mesh = null;
    [SerializeField] Material mat = null;
    




    void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(StarComponent),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld));

        NativeArray<Entity> entityArray = new NativeArray<Entity>(starCount, Allocator.Temp);

        entityManager.CreateEntity(entityArchetype, entityArray);

        for (int i = 0; i < entityArray.Length; i++)
        {
            int r = Random.Range(-maxOrbit, maxOrbit);
            float n = Random.Range(-noise, noise);

            Entity entity = entityArray[i];
            entityManager.SetComponentData(entity, new StarComponent { radius = r, noise = n, speed = orbitSpeed });

            entityManager.SetSharedComponentData(entity, new RenderMesh 
            {
                mesh = mesh,
                material = mat
            });
            
        }

        entityArray.Dispose();
    }

}
