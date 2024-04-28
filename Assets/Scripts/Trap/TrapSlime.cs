using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pdesuka.Enemy
{
    public class TrapSlime : Trap
    {
        public static Action OnKilledBySlime;

        public float _jumpForce = 5f;
        public float _jumpTime = 1f;
        private bool isJump = false;
        private Rigidbody2D _rb;
        [SerializeField] private Animator _anim;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            InvokeRepeating("Jump", 0f, _jumpTime);
        }

        void Update()
        {

        }

        private void Jump()
        {
            if (isJump == false)
            {
                isJump = true;
                //_rb.AddForce(Vector2.up * _jumpForce);
                _rb.velocity = Vector2.up * _jumpForce;
                _anim.SetTrigger("Jump");
            }
            else
            {
                isJump = false;
            }
        }


        protected override void KillPlayer(IPlayer player)
        {
            base.KillPlayer(player);
            OnKilledBySlime?.Invoke();
        }
    }

}
