using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Velocidades {Quieto = 0, Plataforma = 1, Lento = 2, Normal = 3, Rapido = 4};
public class Movimiento : MonoBehaviour
{
    public Velocidades VelocidadActual;
    float[] Velocidad = { 0f, 8f, 8.6f, 10.4f, 12.96f };
    // Start is called before the first frame update
    public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundMask;
    public bool Grounded;
    public int Saltos;
    public float Gravedad = 12.41067f;
    public float FuerzaSalto = 26.6581f;
    public bool Checkpoint = false;

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
                if (Saltos < 1)
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
        if (collision.gameObject.tag == "Finish")
        {
            transform.position += Vector3.right * 0 * Time.deltaTime;
        }
        if (collision.gameObject.tag == "Respawn")
        {
            Checkpoint = true;
        }
        if (collision.gameObject.tag == "Pincho")
        {
            if (Checkpoint == true)
            {
                SceneManager.LoadScene("Juego 1");
            }
            else
            {
                SceneManager.LoadScene("Juego");
            }
        }
        if (collision.gameObject.tag == "Vacio")
        {
            if (Checkpoint == true)
            {
                SceneManager.LoadScene("Juego 1");
            }
            else
            {
                SceneManager.LoadScene("Juego");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Grounded = false;
    }
}