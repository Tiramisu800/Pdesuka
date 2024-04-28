using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Animator _anim;
    public float _boxLifeTime = 1f;
    
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<IPlayer>() is IPlayer player)
        {
            _anim.SetTrigger("Hit");
            StartCoroutine(Break());
            
        }
    }

    IEnumerator Break()
    {
        yield return new WaitForSeconds(_boxLifeTime);
        gameObject.SetActive(false);
        enabled = false;
    }
}
