using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pdesuka.Enemy;
public class Trigger : Trap
{
    public UnityEvent Act;

    protected override void KillPlayer(IPlayer player)
    {
        Act?.Invoke();
    }
}
