using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CinemachineSwitcher : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchCamera(string cameraName)
    {
        _animator.Play(cameraName);
    }
}
