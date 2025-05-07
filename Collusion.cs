using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour
{
    public MonoBehaviour[] scriptsToDisable;
    public float fallSpeed = 1f;
    private bool hasCrashed = false;
    private Vector3 crashPos;

    void Update()
    {
        if (hasCrashed)
        {
            crashPos.y -= fallSpeed * Time.deltaTime;
            transform.position = crashPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasCrashed) return;

        if (collision.collider.CompareTag("Obstacle"))
        {
            hasCrashed = true;
            foreach (var script in scriptsToDisable)
            {
                script.enabled = false;
            }
            var rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;
            }
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            var animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }
            crashPos = transform.position;
            Invoke(nameof(RestartScene), 2f);
        }
    }
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
