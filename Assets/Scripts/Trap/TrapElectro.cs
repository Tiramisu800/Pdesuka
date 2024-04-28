using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pdesuka.Enemy
{
    public class TrapElectro : Trap
    {
        public static Action OnKilledByElectro;
        public static TrapElectro Instance;

        protected override void KillPlayer(IPlayer player)
        {
            base.KillPlayer(player);
            OnKilledByElectro?.Invoke();

            Debug.Log("Tasted 1001 V");
        }
    }
}

