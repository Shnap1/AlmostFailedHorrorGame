using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using YG;

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

    }



    void UpdateHealthUI(int playerHealth, int playerHealthMax)
    {
        healthSlider.value = playerHealth;
        healthSlider.maxValue = playerHealthMax;
    }


}
