using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundStatusUI : MonoBehaviour
{
    private GameRound game => GameRound.instance;

    [SerializeField] TextMeshProUGUI roundLabel;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] TextMeshProUGUI newRoundLabel;
    [SerializeField] TextMeshProUGUI bestScoreLabel;

    [SerializeField] GameObject upgradePanel;
    [SerializeField] TextMeshProUGUI upgradeLabel;

    public IEnumerator NewRoundNotifier(int round) {
        newRoundLabel.gameObject.SetActive(true);
        newRoundLabel.text = "ROUND " + round.ToString();
        newRoundLabel.color = new Color(1, 1, 1, 0);
        newRoundLabel.transform.localScale = new Vector3(2, 2, 2);

        // Fade in
        newRoundLabel.transform.LeanScale(new Vector3(1, 1, 1), 1f).setEaseInExpo();
        LeanTween.value(newRoundLabel.gameObject, 0f, 1f, 1f)
            .setOnUpdate((float value) => {
                newRoundLabel.alpha = value;
            })
            .setEaseLinear();
        yield return new WaitForSeconds(1f);

        // Shop

        yield return new WaitForSeconds(0.5f);
        upgradeLabel.gameObject.SetActive(true);

        upgradePanel.SetActive(true);
        foreach (Transform child in upgradePanel.transform) {
            child.gameObject.SetActive(false);
        }

        bool canPress = false;
        GameRound.UpgradeTypes upgradeType = GameRound.UpgradeTypes.None;

        foreach (Transform child in upgradePanel.transform) {
            yield return new WaitForSeconds(0.1f);

            GameRound.UpgradeTypes type = GameRound.UpgradeTypes.None;
            switch (child.name) {
                case "Vitality": type = GameRound.UpgradeTypes.Vitality; break;
                case "FasterHands": type = GameRound.UpgradeTypes.FastHands; break;
                case "Spunky": type = GameRound.UpgradeTypes.Spunky; break;
            }

            child.gameObject.SetActive(true);
            child.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (!canPress) return;
                upgradeType = type;
            });
        }
        canPress = true;

        yield return new WaitUntil(() => upgradeType != GameRound.UpgradeTypes.None);
        GameRound.instance.Upgrade(upgradeType);

        foreach (Transform child in upgradePanel.transform) {
            child.gameObject.SetActive(false);
        }
        upgradeLabel.gameObject.SetActive(false);
        upgradePanel.SetActive(false);

        // Fade out
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
        bestScoreLabel.text = "Best: " + game.bestScore.ToString();
    }
}
