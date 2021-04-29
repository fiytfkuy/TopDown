using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private AudioSource speaker;
    public float speed = 10;
    public float bulletLifespan = 1;
    public int playerHealth = 3;
    public GameObject bullet;
    public float bulletspeed = 15;
    public AudioClip shootSoundEffect;
    public AudioClip punchSoundEffect;
    public AudioClip pickupSoundEffect;

    private bool canShoot = true;
    public float shootCooldownTime;
    private float timeDifference;
    public bool invincible = false;
    public float invincibleCooldownTime;
    private float timeDifference2;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        speaker = GetComponent<AudioSource>();
        playerHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            transform.SetPositionAndRotation(new Vector2(), new Quaternion());
            playerHealth = 3;
        }

        Vector2 velocity = myRB.velocity;

        velocity.x = Input.GetAxisRaw("Horizontal") * speed;
        velocity.y = Input.GetAxisRaw("Vertical") * speed;

        myRB.velocity = velocity;

        if (canShoot)
        {


            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                GameObject b = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletspeed);

                speaker.clip = shootSoundEffect;
                speaker.Play();

                Destroy(b, bulletLifespan);

                canShoot = false;
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                GameObject b = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletspeed);

                speaker.clip = shootSoundEffect;
                speaker.Play();

                Destroy(b, bulletLifespan);

                canShoot = false;
            }

            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                GameObject b = Instantiate(bullet, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletspeed, 0);

                speaker.clip = shootSoundEffect;
                speaker.Play();

                Debug.Log(b.GetComponent<Rigidbody2D>().velocity);

                Destroy(b, bulletLifespan);

                canShoot = false;
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GameObject b = Instantiate(bullet, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletspeed, 0);

                speaker.clip = shootSoundEffect;
                speaker.Play();

                Destroy(b, bulletLifespan);

                canShoot = false;
            }
        }

        if(canShoot == false)
        {
            timeDifference += Time.deltaTime;

            Debug.Log(timeDifference);

            if (timeDifference >= shootCooldownTime)
            {
                canShoot = true;
                timeDifference = 0;

            }

        }

        if (invincible == true)
        {
            timeDifference2 += Time.deltaTime;

            Debug.Log(timeDifference2);

            if (timeDifference >= invincibleCooldownTime)
            {
                invincible = false;
                timeDifference2 = 0;
            }
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "EnemySprite")
        {
            //This also means playerHealth = playerHealth - 1;

            speaker.clip = punchSoundEffect;
            speaker.Play();

            playerHealth--;
        }

        else if ((collision.gameObject.name == "Pickup") && (playerHealth < 3))
        {

            playerHealth++;

            speaker.clip = pickupSoundEffect;
            speaker.Play();

            collision.gameObject.SetActive(false);
        }
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("enemy"))
        {
            GameObject.Find("EnemySprite").GetComponent<EnemyMovement>().canMove = true;
            GameObject.Find("EnemySprite").GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            Destroy(collision.gameObject);
        }
    }
}
