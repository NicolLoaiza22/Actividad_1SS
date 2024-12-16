using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuReinicio : MonoBehaviour
{
    public GameObject pauseReinicio;

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        pauseReinicio.SetActive(false); // Oculta el menú de pausa
    }

    public void MenuInicial(string nombre)
    {
        SceneManager.LoadScene(nombre);
        Time.timeScale = 1f;
    }

    public void Salir()
    {
        Application.Quit();
    }
}

