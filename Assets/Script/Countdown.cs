using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    
    public static Countdown Instance { get; private set; }

    public TextMeshProUGUI timeCounter;
    [SerializeField] private float countdownTime = 40f;
    [SerializeField] private GameObject hurryup;
    private bool gameStarted = false;

    void Awake()
    {
        hurryup.SetActive(false);
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }


    void Update()
    {
    if (gameStarted && Time.timeScale > 0 && countdownTime > 0)
    {
        countdownTime = Mathf.Max(countdownTime - Time.deltaTime, 0); // Evita valores negativos
        int timeRemaining = Mathf.CeilToInt(countdownTime);
        timeCounter.text = "Tiempo restante " + timeRemaining;

        if (countdownTime < 6)
        {
            timeCounter.text = "Hurry up! " + timeRemaining;
            hurryup?.SetActive(countdownTime > 0);
        }

        if (countdownTime <= 0)  // Mejor usar <= para evitar errores
        {
            GameManager2.Instance.GameOverLose(true);
        }
    }

    
    }

    public void StartCountdown()
    {
        gameStarted = true;
        countdownTime=10f;
       // timeCounter.gameObject.SetActive(true);
    }
}