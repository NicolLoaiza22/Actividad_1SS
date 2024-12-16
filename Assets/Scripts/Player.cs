using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float ratioDisparo;
    [SerializeField] private GameObject disparoPrefab;
    [SerializeField] private GameObject SpawnPoint1;
    [SerializeField] private GameObject SpawnPoint2;
    private float vidas = 100;

    [SerializeField] private TextMeshProUGUI textoVidas;
    public GameObject gameOverPanel;
    public GameObject pauseMenu;
    [SerializeField] private AudioClip playerDamage;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private GameObject efectoExplosion;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MasVida());
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        DelimitarMovimiento();
        textoVidas.text = "Vida " + (vidas);

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }
    void Movimiento()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputH, inputV).normalized * velocidad * Time.deltaTime);
    }
    void DelimitarMovimiento()
    {
        float xClamped = Mathf.Clamp(transform.position.x, -8.4f, 8.4f);
        float yClamped = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector3(xClamped, yClamped, 0);
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DisparoEnemy") || elOtro.gameObject.CompareTag("Enemy"))
        {
            vidas -= 20;
            ControladorSonido.Instance.EjecutarSonido(playerDamage);
            Destroy(elOtro.gameObject);
            if (vidas <= 0)
            {
                KillPlayer();
                Debug.Log("Game Over");
                textoVidas.text = "Cero Vidas";
                Instantiate(efectoExplosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator MasVida()
    {
        while (true)
        {
            // Revisa vida menor que 100
            if (vidas < 100)
            {
                // + 5 puntos
                vidas += 5;

            }
            else
            {
                // Vida = 100, detiene aumento

            }

            yield return new WaitForSeconds(3);
        }
    }
    private void KillPlayer()
    {
        // Activar el panel de "Game Over"
        gameOverPanel.SetActive(true);
        ControladorSonido.Instance.EjecutarSonido(gameOver);
    }

    public void PauseGame()
    {
        //Activar el panel de Pausa
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        // Reanuda el tiempo del juego
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }


}
