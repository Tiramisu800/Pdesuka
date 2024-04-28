using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pdesuka.Enemy
{
    public class Trap : MonoBehaviour
    {
        protected virtual void KillPlayer(IPlayer player)
        {
            player.MakeDamage();
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<IPlayer>() is IPlayer player)
            {
                KillPlayer(player);
            }
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<IPlayer>() is IPlayer player)
            {
                KillPlayer(player);
            }

        }
    }

}
