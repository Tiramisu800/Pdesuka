using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pdesuka.Enemy
{
    public class TrapBat : Trap
    {
        public static Action OnKilledByBat;

        [SerializeField] private GameObject _player;

        public float _speed;
        public bool isChasing = false;
        private Vector2 _startPoint;

        private void Start()
        {
            _startPoint = transform.position;
        }
        void Update()
        {
            if (isChasing == true)
            {
                Chase();
            }
            else
            {
                Return();
            }
            Flip();
        }

        private void Chase()
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        }
        private void Flip()
        {
            if (transform.position.x > _player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        private void Return()
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPoint, _speed * Time.deltaTime);
        }

        protected override void KillPlayer(IPlayer player)
        {
            base.KillPlayer(player);
            OnKilledByBat?.Invoke();
        }
    }
}

