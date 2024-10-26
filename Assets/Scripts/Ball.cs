using System;
using Enums;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ball : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    
    private Team _team;
    
    public event Action OnBallThrown;

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

    public void SetTeam(Team team)
    {
        _team = team;
    }
    
    private void OnSelectExited(SelectExitEventArgs args)
    {
        OnBallThrown?.Invoke();
    }
}