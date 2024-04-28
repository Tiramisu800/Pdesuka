using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public event Action OnKilled;

    public void MakeDamage()
    {
        GetComponent<Collider2D>().isTrigger = true;
        enabled = false;

        Debug.Log("You are Dead");

        OnKilled?.Invoke();
    }
}
