using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundStatusUI : MonoBehaviour
{
    private GameRound game => GameRound.instance;

    [SerializeField] TextMeshProUGUI roundLabel;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] TextMeshProUGUI newRoundLabel;

    void Start()
    {
        
    }

    public IEnumerator NewRoundNotifier() {
        newRoundLabel.gameObject.SetActive(true);

        for (float i = 1; i > 0; i -= 0.1f) {
            newRoundLabel.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {
        scoreLabel.text = "Score: " + game.score.ToString();
        roundLabel.text = "ROUND " + game.round.ToString();
    }
}
