using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pdesuka.Enemy
{
    public class ChaseController : MonoBehaviour
    {
        public TrapBat[] bats;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<IPlayer>() is IPlayer player)
            {
                foreach (var bat in bats)
                {
                    bat.isChasing = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<IPlayer>() is IPlayer player)
            {
                foreach (var bat in bats)
                {
                    bat.isChasing = false;
                }
            }
        }
    }

}
