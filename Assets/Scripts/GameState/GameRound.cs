using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameRound : MonoBehaviour
{
    public static GameRound instance;
    public GameObject enemy;

    public GameObject player;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;
    [SerializeField] private SwordParry swordParry;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemySpawnPosition;
    [SerializeField] RoundStatusUI roundStatusUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseUI;

    public int score = 0;
    public int bestScore = 0;
    public int round = 1;

    public int initialEnemyHealth = 100;
    public int enemyHealthGrowth = 10;

    public float initialCooldown = 1.25f;
    public float cooldownRegression = 1 / 45;
    public float minimumCooldown = 0.35f;

    public float initialBulletSpeed = 5f;
    public float bulletSpeedGrowth = 0.2f;
    public float maximumBulletSpeed = 20f;

    private List<EnemyShoot.ShootingModes> enemyModes = new() { EnemyShoot.ShootingModes.OneTap };
    private Dictionary<int, EnemyShoot.ShootingModes> enemyModeUpgrades = new() {
        { 2, EnemyShoot.ShootingModes.DoubleTap },
        { 3, EnemyShoot.ShootingModes.Radial },
        { 5, EnemyShoot.ShootingModes.Radial3 },
    };

    public bool gameOver = false;
    public bool paused = false;

    private void Awake() {
        instance = this;
        Application.targetFrameRate = 144;

        bestScore = PlayerPrefs.GetInt("Score");
    }

    private void Update() {
        if (gameOver) return;

        if (playerHealth.currentHP <= 0) {
            gameOver = true;
            // Time.timeScale = 0.0f;

            SaveBestScore();

            player.GetComponent<SpriteRenderer>().enabled = false;
            gameOverUI.SetActive(true);
            StartCoroutine(gameOverUI.GetComponent<GameOverUI>().Show());
        }

        if (score > bestScore) {
            bestScore = score;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (paused) {
                Unpause();
            } else {
                Pause();
            }
        }
    }

    public void Pause() {
        Time.timeScale = 0.0f;
        paused = true;
        pauseUI.SetActive(true);
    }

    public void Unpause() {
        Time.timeScale = 1.0f;
        paused = false;
        pauseUI.SetActive(false);
    }

    public void SaveBestScore() {
        PlayerPrefs.SetInt("Score", bestScore);
        PlayerPrefs.Save();
    }

    IEnumerator GameLoop() {
        while (true) {
            yield return roundStatusUI.NewRoundNotifier(round);

            enemy = Instantiate(enemyPrefab, transform.parent);
            enemy.transform.position = enemySpawnPosition.position;
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            EnemyShoot enemyShoot = enemy.GetComponent<EnemyShoot>();

            enemyHealth.initialHP = enemyHealth.currentHP = GetEnemyHealth(round);
            enemyShoot.initialAttackCooldown = GetEnemyCooldown(round);
            enemyShoot.bulletSpeed = GetEnemyBulletSpeed(round);

            if (enemyModeUpgrades.ContainsKey(round)) {
                enemyModes.Add(enemyModeUpgrades[round]);
            }
            enemyShoot.availableModes = enemyModes;

            yield return new WaitUntil(() => enemyHealth.currentHP == 0);

            Destroy(enemy);
            enemy = null;
            round++;

            playerHealth.currentHP = playerHealth.maxHP;

            yield return new WaitForSeconds(1f);
        }
    }

    public float GetEnemyCooldown(int round) {
        return Mathf.Max(minimumCooldown, -cooldownRegression * (round - 1) + initialCooldown);
    }

    public int GetEnemyHealth(int round) {
        return enemyHealthGrowth * (round - 1) * (round - 1) + initialEnemyHealth;
    }

    public float GetEnemyBulletSpeed(int round) {
        return Mathf.Min(maximumBulletSpeed, initialBulletSpeed + bulletSpeedGrowth * (round - 1));
    }

    public enum UpgradeTypes {
        None, Vitality, FastHands, Spunky
    }
    public void Upgrade(UpgradeTypes type) {
        switch (type) {
            case UpgradeTypes.Vitality:
                playerHealth.maxHP += Mathf.CeilToInt(playerHealth.maxHP * 0.5f);
                playerHealth.currentHP = playerHealth.maxHP;
                break;
            case UpgradeTypes.FastHands:
                swordParry.cooldownTime *= 0.85f;
                break;
            case UpgradeTypes.Spunky:
                playerMovement.dodgeCooldown *= 0.9f;
                break;
        }
    }

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        Time.timeScale = 1.0f;
        StartCoroutine(GameLoop());
    }
}
