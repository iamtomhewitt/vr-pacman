﻿using UnityEngine;
using System.Collections;

public class DestroyAfterLifetime : MonoBehaviour 
{
    public float lifetime;

    void Start()
    {
        Destroy(this.gameObject, lifetime);
    }
}
