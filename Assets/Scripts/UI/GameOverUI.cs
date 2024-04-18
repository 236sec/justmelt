using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private GameRound game => GameRound.instance;

    [SerializeField] TextMeshProUGUI titleLabel;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] TextMeshProUGUI bestLabel;
    [SerializeField] Button tryAgainButton;

    public IEnumerator Show() {
        titleLabel.gameObject.SetActive(true);
        scoreLabel.gameObject.SetActive(false);
        bestLabel.gameObject.SetActive(false);
        tryAgainButton.gameObject.SetActive(false);

        titleLabel.color = new Color(1, 1, 1, 0);
        titleLabel.transform.localScale = new Vector3(2, 2, 2);
        titleLabel.transform.LeanScale(new Vector3(1, 1, 1), 1f).setEaseInExpo();
        LeanTween.value(titleLabel.gameObject, 0f, 1f, 1f)
            .setOnUpdate((float value) => {
                titleLabel.alpha = value;
            })
            .setEaseInExpo();
        yield return new WaitForSeconds(1.1f);

        scoreLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        bestLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        tryAgainButton.gameObject.SetActive(true);
        tryAgainButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        });

        yield return new WaitForSeconds(0.1f);

        LeanTween.value(scoreLabel.gameObject, 0, game.score, 1f)
           .setOnUpdate((float value) => {
               scoreLabel.text = "Score: " + Mathf.RoundToInt(value).ToString(); 
           })
           .setEaseLinear();
        yield return new WaitForSeconds(1f);
        LeanTween.value(bestLabel.gameObject, 0, game.bestScore, 1f)
           .setOnUpdate((float value) => {
               bestLabel.text = "Best: " + Mathf.RoundToInt(value).ToString();
           })
           .setEaseLinear();
    }
}
