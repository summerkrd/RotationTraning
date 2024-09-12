using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;

    [SerializeField] private float _timer;

    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private GameObject _winMessage;
    [SerializeField] private GameObject _loseMessage;

    [SerializeField] private float _playerToEnemyMaxDistance = 0.5f;

    private Vector3 _targetPoint;
    
    private float _minPositionX = -0.3f;
    private float _maxPositionX = 0.55f;
    private float _minPositionZ = -0.4f;
    private float _maxPositionZ = 0.5f;
    private float _minDistanceToPoint = 0.05f;
    
    private Vector3 _playerToEnemyDistance;
    
    private string _winAnimation = "Win";
    private string _loseAnimation = "Death";

    private bool _finish;

    private void Start()
    {
        GenerateEnemyTargetPoint();
        _finish = false;
        _winMessage.SetActive(false);
        _loseMessage.SetActive(false);
    }

    private void Update()
    {
        Vector3 direction = _targetPoint - _enemy.transform.position;
        Vector3 normalizeDirection = direction.normalized;

        float distance = direction.magnitude;

        if (distance <= _minDistanceToPoint)
        {
            GenerateEnemyTargetPoint();
        }

        _enemy.MoveBody(normalizeDirection, _targetPoint);       

        TimerAndDistance();

        if (_playerToEnemyDistance.magnitude > _playerToEnemyMaxDistance)
        {
            PlayerLose();
        }
        else if (_timer <= 0)
        {
            PlayerWin();
        }

        if (Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GenerateEnemyTargetPoint()
    {
        _targetPoint = new Vector3(Random.Range(_minPositionX, _maxPositionX), _targetPoint.y, Random.Range(_minPositionZ, _maxPositionZ));
    }

    private void TimerAndDistance()
    {
        _playerToEnemyDistance = (_player.transform.position - _enemy.transform.position);

        if(_finish) return;

        _timer -= Time.deltaTime;
        if (_timer < 0) _timer = 0;

        _timerText.text = $"Осталось:\n{_timer.ToString("00")} секунд";
        _distanceText.text = $"Дистанция\n{(_playerToEnemyMaxDistance - _playerToEnemyDistance.magnitude).ToString("0.00")}";
    }

    private void PlayerWin()
    {
        _player.Finish(_winAnimation);
        _enemy.Finish(_loseAnimation);
        Invoke(nameof(ShowWinMessage), 3f);
        _finish = true;
    }

    private void PlayerLose()
    {
        _player.Finish(_loseAnimation);
        _enemy.Finish(_winAnimation);
        Invoke(nameof(ShowLoseMessage), 3f);
        _finish = true;
    }

    private void ShowWinMessage() => _winMessage.SetActive(true);
    private void ShowLoseMessage() => _loseMessage.SetActive(true);
    
}
