using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public float minSpeed = 1f; // the minimum speed for footstep sounds to play
    public float maxSpeed = 10f; // the maximum speed for footstep sounds to play
    public float footstepRate = 1f; // the rate at which footstep sounds will play (in seconds)
    private AudioSource audioSource;
    private float nextFootstepTime = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float speed = GetComponent<Rigidbody>().velocity.magnitude; // get the player's speed
        if (speed > minSpeed && Time.time > nextFootstepTime)
        {
            int clipIndex = Random.Range(0, footstepSounds.Length);
            audioSource.clip = footstepSounds[clipIndex];
            audioSource.Play();
            nextFootstepTime = Time.time + footstepRate / speed; // adjust footstep rate based on player speed
        }
    }
}