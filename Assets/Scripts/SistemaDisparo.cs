using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SistemaDisparo : MonoBehaviour
{
    [SerializeField] private DisparoE disparoPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int numeroDisparos;
    private ObjectPool<DisparoE> pool;
    private float ratioDisparo;
    private float timer;

    private void Awake() //dar prioridad
    {
        pool = new ObjectPool<DisparoE>(CrearDisparo, null, ReleaseDisparo, DestroyDisparo);
    }

    private DisparoE CrearDisparo()
    {
        DisparoE disparoCopia = Instantiate(disparoPrefab, transform.position, Quaternion.identity);
        disparoCopia.MyPool = pool;
        return disparoCopia;
    }



    private void ReleaseDisparo(DisparoE disparo)
    {
        disparo.gameObject.SetActive(false);
    }

    private void DestroyDisparo(DisparoE disparo)
    {
        Destroy(disparo.gameObject);
    }




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))            
        {            
            //disparo normal
            for (int i = 0; i < 2; i++)
            {
                //Estoy pidiendo a la piscina que me de un nuevo disparo.
                   DisparoE disparoCopia = pool.Get();
                   disparoCopia.gameObject.SetActive(true);
                   disparoCopia.transform.position = spawnPoints[i].transform.position;
                   disparoCopia.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && timer > ratioDisparo)
        {
            StartCoroutine(Esprial());
            timer = 3;
            ratioDisparo = 10;
        }        
    }
    private IEnumerator Esprial()
    {
        float gradosPorDisparo = 360 / numeroDisparos;        

        for (float j = 0; j < 360; j += gradosPorDisparo)
            {
                DisparoE disparoCopia = pool.Get();
                disparoCopia.gameObject.SetActive(true);
                disparoCopia.transform.position = transform.position;
                disparoCopia.transform.eulerAngles = new Vector3(0f, 0f, j);
                yield return new WaitForSeconds(0.1f);
            }
    }

}
