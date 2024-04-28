using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pdesuka.Enemy
{
    public class TrapRat : Trap
    {
        public static Action OnKilledByRat;

        [SerializeField] private float _speed;
        [SerializeField] private float _distance = 1f;

        private Vector2 _startPoint;
        private Vector2 _endPoint;
        private bool isMovingLeft = false;

        private void Start()
        {
            _startPoint = transform.position;
            _endPoint = new Vector2(transform.position.x + _distance, transform.position.y);
        }
        private void Update()
        {
            if (isMovingLeft == true)
            {
                Return();
            }
            else
            {
                Patrol();
            }
            Flip();
        }

        private void Patrol()
        {
            transform.position = Vector2.MoveTowards(transform.position, _endPoint, _speed * Time.deltaTime);
        }
        private void Return()
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPoint, _speed * Time.deltaTime);
        }
        private void Flip()
        {
            if ((Vector2)transform.position == _endPoint)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isMovingLeft = true;
            }
            else if ((Vector2)transform.position == _startPoint)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                isMovingLeft = false;
            }
        }
        protected override void KillPlayer(IPlayer player)
        {
            base.KillPlayer(player);
            OnKilledByRat?.Invoke();
        }

    }
}

