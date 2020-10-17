using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


public struct StarComponent : IComponentData
{
    public float radius;
    public float noise;
    public float speed;
}