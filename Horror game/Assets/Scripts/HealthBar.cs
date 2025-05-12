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
    public Text currentGameStateText;
    GameLoopManager.GameState currentGameState;
    public int number_of_TARGETS_to_collect;
    public int number_of_TARGETS_collected;
    public GameLoopManager gameLoopManager;

    public enum GameLanguages { en, ru, tr }
    public GameLanguages curLanguage;

    public string curLanguageString;


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
        testTranslate();
    }

    void testTranslate()
    {
        if (YandexGame.SDKEnabled && YandexGame.LangEnable())
        {
            if (YandexGame.lang == "ru")
            {
                curLanguage = GameLanguages.ru;
                // Debug.Log("testTranslate() " + curLanguage); 
            }

            if (YandexGame.lang == "tr")
            {
                curLanguage = GameLanguages.tr;
                // Debug.Log("testTranslate() " + curLanguage);
            }
            // Debug.Log("testTranslate() " + curLanguage);

            if (YandexGame.lang == "en")
            {
                curLanguage = GameLanguages.en;
                // Debug.Log("testTranslate() " + curLanguage); 
            }

            // Debug.Log("testTranslate() YandexGame.lang ==  " + YandexGame.lang);
        }

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
        // Debug.Log($"UpdateStateText() gameState = {gameState} + curLanguage =  {curLanguage} ");

        switch (gameState)
        {
            // case GameLoopManager.GameState.SpawnPlayer:

            //     if (curLanguage == GameLanguages.en)
            //     {
            //         currentGameStateText.text = "Go beyond the safe zone.";
            //     }
            //     else if (curLanguage == GameLanguages.ru)
            //     {
            //         currentGameStateText.text = "Выйди за границы безопасной зоны.";
            //     }
            //     else if (curLanguage == GameLanguages.tr)
            //     {
            //         currentGameStateText.text = "Güvenli bölgenin ötesine geçin.";
            //     }
            //     break;

            // case GameLoopManager.GameState.GameStart:
            //     if (curLanguage == GameLanguages.en)
            //     {
            //         currentGameStateText.text = $"{gameLoopManager.current_number_of_TARGETS_collected} of  {gameLoopManager.number_of_TARGETS_to_collect} monsters killed. Kill all monsters.";
            //     }
            //     else if (curLanguage == GameLanguages.ru)
            //     {
            //         currentGameStateText.text = $"{gameLoopManager.current_number_of_TARGETS_collected} из {gameLoopManager.number_of_TARGETS_to_collect} монстров убито. Убей всех монстров.";
            //     }
            //     else if (curLanguage == GameLanguages.tr)
            //     {
            //         currentGameStateText.text = $"Öldürülen {gameLoopManager.number_of_TARGETS_to_collect} canavardan {gameLoopManager.current_number_of_TARGETS_collected}'ı. Bütün canavarları öldür.";
            //     }
            //     break;
            // case GameLoopManager.GameState.LootCollected:
            //     if (curLanguage == GameLanguages.en)
            //     {
            //         currentGameStateText.text = "Mission accomplished. Return to the safe zone.";
            //     }
            //     else if (curLanguage == GameLanguages.ru)
            //     {
            //         currentGameStateText.text = "Задание выполнено. Возвращайтесь в безопасную зону.";
            //     }
            //     else if (curLanguage == GameLanguages.tr)
            //     {
            //         currentGameStateText.text = "Görev tamamlandı. Güvenli bölgeye dönün.";
            //     }
            //     break;
            // case GameLoopManager.GameState.Lobby:
            //     currentGameStateText.text = "You are now in the lobby. Go to the gates to start the game";
            //     break;
            // default:
            //     currentGameStateText.text = "";

            //     break;
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
