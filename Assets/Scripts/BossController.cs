using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public enum bossStates { Idle, Rugido, Roll, Spines, Jump, Walk, Hit, Death}

    [SerializeField] private bossStates state;
    private Animator animator;
    public bool waiting;
    private Transform player;
    private Rigidbody2D rb;
    [Header("Stats")]
    [SerializeField] float knockBackForce;
    [SerializeField] int damage;
    public float life;
    public float maxLife;
    [Header("Movimiento")]
    [SerializeField] float stoppingDistance;
    [SerializeField] float speed;
    [Header("Roar Attack")]
    [SerializeField] private Transform roarSpawnPoint;
    [SerializeField] private GameObject roarProyectil;
    [SerializeField] float roarProyectilSpeed;
    [SerializeField] float roarAttackTime;
    [Header("Roll Attack")]
    [SerializeField] float rollSpeed;
    bool chocado;
    bool isRolling;
    [Header("Spike Attack")]
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] float spikeSpeed;
    //bool isHit;
    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpSpeed = 1;
    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        state = bossStates.Idle;
        animator = GetComponent<Animator>();
        waiting = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        ChangeState(bossStates.Idle);
    }

    void CheckDirection()
    {
        //Sirve para que mire al player cada vez que haga un ataque
        Vector2 distanciaVector = player.position - transform.position;
        Vector2 direccion = distanciaVector.normalized;
        if (direccion.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (direccion.x < 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    public void ChangeState(bossStates _state)
    {
        state = _state;
        CheckDirection();
        switch(state)
        {
                case bossStates.Idle:
                //nada, hacer la animacion de idle
                StartCoroutine(Idle());
                break;
                
            case bossStates.Rugido:
                //animacion de rugido
                //lance algo
                //tendremos que saer cuando termina para cambiar el estado
                StartCoroutine(Roar());
            break;
               
            case bossStates.Roll:
                //hacer animacion de rodar
                //moverlo mientras rueda
                //cuando impacte contra la pared termina el estado
                StartCoroutine(Roll());
            break;
                
            case bossStates.Spines:
                //animacion de spines
                //instanciamos las espinas
                //que se quede unos segundos
                //despues de ese tiempo termina el estado
                StartCoroutine(Spines());
            break;
                
            case bossStates.Jump:
                //animacion de jump
                //desplazar al enemigo a otro sitio
                //termina el estado cuando toque suelo 
                StartCoroutine(Jump());
            break;
                
            case bossStates.Walk:
                //animacion andar
                //mover hacia el jugador
                //a cierta distancia del player termina el estado
                StartCoroutine(Walk());
            break;

                
            case bossStates.Death:
                    //animacion de muerte
                    //desactivar el controlador y que se quede muerto

            break;

                
            default:

            break;
        }
    }

    IEnumerator Idle()
    {
        while (waiting == true)
        {
            yield return null;
        }

        ChangeState(bossStates.Walk);
    }

    IEnumerator Walk()
    {
        animator.SetBool("Walk", true);
        Vector2 distanciaVector = player.position - transform.position;
        Vector2 direccion = distanciaVector.normalized;
        float distancia = distanciaVector.magnitude;
        while(distancia > stoppingDistance)
        {

            rb.velocity = new Vector2(speed * direccion.x, 0);
            CheckDirection();
            distanciaVector = player.position - transform.position;
            direccion = distanciaVector.normalized;
            distancia = distanciaVector.magnitude;
            yield return null;
        }
        rb.velocity = Vector2.zero;
        animator.SetBool("Walk", false);
        int azar = Random.Range(1, 4);
        ChangeState((bossStates)azar);
    }

   IEnumerator Roar()
    {
        //animacion de rugido
        animator.SetTrigger("Roar");
        //tendremos que saer cuando termina para cambiar el estado
        yield return new WaitForSeconds(roarAttackTime);

        //Para Debug
        int azar = Random.Range(1, 6);
        ChangeState((bossStates)azar);
    }

    public void ShotRoarProyectil()
    {
        GameObject clone = Instantiate(roarProyectil, roarSpawnPoint.position, roarSpawnPoint.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * -1 * roarProyectilSpeed);
    }

    IEnumerator Roll()
    {
        animator.SetTrigger("Roll");
        chocado = false;
        yield return new WaitForSeconds(1.2f);
        isRolling = true;
        while(chocado == false)
        {
            yield return null;
            rb.velocity = transform.right * -1 * rollSpeed;
        }
        isRolling = false;
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(1.36f, 0.89f);
        yield return new WaitForSeconds(1);
        int azar = Random.Range(1, 6);
        ChangeState((bossStates)azar);

    }

    public void StartRoll()
    {
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.88f, 0.88f);
    }

    IEnumerator Spines()
    {
        animator.SetTrigger("spike");
        gameObject.layer = 7;
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.82f, 0.73f);
        float timePass = 0;
        while (timePass<4)  //&& isHit == false
        {
            yield return null;
            timePass += Time.deltaTime;
        }
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(1.36f, 0.89f);

        animator.SetTrigger("NotTired");
        yield return new WaitForSeconds(1);
    }

    public void LaunchSpines()
    {
        for(int i = 0; i<spawnPoints.Length; i++) 
        {
            GameObject cloneSpike = Instantiate(spikePrefab, spawnPoints[i].position, spawnPoints[i].rotation);
            cloneSpike.GetComponent<Rigidbody2D>().AddForce(cloneSpike.transform.right * -1 * spikeSpeed);
        }
    }

    IEnumerator Jump()
    {
        animator.SetBool("Jump", true);
        animator.SetTrigger("TakeOff");
        yield return new WaitForSeconds(0.4f);
        isJumping = true;
        Vector3 playerPos = player.position;
        Vector3 bossPos = transform.position;
        rb.AddForce(Vector2.up * jumpForce);
        float t = 0;
        while(t < 1 || isJumping == true)
        {
            t += Time.deltaTime * jumpSpeed;
            float x = Mathf.Lerp(bossPos.x, playerPos.x, t);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            yield return null;
        }


        yield return new WaitForSeconds(1);
        int azar = Random.Range(1, 6);
        ChangeState((bossStates)azar);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isRolling == true)
        {
            if (collision.gameObject.tag == "Limite")
            {
                rb.velocity = Vector2.zero;
                gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(1.36f, 0.89f);
                animator.SetTrigger("Chocado");
                chocado = true;
                gameObject.layer = 7;
            }
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
                gameObject.layer = 8;
            }
          
        }
        else if (isJumping == true)
        {
            if(collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
                gameObject.layer = 8;
                AddKnockBackForceToPlayer();
            }

            if (collision.gameObject.tag == "ground")
            {
                animator.SetBool("Jump", false);
                isJumping = false;
                gameObject.layer = 7;
            }
        }
    }

    void AddKnockBackForceToPlayer()
    {
        player.GetComponent<Rigidbody2D>().AddForce(transform.right * -1);
    }

    public void TakeDamage(float _damage)
    {
        life -= _damage;
        if(life <=0)
        {
            Death();
        }
        else
        {
            StartCoroutine(HitAnim());
        }
    }

    IEnumerator HitAnim()
    {
        //inicial
        //final
        //t
        Color colorInicial = Color.white;
        Color colorFinal = Color.red;
        float t = 0;
        SpriteRenderer bossSprite = GetComponent<SpriteRenderer>();
        while(t<1)
        {
            bossSprite.color = Color.Lerp(colorInicial, colorFinal, t);
            t += Time.deltaTime * 4; //tarda 0.33 en pasar de color a color, que numerin
            yield return null;
        }
        while (t > 0)
        {
            bossSprite.color = Color.Lerp(colorInicial, colorFinal, t);
            t -= Time.deltaTime * 4; //tarda 0.33 en pasar de color a color, que numerin
            yield return null;
        }
    }

    void Death()
    {
        animator.SetTrigger("Death");
        StopAllCoroutines();
        GetComponent<CapsuleCollider2D>().enabled = false;
        rb.isKinematic = true; //si kinematic esta en true, no le afectan las fuerzas
    }
}
