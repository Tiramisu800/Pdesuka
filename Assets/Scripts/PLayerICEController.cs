using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using Pdesuka.Manager;

namespace Pdesuka.Controllers
{
    public class PLayerICEController : Player
    {
        public float _speed = 50;
        public bool canMove = false;
        private Vector3 _playerDir = Vector3.zero;

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Animator _anim;
        [SerializeField] private SwipeListener _swipeListener;

        //Swipe
        private void OnEnable()
        {
            _swipeListener.OnSwipe.AddListener(OnSwipe);
        }
        private void OnSwipe(string swipe)
        {
            if (canMove == true)
            {
                switch (swipe)
                {
                    case "Left":
                        _playerDir = Vector3.left;
                        _anim.SetTrigger("Left");
                        SoundManager.Instance.PlaySound("Skate");
                        break;
                    case "Right":
                        _playerDir = Vector3.right;
                        _anim.SetTrigger("Right");
                        SoundManager.Instance.PlaySound("Skate");
                        break;
                    case "Up":
                        _playerDir = Vector3.up;
                        _anim.SetTrigger("Up");
                        SoundManager.Instance.PlaySound("Skate");
                        break;
                    case "Down":
                        _playerDir = Vector3.down;
                        _anim.SetTrigger("Down");
                        SoundManager.Instance.PlaySound("Skate");
                        break;
                }
            }

        }
        private void OnDisable()
        {
            _swipeListener.OnSwipe.RemoveListener(OnSwipe);
        }


        void Update()
        {
            _rb.AddForce(_playerDir * _speed);
        }

        //Can Move bool
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Wall")
            {
                canMove = true;
                SoundManager.Instance.PlaySound("Bump");
            }
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Wall")
            {
                canMove = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            canMove = false;
        }

    }
}

