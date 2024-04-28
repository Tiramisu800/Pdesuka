using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pdesuka.Enemy
{
    public class TrapIce : Trap
    {
        public static Action OnKilledByIceSpike;
        public static TrapIce Instance;

        protected override void KillPlayer(IPlayer player)
        {
            base.KillPlayer(player);
            OnKilledByIceSpike?.Invoke();

            Debug.Log("Become Iced Kebab");
        }

    }

}
