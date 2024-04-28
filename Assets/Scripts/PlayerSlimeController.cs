using Pdesuka.Manager;
using System;
using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Pdesuka.Controllers
{
    public class PlayerSlimeController : Player
    {
        public static PlayerSlimeController Instance;

        [SerializeField] private Animator _anim;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private TrailRenderer _tr;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundCheckDistance = 0.05f;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private int _maxJumps = 3;
        [SerializeField] private float _dashingPower = 10f;
        [SerializeField] private float _dashingTime = 0.2f;
        [SerializeField] private float _dashingCooldown = 1f;

        private bool canDash = true;
        private bool isDashing;
        private float _rememberJumpPressed = 0;

        public float _rememberJumpPressedTime = 0.2f;
        public int _jumpsLeft;

        public float DashingTime { get => _dashingTime; set => _dashingTime = value; }

        void Start()
        {
            _jumpsLeft = _maxJumps;
        }

        private void Update()
        {
            if (isDashing) { return; }

            Jump();
        }
        private void FixedUpdate()
        {
            if (isDashing) { return; }
            transform.Translate(Vector2.right * Time.deltaTime * _speed);
        }

        private void Jump()
        {
            _rememberJumpPressed -= Time.deltaTime;

            if (isGrounded() && _rb.velocity.y <= 0)
            {
                _jumpsLeft = _maxJumps;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    _rememberJumpPressed = _rememberJumpPressedTime;
                }

                if ((_rememberJumpPressed > 0) && _jumpsLeft > 0)
                {
                    _rememberJumpPressed = 0;

                    _rb.velocity = Vector2.up * _jumpForce;

                    _jumpsLeft -= 1;
                    _anim.SetTrigger("isJump");
                    SoundManager.Instance.PlaySound("Jump");
                }

                if ((touch.phase == TouchPhase.Moved) && canDash)
                {
                    StartCoroutine(Dash());

                    Debug.Log("Dashed!");
                }
            }
        }
        private bool isGrounded()
        {
            return Physics2D.OverlapCircle((Vector2)_groundCheck.position, _groundCheckDistance, _groundLayer);
        }

        private IEnumerator Dash()
        {
            canDash = false;
            isDashing = true;
            float originalGravity = _rb.gravityScale;
            var originalVelocity = _rb.velocity;
            _rb.gravityScale = 0f;
            SoundManager.Instance.PlaySound("Dash");
            _rb.velocity = new Vector2(transform.localScale.x * _dashingPower, 0f);
            _tr.emitting = true;
            yield return new WaitForSeconds(_dashingTime);

            _tr.emitting = false;
            _rb.gravityScale = originalGravity;
            _rb.velocity = originalVelocity;
            isDashing = false;
            yield return new WaitForSeconds(_dashingCooldown);
            canDash = true;
        }
    }
}

