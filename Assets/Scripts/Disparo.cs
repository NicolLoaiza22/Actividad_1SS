using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DisparoE : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Vector3 direccion;
    private ObjectPool<DisparoE> myPool;

    //crear variable de otra script "Alt+Enter, Encapsular campo y seguir usandolo Mayúscula al principio externo"
    public ObjectPool<DisparoE> MyPool { get => myPool; set => myPool = value; }

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= 4)
        {
            timer = 0;
            myPool.Release(this);
        }
    }
}
