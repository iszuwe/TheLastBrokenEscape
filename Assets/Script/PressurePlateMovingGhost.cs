using UnityEngine;

public class PressurePlateMovingGhost : MonoBehaviour
{
    public GameObject ghost;
    public Transform destination;  // Where the ghost should move to
    public float moveSpeed = 3f;

    private bool triggered = false;
    private bool moving = false;

    [Header("Audio")]
    public AudioSource doorSound;

    void Start()
    {
        ghost.SetActive(false);  // Hide at start
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            ghost.SetActive(true); // Appear
            moving = true;

            doorSound.Play();
            // Optional: Play sound
            //ghost.GetComponent<AudioSource>()?.Play();
        }
    }

    void Update()
    {
        if (moving && ghost != null && destination != null)
        {
            ghost.transform.position = Vector3.MoveTowards(
                ghost.transform.position,
                destination.position,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(ghost.transform.position, destination.position) < 0.5f)
            {
                ghost.SetActive(false);
                moving = false;
            }

        }
    }
}
