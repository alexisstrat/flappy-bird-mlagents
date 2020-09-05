using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform endPos;
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.World);

        ReachedEnd();
    }

    /// <summary>
    /// Check if the pipe is on end position.
    /// If yes deactivate it
    /// </summary>
    private void ReachedEnd()
    {
        if (transform.position.x >= endPos.position.x)
        {
            gameObject.SetActive(false);
        }
    }
}