using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;

    [SerializeField] private float _timer;

    [SerializeField] private float _minDistance = 5;

    private Vector3 _targetPoint;
    
    private float _minPositionX = -0.3f;
    private float _maxPositionX = 0.55f;
    private float _minPositionZ = -0.4f;
    private float _maxPositionZ = 0.5f;

    private void Start()
    {
        GenerateEnemyTargetPoint();
        Debug.Log(_targetPoint);
    }

    private void Update()
    {
        Vector3 direction = _targetPoint - _enemy.transform.position;
        Vector3 normalizeDirection = direction.normalized;


        float distance = direction.magnitude;

        _enemy.Move(normalizeDirection, _targetPoint);

        if (distance <= 0.1f)
        {
            GenerateEnemyTargetPoint();
        }
    }

    private void GenerateEnemyTargetPoint()
    {
        _targetPoint = new Vector3(Random.Range(_minPositionX, _maxPositionX), _targetPoint.y, Random.Range(_minPositionZ, _maxPositionZ));
    }
}
