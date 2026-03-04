using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ResultUIController : MonoBehaviour
{

    [Header("Fire Text")]
    public TextMeshProUGUI FireRewardText;

    [Header("Human Text")]
    public TextMeshProUGUI RewardText;

    [Header("Bonus Item Text")]
    public TextMeshProUGUI BonusItemText;


    public Image Star1;
    public Image Star2;
    public Image Star3;

    public Sprite filledStar;
    public Sprite emptyStar;

    private LevelResultData resultData;
    private void Start()
    {

        BonusItemText.text = BonusTracker.CollectedCount + "/" + BonusTracker.TotalCount;

        RewardText.text = LevelResultData.humansSaved + "/" + LevelResultData.totalHumans;

        var data = LevelResultCache.Data;

        if (data == null )
        {
            Debug.LogError("No Results");
            return;
        }

        int stars = CalculateStars(data);
        DisplayStars(stars);

        SaveStarProgress(data.levelName, stars);

    }

    int CalculateStars(LevelResultData d)
    {
        int stars = 0;

        //star 1 humans saved
        if (d.humansSaved == d.totalHumans)
            stars++;

        //star 2 fire put out

        if (d.fireExtinguishedPercent >= 0.75f)
            stars++;


        //star 3 bonus collected

        if (d.bonusCollected == d.bonusTotal)
            stars++;

        return stars;
    }

    void DisplayStars(int stars)
    {
        if (Star1 != null) Star1.sprite = stars >= 1 ? filledStar : emptyStar;
        if (Star2 != null) Star2.sprite = stars >= 2 ? filledStar : emptyStar;
        if (Star3 != null) Star3.sprite = stars >= 3 ? filledStar : emptyStar;
    }


    void SaveStarProgress(string levelName, int stars)
    {
        int current = PlayerPrefs.GetInt(levelName + "_Stars", 0);

        if (stars > current)
            PlayerPrefs.SetInt(levelName + "_Stars", stars);

    }
    public void ContinueToFirehouse()
    {
        SceneManager.LoadScene("Firehouse");
    }

    public void RetryLevel()
    {
        if (resultData == null)
        {
            Debug.LogError("No result data found. Retry not possible.");
            return;
        }

        SceneManager.LoadScene(resultData.levelName);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
