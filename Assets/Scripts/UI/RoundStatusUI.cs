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

    public IEnumerator NewRoundNotifier(int round) {
        newRoundLabel.gameObject.SetActive(true);
        newRoundLabel.text = "ROUND " + round.ToString();
        newRoundLabel.color = new Color(1, 1, 1, 0);
        newRoundLabel.transform.localScale = new Vector3(2, 2, 2);

        newRoundLabel.transform.LeanScale(new Vector3(1, 1, 1), 1f).setEaseInExpo();
        LeanTween.value(newRoundLabel.gameObject, 0f, 1f, 1f)
            .setOnUpdate((float value) => {
                newRoundLabel.alpha = value;
            })
            .setEaseLinear();
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(2f);

        LeanTween.value(newRoundLabel.gameObject, 1f, 0f, 1f)
            .setOnUpdate((float value) => {
                newRoundLabel.alpha = value;
            })
            .setEaseLinear();
        yield return new WaitForSeconds(1f);

        newRoundLabel.gameObject.SetActive(false);
    }

    void Update()
    {
        scoreLabel.text = "Score: " + game.score.ToString();
        roundLabel.text = "ROUND " + game.round.ToString();
    }
}
