using System.Collections;
using UnityEngine;

public class SurvivalBorder : MonoBehaviour 
{    
    public bool IsStop; 

    [SerializeField] private float _speed = 20.0f;

    private Transform _transform;
    private Vector3 _direction = new Vector3(1f, 0, 0);

    private float _time;
    private float _tempTime = 0;

    private int _liftingLevel = 25;


    private void Start()
    {
        _transform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        if (!IsStop && SurvivalLevel.CoolDownIsEnd)
            Move();
        else if (IsStop)
            BorderUp(_liftingLevel);
        SpeedIncrease();

    }
    private void Move()
    {
        var targetPosition = _transform.position + _direction;
        _transform.position = Vector3.Lerp(_transform.position, targetPosition, _speed * Time.deltaTime);
    }
    private void BorderUp(int height)
    {
        _transform.position += new Vector3(-height, 0,0);
        IsStop = false;
    }
    private void SpeedIncrease()
    {
        _time += 1 * Time.deltaTime;
        if(_time - _tempTime > 3)
        {
            _tempTime = _time;
            _speed += 1;
        }    
    }

}
