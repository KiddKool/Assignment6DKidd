using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;

    private int score = 0;
    private int lives = 3;
    public Text prizeText;
    public Text lifeText;
    private void Start()
    {
        agent.updateRotation = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        if (agent.remainingDistance > agent.stoppingDistance) // if character hasn't reached destination...
        {
            character.Move(agent.desiredVelocity, false, false); // move character
        }
        else // if character has reached destination...
        {
            character.Move(Vector3.zero, false, false); // stop moving character
        }

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Prize")
        {
            other.gameObject.SetActive(false);
            score += 10;
            prizeText.text = "Score: " + score;

        }
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Life Lost");
            lives -= 1;
            lifeText.text = "Lives: " + lives;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
}
