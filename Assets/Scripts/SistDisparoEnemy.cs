using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SistDisparoEnemy : MonoBehaviour
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
        StartCoroutine(SpawnearDisparos());
        Debug.Log(spawnPoints.Length); 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;              
    }

    IEnumerator SpawnearDisparos() //disparo automático
    {
        while (true)
        {
            for (int i = 0; i < 1; i++)
            {
                //Estoy pidiendo a la piscina que me de un nuevo disparo.
                DisparoE disparoCopia = pool.Get();
                disparoCopia.gameObject.SetActive(true);
                disparoCopia.transform.position = spawnPoints[i].transform.position;
            }
            yield return new WaitForSeconds(2f);
        }
    }
}