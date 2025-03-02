using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    
    public static Countdown Instance { get; private set; }

    public TextMeshProUGUI timeCounter;
    [SerializeField] private float countdownTime;
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
            hurryup.SetActive(false);
        }
    }

    
    }

    public void StartCountdown(float num)
    {
        hurryup.SetActive(false);
        gameStarted = true;
        countdownTime=num;
       // timeCounter.gameObject.SetActive(true);
    }
}