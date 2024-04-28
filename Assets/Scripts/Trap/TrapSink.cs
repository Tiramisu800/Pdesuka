using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pdesuka.Enemy
{
    public class TrapSink : Trap
    {
        public static Action OnKilledByColdWater;
        public static TrapSink Instance;

        protected override void KillPlayer(IPlayer player)
        {
            base.KillPlayer(player);
            OnKilledByColdWater?.Invoke();

            Debug.Log("Sinked");
        }

    }
}

