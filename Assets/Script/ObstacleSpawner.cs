using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{    
    
    [SerializeField] GameObject obstacle;
    public Transform[] spawnPoints;
    public float time;
    public int randomIndex;
    public bool canSpawn;
   

    void Update()
    {
        //Condición para poder iniciar el spawneo
        switch (canSpawn)
        {
            case true:

                //Cuando puede, cuenta el tiempo
                time += Time.deltaTime;
                //Si el tiempo es igual o mayor a 3 segundos
                if(time >= 3f)
                {
                    //Generamos un número aleatorio entre 0 y la canditad de puntos que hemos hecho
                    randomIndex = Random.Range(0,spawnPoints.Length);
                    //Con esta función, aparecemos el objeto en la escena en el punto aleatorio que generamos antes
                    Instantiate(obstacle, spawnPoints[randomIndex].position, Quaternion.identity);
                    //Devolvemos el contador a 0 para repetir el ciclo y limitar la cantidad de objetos que aparecen.
                    time = 0; 
                }

            break;
            
            case false:
             Debug.Log("Stoped");
            break;
    }         
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lose"))
        {
            HandleLose();
        }
    }

    void HandleLose()
    {
        GameManager2 gameManager = FindObjectOfType<GameManager2>();
        
        if (gameManager != null)
        {
            gameManager.GameOverLose(true);
        }
        else
        {
            Debug.LogError("No se encontró el GameManager en la escena.");
        }
    }

}

}
   