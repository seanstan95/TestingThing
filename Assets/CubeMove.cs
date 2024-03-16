using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeMove : MonoBehaviour
{
    public GameObject pauseText, light;
    public TMP_Text countdownText, scoreText;
    private float timer = 0f;
    private bool countdownActive = false;
    private int score = 0;

    // Update is called once per frame
    void Update()
    {
        light.transform.Rotate(.1f, 0f, 0f); //camera rotate

        float speed = 20f;
        float horizMove = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float vertMove = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.position += new Vector3(horizMove, 0, vertMove);

        if(Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseText.SetActive(true);
            Debug.Log("game paused");
        } else if(Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseText.SetActive(false);
            Debug.Log("game unpaused");
        }

        if(Input.GetKeyDown(KeyCode.Escape) || score == 5)
        {
            countdownActive = true;
        }

        if(countdownActive)
        {
            if(timer < 3)
            {
                timer += Time.deltaTime; //adds the appropriate amount of time based on framerate
                //Debug.Log(timer);
                countdownText.text = "Shutting down in: " + (int)(3 - timer);
            } else
            {
                //Application.Quit(); won't work when testing in unity editor
                UnityEditor.EditorApplication.isPlaying = false; //works when testing in unity editor
            }
        }


        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.position += new Vector3(-speed, 0, 0);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.position += new Vector3(speed, 0, 0);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.position += new Vector3(0, 0, -speed);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.position += new Vector3(0, 0, speed);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger!");
        //Debug.Log("touched " + other.gameObject.name);
        float newX = Random.Range(-22, 22);
        float newZ = Random.Range(-22, 22);
        other.transform.position = new Vector3(newX, 2.5f, newZ);
        score++;
        scoreText.text = "Score: " + score;
    }
}
