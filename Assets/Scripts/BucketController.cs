using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;
    public static event PlayerDelegate OnPlayerGainLife;
    public static event PlayerDelegate OnPlayerLoseLife;
    public static event PlayerDelegate OnPlayerLoseScore;

    public delegate void GameDelegate();
    public static event GameDelegate collided;


    public float speed = 0.1f;
    public Vector3 startPos;

    GameManager game;

    // Use this for initialization
    void Start()
    {
        
        game = GameManager.Instance;

    }
    void OnEnable()
    {
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.GameOver == true)
        {
            speed = ChangeSpeed();
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 position = this.transform.position;
            if (position.x <= -2.5)
            {
                //edge
            }
            else
            {
                position.x -= speed;
                this.transform.position = position;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 position = this.transform.position;
            if (position.x >= 2.5)
            {
                //edge
            }
            else
            {
                position.x += speed;
                this.transform.position = position;
            }

        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "point")
        {
            OnPlayerScored();
            StartCoroutine(hide(col.gameObject));

        }
        else if (col.gameObject.tag == "life")
        {
            OnPlayerGainLife();
            StartCoroutine(hide(col.gameObject));


        }
        else if (col.gameObject.tag == "bug")
        {
            OnPlayerLoseLife();
            StartCoroutine(hide(col.gameObject));


        }
        else if (col.gameObject.tag == "coffee")
        {
            speed = ChangeSpeed();
            StartCoroutine(hide(col.gameObject));

        }
        else if (col.gameObject.tag == "sand-watch")
        {
            GameObject.Find("Environment/one").GetComponent<Parallaxer>().shiftSpeed = GameObject.Find("Environment/one").GetComponent<Parallaxer>().shiftSpeed/2.0f;
            GameObject.Find("Environment/zero").GetComponent<Parallaxer>().shiftSpeed = GameObject.Find("Environment/zero").GetComponent<Parallaxer>().shiftSpeed / 2.0f;
            GameObject.Find("Environment/bug").GetComponent<Parallaxer>().shiftSpeed = GameObject.Find("Environment/bug").GetComponent<Parallaxer>().shiftSpeed / 2.0f;
            GameObject.Find("Environment/coffee").GetComponent<Parallaxer>().shiftSpeed = GameObject.Find("Environment/coffee").GetComponent<Parallaxer>().shiftSpeed / 2.0f;
            GameObject.Find("Environment/life").GetComponent<Parallaxer>().shiftSpeed = GameObject.Find("Environment/life").GetComponent<Parallaxer>().shiftSpeed/2.0f;
            GameObject.Find("Environment/sand-watch").GetComponent<Parallaxer>().shiftSpeed = GameObject.Find("Environment/sand-watch").GetComponent<Parallaxer>().shiftSpeed / 2.0f;
            GameObject.Find("Environment/wrench").GetComponent<Parallaxer>().shiftSpeed = GameObject.Find("Environment/wrench").GetComponent<Parallaxer>().shiftSpeed / 2.0f;
            StartCoroutine(hide(col.gameObject));

        }
        else if (col.gameObject.tag == "no-point")
        {
            OnPlayerLoseScore();
            StartCoroutine(hide(col.gameObject));

        }
    }

    float ChangeSpeed()
    {
        if (System.Math.Abs(speed - 0.1f) <= 0)
        {
            return 0.5f;
        }
        else
        {
            return 0.1f;
        }
    }

    IEnumerator hide(GameObject gameObj)
    {
        gameObj.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        gameObj.GetComponent<SpriteRenderer>().enabled = true;

    }
}
