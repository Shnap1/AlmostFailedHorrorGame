using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using YG;
//using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] int maxHealth;
    [SerializeField] Text currentGameStateText;
    GameLoopManager.GameState currentGameState;
    int number_of_TARGETS_to_collect;
    int number_of_TARGETS_collected;
    public GameLoopManager gameLoopManager;

    enum Languages { en, ru, tr }
    Languages curLanguage;


    private void OnEnable()
    {
        HealthCounter.onPlayerHealthChanged += UpdateHealthUI;
        // GameLoopManager.OnGameUpdate += UpdateStateText;
        GameLoopManager.onTargetCollected += GetGameState;
        //UpdateHealthUI()
        // YandexGame.GetDataEvent += GetData;
    }
    private void OnDisable()
    {
        HealthCounter.onPlayerHealthChanged += UpdateHealthUI;
        // GameLoopManager.OnGameUpdate -= UpdateStateText;
        GameLoopManager.onTargetCollected -= GetGameState;
        // YandexGame.GetDataEvent -= GetData;
    }
    private void Start()
    {
        // UpdateStateText(GameLoopManager.currentGameState);
        curLanguage = Languages.ru;
        testTranslate();
    }

    void testTranslate()
    {
        if (YandexGame.SDKEnabled && YandexGame.LangEnable())
        {
            if (YandexGame.lang == "ru") curLanguage = Languages.ru;
            if (YandexGame.lang == "tr") curLanguage = Languages.tr;
            if (YandexGame.lang == "en") curLanguage = Languages.en;
            Debug.Log("testTranslate " + curLanguage);
        }

    }

    public void GetData()
    {
        while (!YandexGame.SDKEnabled)
        {
            curLanguage = Languages.en;

            // int currentLevel = (PlayerPrefs.GetInt("CurrentLevel", 0) + 1);
            switch (YandexGame.EnvironmentData.language)
            {
                case "en":
                    curLanguage = Languages.en;
                    break;
                case "ru":
                    curLanguage = Languages.ru;
                    break;
                case "tr":
                    curLanguage = Languages.tr;
                    break;
                default:
                    curLanguage = Languages.en;
                    break;
            }

        }
        //UpdateStateText(currentGameState, number_of_TARGETS_to_collect, number_of_TARGETS_collected);
    }
    void GetGameState(GameLoopManager.GameState gameState, int number_of_TARGETS_to_collect, int number_of_TARGETS_collected)
    {
        this.currentGameState = gameState;
        this.number_of_TARGETS_to_collect = number_of_TARGETS_to_collect;
        this.number_of_TARGETS_collected = number_of_TARGETS_collected;

        UpdateStateText(currentGameState, number_of_TARGETS_to_collect, number_of_TARGETS_collected);
    }

    void UpdateStateText(GameLoopManager.GameState gameState, int number_of_TARGETS_to_collect, int number_of_TARGETS_collected)
    {
        //currentGameStateText.text = gameState.ToString();
        if (curLanguage == 0) curLanguage = Languages.ru;

        switch (gameState)
        {
            case GameLoopManager.GameState.GatesOpen:

                if (curLanguage == Languages.en)
                {
                    currentGameStateText.text = "Go through the gate to collect all the artifacts that are highlighted with a green arrow.";
                }
                else if (curLanguage == Languages.ru)
                {
                    currentGameStateText.text = "Перейди через ворота, чтобы собрать все артефакты, которые подсвечены зелёной стрелочкой.";
                }
                else if (curLanguage == Languages.tr)
                {
                    currentGameStateText.text = "Yeşil bir okla vurgulanan tüm eserleri toplamak için kapıdan geçin.";
                }
                break;

            case GameLoopManager.GameState.GameStart:
                if (curLanguage == Languages.en)
                {
                    currentGameStateText.text = $"{gameLoopManager.current_number_of_TARGETS_collected} artifacts out of {gameLoopManager.number_of_TARGETS_to_collect} collected. Collect all artifacts";
                }
                else if (curLanguage == Languages.ru)
                {
                    currentGameStateText.text = $"{gameLoopManager.current_number_of_TARGETS_collected} артефактов из {gameLoopManager.number_of_TARGETS_to_collect} собрано. Собери все цели";
                }
                else if (curLanguage == Languages.tr)
                {
                    currentGameStateText.text = $"{gameLoopManager.current_number_of_TARGETS_collected} eserden {gameLoopManager.number_of_TARGETS_to_collect}'u toplandı. Tüm eserleri toplayın";
                }
                break;
            case GameLoopManager.GameState.LootCollected:
                if (curLanguage == Languages.en)
                {
                    currentGameStateText.text = "All artifacts collected. RUN BACK TO THE GATE!";
                }
                else if (curLanguage == Languages.ru)
                {
                    currentGameStateText.text = "Все артефакты собраны. БЕГИ НАЗАД К ВОРОТАМ!";
                }
                else if (curLanguage == Languages.tr)
                {
                    currentGameStateText.text = "Tüm hedefler toplandı. KAPIYA GERI KOŞUN!";
                }
                break;
            case GameLoopManager.GameState.Lobby:
                currentGameStateText.text = "You are now in the lobby. Go to the gates to start the game";
                break;
            default:
                currentGameStateText.text = "";

                break;
        }
    }

    void UpdateHealthUI(int playerHealth, int playerHealthMax)
    {
        healthSlider.value = playerHealth;
        healthSlider.maxValue = playerHealthMax;
    }

    //private void Start()
    //{
    //    healthSlider = GetComponentInChildren<Slider>();
    //    UpdateHealthUI(100,100);
    //}
}
