using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    
    private Animator _animator;

    private string _isRunning = "isRunning";

    private bool _finish;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _finish = false;
        _animator.SetBool(_isRunning, true);
    }

    public void MoveBody(Vector3 direction, Vector3 target)
    {
        if (_finish) return;

        transform.Translate(new Vector3(direction.x, 0, direction.z) * _moveSpeed * Time.deltaTime, Space.World);
        transform.LookAt(target);
        Debug.DrawRay(transform.position, direction * 0.1f, Color.red);
    }    

    public void Finish(string animation)
    {
        _finish = true;
        _animator.SetTrigger(animation);
    }
}
