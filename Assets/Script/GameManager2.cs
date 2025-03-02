using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance { get; private set; }
    public bool isGameActive;
    [SerializeField] private bool isPaused, didLose;    
    private ObstacleSpawner obstacleSpawner;
    private float timeToWin;
    private bool juegoIniciado;
    [SerializeField] private float countdownTime;

    [Header("Prefabs (Opcional)")]
    [SerializeField] private GameObject redPrefab;
    [SerializeField] private GameObject greenPrefab;
    [SerializeField] private GameObject red;
    [SerializeField] private GameObject green;        
    [SerializeField] private string allowedScene = "orange";
    public TextMeshProUGUI GameOverText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento de cambio de escena
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        isGameActive = true;
        FindGameObjects(); // Buscar o instanciar objetos
        red.SetActive(false);
        green.SetActive(false);
        juegoIniciado=true;
        
        if (SceneManager.GetActiveScene().name != allowedScene)
        {
            Debug.Log("Este script no se ejecuta en esta escena.");
            enabled = false; // Desactiva el script
        }
        
    }

    private void Update()
    {
         if (juegoIniciado) 
        {
            timeToWin += Time.deltaTime;
        }

        if (timeToWin >= countdownTime)
        {
            GameOverLose(true);
            timeToWin = 0;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindGameObjects(); // Buscar o instanciar objetos en la nueva escena
    }

    public void GameOver()
    {
        if (GameOverText != null)
        {
            GameOverText.gameObject.SetActive(true);
        }
        isGameActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Bad"))
        {
            GameOver();         
        }

        Destroy(gameObject);
    }  

    

        public void GameOverLose(bool lose)
    {
        if (lose)
        {            
            if (red != null)
            {
                Time.timeScale = 0;                
                red.SetActive(true);
                Debug.Log("Red activado correctamente.");
            }
            else
            {
                Debug.LogError("Red no está asignado en el Inspector.");
            }

            StartCoroutine(ReloadScene());
            
        }
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSecondsRealtime(5f);
            if (red.activeSelf)
            {
                red.SetActive(false);
            }
            else if (green.activeSelf)
            {
                green.SetActive(false);
            }                
        SceneManager.LoadScene("menu");
        Time.timeScale = 1; // Restaurar tiempo después de recargar
    }

   
    private void FindGameObjects()
    {
        obstacleSpawner = FindAnyObjectByType<ObstacleSpawner>();

        // Buscar en la escena actual
        red = GameObject.Find("Red");
        green = GameObject.Find("Green");

        // Si no se encuentran, instanciarlos
        if (red == null && redPrefab != null)
        {
            red = InstantiatePrefab(redPrefab, "Red");
        }

        if (green == null && greenPrefab != null)
        {
            green = InstantiatePrefab(greenPrefab, "Green");
        }
    }

    private GameObject InstantiatePrefab(GameObject prefab, string name)
    {
        if (prefab != null)
        {
            GameObject obj = Instantiate(prefab);
            obj.name = name;
            obj.SetActive(false); // Se activará solo cuando sea necesario
            return obj;
        }
        Debug.LogError($"El prefab {name} no ha sido asignado en el Inspector.");
        return null;
    }
    
    public void GameOverWin(bool didWin)
    {
        if (didWin)
        {
            if (obstacleSpawner != null)
                obstacleSpawner.canSpawn = false;

            Time.timeScale = 0; // Pausa el juego

            if (green != null)
            {
                
                green.SetActive(true);
                Debug.Log("Green activado correctamente.");
            }
            else
            {
                Debug.LogError("Green no ha sido asignado en el Inspector.");
            }
            StartCoroutine(ReloadScene());
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desuscribirse del evento para evitar errores
    }

    public void StartGame( bool start )
    {
        if(start){
        countdownTime=10;    
        Time.timeScale = 1; // Inicia el tiempo del juego
        Countdown.Instance.StartCountdown(50); 
        juegoIniciado=true;
        }
    }

     

}

