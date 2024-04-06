using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Week Display")]
    public TextMeshProUGUI weekText;

    [Header("Resource Display")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI reputationText;

    [Header("Compost Bin Display")]
    public CompostBinUI compostBinUI;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void UpdateWeekDisplay(int currentWeek)
    {
        weekText.text = $"Week: {currentWeek}";
    }

    public void UpdateGameState(int money, int reputation, float compostQuality)
    {
        moneyText.text = $"Money: {money}";
        reputationText.text = $"Reputation: {reputation}";

        // Update the compost bin UI based on the compost quality
        compostBinUI.UpdateCompostQuality(compostQuality);
    }
}