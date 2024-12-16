using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private TextMeshProUGUI textoOleadas;
    // Start is called before the first frame update
    void Start()
    {        
        StartCoroutine(SpawnearEnemigos());
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    IEnumerator SpawnearEnemigos()
    {
        for (int h = 0; h < 5; h++) //Niveles
        {            
            for (int i = 0; i < 3; i++) //Oleadas
            {
                textoOleadas.text = "Nivel " + (h + 1) + " - " + "Oleada " + (i + 1);
                yield return new WaitForSeconds(3f);
                textoOleadas.text = "";
                for (int j = 0; j < 10; j++) //Enemigos
                {
                    Vector3 puntoAleatorio = new Vector3(transform.position.x, Random.Range(-4.5f, 4.5f), 0);
                    Instantiate(enemigoPrefab, puntoAleatorio, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);

                }
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
