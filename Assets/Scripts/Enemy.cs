using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private Animator _animator;    

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();        
    }

    private void Start()
    {
        _animator.SetBool("isRunning", true);
    }

    public void Move(Vector3 direction, Vector3 target)
    {
        transform.Translate(new Vector3(direction.x, 0, direction.z) * _moveSpeed * Time.deltaTime);
        
    }
}
