using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using HighScore;
using System.Reflection.Emit;

public class Scores : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset m_VisualTreeAsset;
    [SerializeField] private StyleSheet m_StyleSheet;

    private Button btnMenu;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.styleSheets.Add(m_StyleSheet);

        var title = new UnityEngine.UIElements.Label("HIGH SCORES");
        title.AddToClassList("title");
        root.Add(title);

        var highScoreData = HighScoreManager.LoadHighScores();
        List<HighScoreManager.HighScoreEntry> highScores = highScoreData.highScores;

        int rank = 1;
        foreach (var entry in highScores)
        {
            var scoreRow = new VisualElement();
            scoreRow.AddToClassList("score-row");

            var rankLabel = new UnityEngine.UIElements.Label(rank.ToString() + ".");
            rankLabel.AddToClassList("rank");

            var playerName = new UnityEngine.UIElements.Label(entry.playerName);
            playerName.AddToClassList("player-name");

            var score = new UnityEngine.UIElements.Label(entry.score.ToString());
            score.AddToClassList("player-score");

            scoreRow.Add(rankLabel);
            scoreRow.Add(playerName);
            scoreRow.Add(score);

            root.Add(scoreRow);

            rank++;
        }
    }

    private void Awake()
    {
        btnMenu = GetComponent<UIDocument>().rootVisualElement.Query(className: "btn").AtIndex(0) as Button;
        btnMenu.RegisterCallback<ClickEvent>(OnClickMenu);
    }

    private void OnClickMenu(ClickEvent click)
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
