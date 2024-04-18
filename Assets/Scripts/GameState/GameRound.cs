using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRound : MonoBehaviour
{
    public static GameRound instance;
    public GameObject enemy;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemySpawnPosition;
    [SerializeField] RoundStatusUI roundStatusUI;

    public int score = 0;
    public int round = 1;

    private void Awake() {
        instance = this;
    }

    IEnumerator GameLoop() {
        while (true) {
            yield return roundStatusUI.NewRoundNotifier(round);

            enemy = Instantiate(enemyPrefab, transform.parent);
            enemy.transform.position = enemySpawnPosition.position;
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            yield return new WaitUntil(() => enemyHealth.currentHP == 0);

            Destroy(enemy);
            enemy = null;
            round++;

            yield return new WaitForSeconds(1f);
        }
    }

    void Start()
    {
        StartCoroutine(GameLoop());
    }
}
