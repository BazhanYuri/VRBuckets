using System;
using System.Collections;
using Enums;
using Normal.Realtime;
using Player;
using UnityEngine;

namespace Multiplayer.Services
{
    public class TeamManager : MonoBehaviour
    {
        private int _team1Count = 0;
        private int _team2Count = 0;
        private RealtimeAvatarManager _manager;
        private ScoreBoard _scoreBoard;
        
        private void Awake()
        {
            _manager = FindObjectOfType<RealtimeAvatarManager>();
            _scoreBoard = FindObjectOfType<ScoreBoard>();
            
            _manager.avatarCreated += AssignTeam;
            _manager.avatarDestroyed += AssignTeam;
            _scoreBoard.OnScoreChanged += OnGoaled;
            
            _manager.avatarCreated += OnAvatarCreated;
            _manager.avatarDestroyed += OnAvatarDestroyed;
        }

        private void OnAvatarCreated(RealtimeAvatarManager avatarmanager, RealtimeAvatar avatar, bool islocalavatar)
        {
            Player.Player player = avatar.GetComponent<Player.Player>();
            player.ballsSpawner.BallThrown += OnBallThrown;
        }
        
        private void OnAvatarDestroyed(RealtimeAvatarManager avatarmanager, RealtimeAvatar avatar, bool islocalavatar)
        {
            Player.Player player = avatar.GetComponent<Player.Player>();
            player.ballsSpawner.BallThrown -= OnBallThrown;
        }

        private void AssignTeam(RealtimeAvatarManager avatarmanager, RealtimeAvatar avatar, bool islocalavatar)
        {
            int index = 0;
            int temp1Count = 0;
            int temp2Count = 0;
            
            _scoreBoard.firstTeamUI.DeleteAllPlayerForms();
            _scoreBoard.secondTeamUI.DeleteAllPlayerForms();
            
            foreach (var temp in _manager.avatars)
            {
                Player.Player player = temp.Value.GetComponent<Player.Player>();

                Team team;
                
                if (index % 2 == 0)
                {
                    team = Team.First;
                    temp1Count++;
                    
                    string playerName = PlayerNameSet(player);
                    _scoreBoard.firstTeamUI.AddPlayerForm(playerName);
                }
                else
                {
                    team = Team.Second;
                    temp2Count++;
                    
                    string playerName = PlayerNameSet(player);
                    _scoreBoard.secondTeamUI.AddPlayerForm(playerName);
                }
                player.AssignTeam(team);
                index++;
                
            }
            
            _team1Count = temp1Count;
            _team2Count = temp2Count;
            
            UpdateTeamCount();
        }

        private static string PlayerNameSet(Player.Player player)
        {
            string playerName = "Player " + (player.RealtimeView.ownerID + 1);
            if (player.RealtimeView.isOwnedLocally)
            {
                playerName += " (You)";
            }

            return playerName;
        }


        private void UpdateTeamCount()
        {
            _scoreBoard.UpdateTeamCount(_team1Count, _team2Count);
            
            if (_team1Count == _team2Count)
            {
                StartStartCountdown();
            }
            else
            {
                _scoreBoard.countDownText.text = "Waiting for second player...";
            
                StopCoroutine(StartCountdown());
            }
        }

        private void StartStartCountdown()
        {
            StartCoroutine(StartCountdown());
        }

        private IEnumerator StartCountdown()
        {
            int countdown = 5;
            
            while (countdown > 0)
            {
                _scoreBoard.countDownText.text = "Starts in: " + countdown;
                yield return new WaitForSeconds(1);
                countdown--;
            }
            
            _scoreBoard.countDownText.text = "Start!";
            
            yield return new WaitForSeconds(1);
            
            _scoreBoard.countDownText.gameObject.SetActive(false);
            _scoreBoard.CurrentTurnIndex = 0;
            _scoreBoard.whoseTurnText.gameObject.SetActive(true);
        }


        private void OnGoaled()
        {
            StopCoroutine(StartTimerToCheckIfBallDidNotHit());
        }

        private void SwapTurn()
        {
            _scoreBoard.CurrentTurnIndex = _scoreBoard.CurrentTurnIndex == 0 ? 1 : 0;
        }

        private void OnBallThrown()
        {
            StartCoroutine(StartTimerToCheckIfBallDidNotHit());
        }

        private IEnumerator StartTimerToCheckIfBallDidNotHit()
        {
            yield return new WaitForSeconds(5);
            SwapTurn();
        }
    }

}