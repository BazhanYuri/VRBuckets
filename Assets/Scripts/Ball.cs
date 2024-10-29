using System;
using System.Collections;
using DefaultNamespace;
using Enums;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ball : MonoBehaviour
{
    public RealtimeTransform realtimeTransform;
    public XRGrabInteractable grabInteractable;
    
    private Team _team;
    private BasketHoop _basketHoop;
    public ThrowScoreZone currentThrowZone;

    public event Action<Ball> OnBallThrown;

    public Team Team
    {
        get => _team;
        set => _team = value;
    }

    private void OnEnable()
    {
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }
    
    private void OnDisable()
    {
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void Awake()
    {
        _basketHoop = FindObjectOfType<BasketHoop>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            InvokeBallThrown();
            transform.position = _basketHoop.goalPointForPC.position;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            InvokeBallThrown();
            transform.position = _basketHoop.goalPointForPC.position + new Vector3(1.0f, 0, 0);
        }
    }

    
    public void SetTeam(Team team)
    {
        _team = team;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (realtimeTransform.isOwnedLocally == false)
        {
            return;
        }
        InvokeBallThrown();
    }

    private void InvokeBallThrown()
    {
        OnBallThrown?.Invoke(this);

        StartCoroutine(DestroyBall());
    }

    private IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(5f);
        Realtime.Destroy(gameObject);
    }
}