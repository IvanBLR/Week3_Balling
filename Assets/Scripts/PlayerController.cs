using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    public Action<int> OnMouseClickEvent;

    [SerializeField]
    private Slider _force;

    private int _clickCounter;
    private GameObject _player;
    private Rigidbody _rigidbody;
    private Vector3 _directionForce;
    private Stopwatch _stopwatch;

    private void Start()
    {
        _stopwatch = new Stopwatch();
        _force.value = 650;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _stopwatch.Restart();
        }
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _stopwatch.Stop();

            if (Physics.Raycast(ray, out var hitInfo))
            {
                _directionForce = (hitInfo.point - _player.transform.position).normalized;
                var multiplier = Mathf.Clamp((float)_stopwatch.Elapsed.TotalSeconds, 0.1f, 1);
                PlayersMoving(_directionForce, multiplier);
                _clickCounter++;
                OnMouseClickEvent?.Invoke(_clickCounter);
            }
        }
    }


    public void Initialize(GameObject playerPrefab, Vector3 position)
    {
        var player = Instantiate(playerPrefab);
        player.transform.position = position;
        _rigidbody = player.GetComponent<Rigidbody>();
        _player = player;
        _player.AddComponent<CheckPlayerCollisionWithEnemy>();
    }

    private void PlayersMoving(Vector3 directionForce, float multiplier)
    {
        _rigidbody.AddForce(directionForce * _force.value * multiplier);
    }
}

