using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_Controller : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _totalClicks;

    [UsedImplicitly]
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void UpdateScore(int amount)
    {
        var counter = amount.ToString();
        _totalClicks.text = "Total click  " + counter;
    }
}
