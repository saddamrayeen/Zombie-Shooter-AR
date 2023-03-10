using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 30;

    private void Start()
    {
        // destroy bullet
        Destroy(gameObject, 5f);
    }
}
