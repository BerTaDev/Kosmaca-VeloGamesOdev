using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace BerTaDEV
{
    public class GameManager : MonoBehaviour
    {
        public bool isGame;
        public bool isBestScore;
        [Space]
        Animator ui_animator;
        public float game_speed = 1.0f;
        public float game_speed_multiplier = 1.0f;
        public float game_maxSpeed = 5.0f;
        public static GameManager singleton;
        [Space]
        public float current_score = 0.0f;
        public float current_score_multiplier = 1.0f;
        public float best_score = 0.0f;
        public GameObject best_score_text_object;
        public TMP_Text current_score_text;
        public TMP_Text best_score_text;
        [Space]
        public int coin_amount;
        public TMP_Text coin_text;
        [Space]
        public float _curveChangeSpeed = 0.5f;
        public float _curveChangeCooldown;
        float awakeGameSpeed;
        float _curveTimer;
        float _curveValue;
        public Material _curvedMaterial;
        public bool _curvedRight;

        private void Awake()
        {
            singleton = this;
        }
        private void Start()
        {
            awakeGameSpeed = game_speed;
            ui_animator = FindObjectOfType<Canvas>().GetComponent<Animator>();
            MainMenu();
        }
        private void Update()
        {
            if (!isGame) return;

            ChangeCurvedDirection();
            SetGameSpeed();
            SetGameScore();
        }
        private void SetGameSpeed()
        {
            if (game_speed < game_maxSpeed)
            {
                game_speed += game_speed_multiplier * Time.deltaTime;
            }
        }
        private void SetGameScore()
        {
            current_score += Time.deltaTime * (current_score_multiplier * game_speed);
            current_score_text.text = current_score.ToString("0");
            if (CheckBestScore())
            {
                if (!best_score_text_object.activeInHierarchy)
                    best_score_text_object.SetActive(true);
                best_score = current_score;
                best_score_text.text = best_score.ToString("0");
            }
        }
        private bool CheckBestScore()
        {
            return isBestScore = current_score > best_score;
        }
        private void ChangeCurvedDirection()
        {
            _curveTimer += Time.deltaTime;
            if (_curveTimer >= _curveChangeCooldown)
            {
                _curveTimer = 0;
                _curvedRight = !_curvedRight;
            }
            _curveValue = Mathf.Lerp(_curveValue, _curvedRight ? 0.02f : -0.02f, _curveChangeSpeed * Time.deltaTime);
            _curvedMaterial.SetFloat("_SideWay_Strength", _curveValue);
        }
        private void OnApplicationQuit()
        {
            _curvedMaterial.SetFloat("_SideWay_Strength", 0);
            _curvedMaterial.SetFloat("_Backward_Strength", 0f);
        }
        public void MainMenu()
        {
            SFXManager.singleton.PlayButton();
            coin_amount = PlayerPrefs.GetInt("player_coin_amount");
            coin_text.text = coin_amount.ToString();

            current_score = 0;
            current_score_text.text = current_score.ToString("0");

            best_score_text_object.SetActive(false);
            best_score = PlayerPrefs.GetFloat("player_best_score");
            best_score_text.text = best_score.ToString("0");


            ui_animator.SetInteger("state", 0);
            isGame = false;
            PlayerController.singleton.thirdPerson = true;
            PlayerRagdoll.singleton.SetRagdoll(false);
            ObjectPooling.singleton.DisableAllObstacles();
            _curveValue = 0.02f;
            _curvedRight = true;
            _curvedMaterial.SetFloat("_SideWay_Strength", _curveValue);
            _curvedMaterial.SetFloat("_Backward_Strength", -0.01f);
            PlayerController.singleton.animator.SetBool("isGame", false);
        }
        public void NewGame()
        {
            SFXManager.singleton.PlayButton();
            isGame = true;
            ui_animator.SetInteger("state", 1);
            game_speed = awakeGameSpeed;

            PlayerController.singleton.thirdPerson = false;
            PlayerRagdoll.singleton.SetRagdoll(false);
            ObstacleSpawner.singleton.onNewGame();
            PlayerController.singleton.animator.SetBool("isGame", true);
        }
        public void ExitGame()
        {
            Invoke(nameof(delayedExit), 0.5f);
        }
        void delayedExit()
        {
            SFXManager.singleton.PlayButton();
            Application.Quit();
        }
        public void FailGame()
        {
            if (!isGame) return;
            SFXManager.singleton.PlayFail();
            ui_animator.SetInteger("state", 2);
            isGame = false;
            PlayerController.singleton.thirdPerson = true;
            PlayerRagdoll.singleton.SetRagdoll(true);
            if (isBestScore)
            {
                PlayerPrefs.SetFloat("player_best_score", best_score);
            }
        }
        public void AddCoin(int amount)
        {
            SFXManager.singleton.PlayCoin();
            coin_amount += amount;
            coin_text.text = coin_amount.ToString();
            PlayerPrefs.SetInt("player_coin_amount", coin_amount);
        }
    }
}
