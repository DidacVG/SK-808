using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Velocidades {Quieto = 0, Lento = 1, Normal = 2, Rapido = 3 };
public class Movimiento : MonoBehaviour
{
    public Velocidades VelocidadActual;
    float[] Velocidad = {0f, 8.6f, 10.4f, 12.96f};
    // Start is called before the first frame update
    public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundMask;
    public bool Grounded;
    public int Saltos;
    public float Gravedad = 12.41067f;
    public float FuerzaSalto = 26.6581f;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Velocidad[(int)VelocidadActual] * Time.deltaTime;

        if (rb.velocity.y < -24.2f)
        {
            rb.velocity = new Vector2(rb.velocity.x, -24.2f);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (Grounded == true)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * FuerzaSalto, ForceMode2D.Impulse);
            }
            else
            {
                if(Saltos < 1)
                {
                    Saltos += 1;
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.up * FuerzaSalto, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Grounded = true;
        Saltos = 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Grounded = false;
    }
}