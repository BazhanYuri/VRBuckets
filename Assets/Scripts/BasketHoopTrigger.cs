using System;
using UnityEngine;

public class BasketHoopTrigger : MonoBehaviour
{
    public event Action<Ball> BallEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ball ball))
        {
            BallEntered?.Invoke(ball);
        }
    }
}