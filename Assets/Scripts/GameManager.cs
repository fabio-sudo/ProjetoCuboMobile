using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Obstaculo Settings")]
    [SerializeField] private GameObject obstacle;
    public float timeSpawn = 2f;
    public bool gameOver = false;
    [Range(0f, 20f)]
    [SerializeField]private float xSpawn = 7f;
    [Range(0f, 20f)]
    [SerializeField] private float ySpawn = 11f;
    [Range(0f, 20f)]
    [SerializeField] private float speedFalling = 2f;//Velocidade de queda
    [Range(0f, 20f)]
    [SerializeField] private int numberSpawn = 4;//Quantidade de objstaculos a serem gerados
    [Range(0f, 10f)]
    [SerializeField] private float speedRotation = 0.5f;//Velocidade de rotação
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        while (!gameOver)
        {
            var obstacleToSpawn = Random.Range(0, numberSpawn); //Gerar um número aleatório entre 0 e 2

            for (int i = 0; i < obstacleToSpawn; i++)
            {

                //Posição aleatória em X
                float x = Random.Range(-xSpawn, xSpawn);

                //Instanciar o Obstaculo
                GameObject objObstacle = Instantiate(
                    obstacle,
                    new Vector3(x, ySpawn, 0f),
                    Quaternion.identity);

                //Velocidade Aleatória de queda
                float damping = Random.Range(0f, speedFalling);

                Rigidbody rb =
                    objObstacle.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.linearDamping = damping;

                    rb.AddTorque(
                        new Vector3(
                            Random.Range(-speedRotation, speedRotation),
                            Random.Range(-speedRotation, speedRotation),
                            Random.Range(-speedRotation, speedRotation)
                        ),
                        ForceMode.Impulse
                        );
                }

            }
            yield return new WaitForSeconds(timeSpawn);
        }
    }

    public void GameOver()
    {
        gameOver = true;
    }
}
