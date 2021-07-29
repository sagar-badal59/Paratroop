using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterControl : MonoBehaviour {

    public float speed = 0f;
    private Rigidbody2D rigidBody;
    //private Animator myAnimator;
    public GameObject launch;
    GameObject spawnerReference;

    // Variables for releasing package
    float rand;
    float toReleasePackage;
    bool releasePackage;
    int rand1;
    bool packageReleased;

    //// Variables for releasing bomb
    //float bomberRandomVar; 
    //bool ifBomber;
    //float whereToReleaseBomb;
    //bool bombReleased;
    //public GameObject bombPrefab;
    //public GameObject bombInstance;

    public static float fracPara = 0.5f;
    //public static float fracBomb = 0.1f;

    public bool comesFromLeft;


	// Use this for initialization
	void Start () {
        speed = 5f;

        //myAnimator = gameObject.GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        rand1 = Random.Range(0, 2);
        if(rand1 == 0)
            rand = Random.Range(0.2f, 0.35f);
        else
            rand = Random.Range(0.65f, 0.8f);
        toReleasePackage = Random.Range(0f, 1f);

        releasePackage = false;
        if (toReleasePackage < fracPara)
            releasePackage = true;

        //bomberRandomVar = Random.Range(0f, 1f);
        //if (bomberRandomVar < fracBomb)
        //{
        //    ifBomber = true;
        //    bombReleased = false;
        //    //myAnimator.SetBool("ifBomber", true);
        //    whereToReleaseBomb = Random.Range(0.25f, 0.75f);

        //}
        //else
        //    ifBomber = false;
	}

    void assignDirection(bool a)
    {
        comesFromLeft = a;
        SpriteRenderer tempSpriteRenderer = GetComponent<SpriteRenderer>();
        if (!comesFromLeft)
        {
            tempSpriteRenderer.flipX = false;
        }
        else
        {
            tempSpriteRenderer.flipX = true;
        }
            
            
    }
	
	// Update is called once per frame
	void Update () {

        if(comesFromLeft)
            rigidBody.velocity = new Vector2(speed, 0);
        else
            rigidBody.velocity = new Vector2(-speed, 0);
        // Calculate threshold point
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * rand, 0, 0));

        // Script for releasing paratrooper

        if (releasePackage)
        {
            if ((rigidBody.transform.position.x >= point.x) & !packageReleased & comesFromLeft)
            {
                Debug.Log("Comes From Left");
                GameObject newLaunch = (GameObject) Instantiate(launch, new Vector3(rigidBody.transform.position.x, rigidBody.transform.position.y - 0.5f, rigidBody.transform.position.z), Quaternion.identity);
                //Sprite Rendering
                SpriteRenderer m_SpriteRenderer = newLaunch.GetComponent<SpriteRenderer>();
                if (rand >= 0.65f)
                    m_SpriteRenderer.flipX = false;
                Physics2D.IgnoreCollision(newLaunch.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                packageReleased = true;
            }
            if ((rigidBody.transform.position.x <= point.x) & !packageReleased & !comesFromLeft)
            {
                Debug.Log("Comes From Right");
                GameObject newLaunch = (GameObject)Instantiate(launch, new Vector3(rigidBody.transform.position.x, rigidBody.transform.position.y - 0.5f, rigidBody.transform.position.z), Quaternion.identity);
                //Sprite Rendering
                SpriteRenderer m_SpriteRenderer = newLaunch.GetComponent<SpriteRenderer>();
                if (rand >= 0.65f)
                    m_SpriteRenderer.flipX = true;
                Physics2D.IgnoreCollision(newLaunch.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                packageReleased = true;
            }
        }
        //if(ifBomber)
        //{
        //    Debug.Log("Drop Bomb");
        //    point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * whereToReleaseBomb, 0, 0));
        //    if ((rigidBody.transform.position.x >= point.x) & !bombReleased & comesFromLeft)
        //    {
        //        bombInstance = (GameObject)Instantiate(bombPrefab, new Vector3(rigidBody.transform.position.x, rigidBody.transform.position.y - 0.5f, rigidBody.transform.position.z), Quaternion.identity);
        //        bombReleased = true;
        //    }
        //    if ((rigidBody.transform.position.x <= point.x) & !bombReleased & !comesFromLeft)
        //    {
        //        bombInstance = (GameObject)Instantiate(bombPrefab, new Vector3(rigidBody.transform.position.x, rigidBody.transform.position.y - 0.5f, rigidBody.transform.position.z), Quaternion.identity);
        //        bombReleased = true;
        //    }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Paratrooper(Clone)")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (collision.gameObject.name == "Heliocopter(Clone)")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == "anyBullet")
        {
            Destroy(collision.gameObject);

            StartCoroutine(CollisionFunction());
            //Destroy(gameObject);
        }
    }


    IEnumerator CollisionFunction()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
