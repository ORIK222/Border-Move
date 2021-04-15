using UnityEngine;
using TouchScript.Gestures;

public class GesturesHandler : MonoBehaviour
{

    [SerializeField] TapGesture _tap1Gesture;
    [SerializeField] TapGesture _tap2Gesture;
    [SerializeField] FlickGesture _horizontalFlick;
    [SerializeField] LongPressGesture _longPres1Gesture;
    [SerializeField] LongPressGesture _longPres2Gesture;
    [SerializeField] int _playerNum;
    private CommandsManager _commandManager;
    [SerializeField] private Vector2 _orientation;

    void Start()
    {
        _commandManager = GetComponent<CommandsManager>();
        _tap2Gesture.AddFriendlyGesture(_tap1Gesture);
    }

    private void OnEnable()
    {
        _tap1Gesture.Tapped += tap1GestureHandler;
        _tap2Gesture.Tapped += tap2GestureHandler;

        _horizontalFlick.Flicked += flickHandler;

        _longPres1Gesture.LongPressed += longPres1GestureHandler;
        _longPres2Gesture.LongPressed += longPres2GestureHandler;
    }

    private void OnDisable()
    {
        _tap1Gesture.Tapped -= tap1GestureHandler;
        _tap2Gesture.Tapped -= tap2GestureHandler;

        _horizontalFlick.Flicked -= flickHandler;

        _longPres1Gesture.LongPressed -= longPres1GestureHandler;
        _longPres2Gesture.LongPressed -= longPres2GestureHandler;
    }

    private void tap1GestureHandler(object sender, System.EventArgs e)
    {
        HandleTapEvent(1);
    }

    private void tap2GestureHandler(object sender, System.EventArgs e)
    {
        HandleTapEvent(2);
    }

    private void flickHandler(object sender, System.EventArgs e)
    {
        HandleFlickEvent(_horizontalFlick.ScreenFlickVector);
    }

    private void longPres1GestureHandler(object sender, System.EventArgs e)
    {
        LongPresGestureHandler(1);
    }

    private void longPres2GestureHandler(object sender, System.EventArgs e)
    {
        LongPresGestureHandler(2);
    }

    void LongPresGestureHandler(int touchCount)
    {
        if (touchCount == 1)
        {
            _commandManager.CheckCurrentCommand(CommandsManager.CommandType.OneFingerLongTap);
        }
        if (touchCount == 2)
        {
            _commandManager.CheckCurrentCommand(CommandsManager.CommandType.TwoFingerLongTap);
        }
    }


    void HandleFlickEvent(Vector2 direction)
    {
        Vector2 absDirection = new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
        if (absDirection.x > absDirection.y)
        {
            direction = new Vector2(direction.x, 0);
        }
        else
        {
            direction = new Vector2(0, direction.y);
        }

        Vector2 userDir = new Vector2(direction.y * _orientation.x, direction.x * _orientation.y);

        if (userDir.x < 0)
        {
            _commandManager.CheckCurrentCommand(CommandsManager.CommandType.LeftFlick);
        }
        else if (userDir.x > 0)
        {
            _commandManager.CheckCurrentCommand(CommandsManager.CommandType.RightFlick);
        }
        else
        {
            if (userDir.y < 0)
            {
                _commandManager.CheckCurrentCommand(CommandsManager.CommandType.DownFlick);
            }
            else if (userDir.y > 0)
            {
                _commandManager.CheckCurrentCommand(CommandsManager.CommandType.UpFlick);
            }
        }
    }

    void HandleTapEvent(int tapCount)
    {
        if (1 == tapCount)
        {
            _commandManager.CheckCurrentCommand(CommandsManager.CommandType.OneFingerTap);
        }
        else if (1 == tapCount)
        {
            _commandManager.CheckCurrentCommand(CommandsManager.CommandType.TwoFingerTap);
        }
    }
}