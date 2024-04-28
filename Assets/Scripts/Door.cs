using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();

        _anim.SetTrigger("Close");
    }

    public void DoorOpenAnim()
    {
        _anim.SetTrigger("Open");
    }


}
