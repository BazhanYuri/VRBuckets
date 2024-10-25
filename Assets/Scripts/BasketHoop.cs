using Enums;
using Multiplayer.Services;
using UnityEngine;

public class BasketHoop : MonoBehaviour
{
    public BasketHoopTrigger firstTrigger;
    public BasketHoopTrigger secondTrigger;

    private ScoreBoard _scoreBoard;
    private bool _firstTriggerActivated;
    private const float TimeLimit = 0.7f;

    private void OnEnable()
    {
        firstTrigger.BallEntered += OnFirstTriggerEntered;
        secondTrigger.BallEntered += OnSecondTriggerEntered;
    }

    private void OnDisable()
    {
        firstTrigger.BallEntered -= OnFirstTriggerEntered;
        secondTrigger.BallEntered -= OnSecondTriggerEntered;
    }

    private void Awake()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnFirstTriggerEntered(Ball ball)
    {
        _firstTriggerActivated = true;
        Invoke(nameof(ResetFirstTrigger), TimeLimit);
    }

    private void OnSecondTriggerEntered(Ball ball)
    {
        if (_firstTriggerActivated)
        {
            BallScored(ball);
            CancelInvoke(nameof(ResetFirstTrigger));
            _firstTriggerActivated = false;
        }
    }

    private void ResetFirstTrigger()
    {
        _firstTriggerActivated = false;
    }

    private void BallScored(Ball ball)
    {
        if (ball.Team == Team.First)
        {
            _scoreBoard.IncrementScoreForFirstTeam();
        }
        else
        {
            _scoreBoard.IncrementScoreForSecondTeam();
        }
    }
}