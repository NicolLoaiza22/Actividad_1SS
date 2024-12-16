using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    public void JuegoSS(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
