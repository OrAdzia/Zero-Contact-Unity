using System.Collections;
using UnityEngine;

public class RocketIdleAnimation : MonoBehaviour
{
    public float moveDistance = 0.5f; // How much to move up and down
    public float moveSpeed = 1.0f;    // Speed of the movement
    public float waitTime = 2.0f;     // Wait time at bottom
    public ParticleSystem thrustParticles; // Add this reference

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(HoverLoop());
    }

    IEnumerator HoverLoop()
    {
        while (true)
        {
            // Turn on particles when moving up
            thrustParticles.Play();
            yield return MoveToPosition(startPosition + Vector3.up * moveDistance);
            thrustParticles.Stop();

            yield return MoveToPosition(startPosition);
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }
}
