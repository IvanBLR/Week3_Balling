using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Action<Vector3, Color> OnPlayerTouchedSphereEvent;

    [SerializeField]
    private UI_Controller _ui;

    [SerializeField]
    private CylindrSpawner _cylinderSpawner;

    [SerializeField]
    private Ball _ball;

    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private OutsideController _outsideController;

    [SerializeField]
    private Color[] _colors;

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _ballPrefab;

    [SerializeField]
    private GameObject _playerPrefab;

    private float[] _xPlaneCoordinate = new float[] { -4, -3, -2, -1, 1, 2, 3, 4 };
    private float[] _yPlaneCoordinate = new float[] { -4, -3, -2, -1, 1, 2, 3, 4 };

    private List<float> _lastThreeAvaliblePoints = new List<float>();
    private void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            _cylinderSpawner.Initialize(_enemyPrefab, GetStartCoordinates(ref _xPlaneCoordinate, ref _yPlaneCoordinate), GetColor());
        }
        _ball.Initialize(_ballPrefab, GetStartCoordinates(ref _xPlaneCoordinate, ref _yPlaneCoordinate), GetColor());
        _playerController.Initialize(_playerPrefab, new Vector3(0, 0, 0));
    }
    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_xPlaneCoordinate[i] != 0)
            {
                _lastThreeAvaliblePoints.Add(_xPlaneCoordinate[i]);
            }
            if (_yPlaneCoordinate[i] != 0)
            {
                _lastThreeAvaliblePoints.Add(_yPlaneCoordinate[i]);
            }
        }
        _lastThreeAvaliblePoints.Add(0);

        _playerController.OnMouseClickEvent += _ui.UpdateScore;
    }

    private void Update()
    {
        if (_outsideController.IsPlayerOutside)
        {
            _playerController.Initialize(_playerPrefab, new Vector3(0, 0, 0));
            _outsideController.IsPlayerOutside = false;
        }
    }
    public void GenerateNewPositionAndColor()
    {
        var position = GetOtherCoordinatesToSphere();
        var color = GetColor();
        OnPlayerTouchedSphereEvent?.Invoke(position, color);
    }
    private Vector3 GetStartCoordinates(ref float[] arrayX, ref float[] arrayY)
    {
        int indexX = UnityEngine.Random.Range(0, 8);
        while (arrayX[indexX] == 0)
        {
            indexX = UnityEngine.Random.Range(0, 8);
        }
        int indexZ = UnityEngine.Random.Range(0, 8);
        while (arrayY[indexZ] == 0)
        {
            indexZ = UnityEngine.Random.Range(0, 8);
        }
        float x = arrayX[indexX];
        float y = 0.5f;
        float z = arrayY[indexZ];

        arrayX[indexX] = 0;
        arrayY[indexZ] = 0;

        return new Vector3(x, y, z);
    }
    private Vector3 GetOtherCoordinatesToSphere()
    {
        int index = UnityEngine.Random.Range(0, 6);

        switch (index)
        {
            case 0:
                return new Vector3(_lastThreeAvaliblePoints[0], 0.5f, _lastThreeAvaliblePoints[1]);
            case 1:
                return new Vector3(_lastThreeAvaliblePoints[1], 0.5f, _lastThreeAvaliblePoints[2]);
            case 2:
                return new Vector3(_lastThreeAvaliblePoints[0], 0.5f, _lastThreeAvaliblePoints[2]);
            case 3:
                return new Vector3(_lastThreeAvaliblePoints[1], 0.5f, _lastThreeAvaliblePoints[0]);
            case 4:
                return new Vector3(_lastThreeAvaliblePoints[2], 0.5f, _lastThreeAvaliblePoints[1]);
            case 5:
                return new Vector3(_lastThreeAvaliblePoints[2], 0.5f, _lastThreeAvaliblePoints[0]);
        }
        return new Vector3(2, 0.5f, 0);
    }
    private Color GetColor()
    {
        int index = UnityEngine.Random.Range(0, 3);
        return _colors[index];
    }

}
