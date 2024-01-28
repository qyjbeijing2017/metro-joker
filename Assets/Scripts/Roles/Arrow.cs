using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 direction = Vector3.zero;

    public void Show(Vector3 d)
    {
        UpdateDirection(d);

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    private void UpdateDirection(Vector3 d)
    {
        direction = d;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    public void Release(Vector3 d)
    {
        UpdateDirection(d);
        if (direction == Vector3.zero)
        {
            Hide();
        }
    }

    public void Hide()
    {
        direction = Vector3.zero;
        gameObject.SetActive(false);
    }
}