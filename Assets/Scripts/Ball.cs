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
    public AudioSource ballAudioSource;
    public Rigidbody rb;

    private Team _team;
    private bool _isThrown = false;
    private BasketHoop _basketHoop;
    public ThrowScoreZone currentThrowZone;

    public event Action<Ball> OnBallExitedZoneOrThrown;

    public Team Team
    {
        get => _team;
    }

    private void OnEnable()
    {
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnCollisionEnter(Collision other)
    {
        ballAudioSource.Play();
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
        StartCoroutine(DeleteGrabbable());
    }

    private void InvokeBallThrown()
    {
        if (_isThrown)
        {
            return;
        }

        _isThrown = true;

        StartCoroutine(DestroyBall());
    }

    private IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(2.5f);
        OnBallExitedZoneOrThrown?.Invoke(this);
        Realtime.Destroy(gameObject);
    }

    private IEnumerator DeleteGrabbable()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(grabInteractable);
    }
}