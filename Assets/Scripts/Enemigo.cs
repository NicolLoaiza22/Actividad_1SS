using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject disparoPrefab;
    [SerializeField] private GameObject spawnPoint;    
    [SerializeField] private AudioClip explotarEnemy;
    [SerializeField] private GameObject efectoExplosion;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnearDisparos());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidad * Time.deltaTime);
    }
    IEnumerator SpawnearDisparos() //disparo automático
    {
        while (true)
        {
            Instantiate(disparoPrefab, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DisparoPlayer"))
        {
            Instantiate(efectoExplosion,transform.position, Quaternion.identity);
            Destroy(elOtro.gameObject);
            ControladorSonido.Instance.EjecutarSonido(explotarEnemy);
            Destroy(this.gameObject);
        }
    }
}
