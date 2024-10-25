using Enums;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Team _team;

    public Team Team
    {
        get => _team;
        set => _team = value;
    }

    public void SetTeam(Team team)
    {
        _team = team;
    }
}