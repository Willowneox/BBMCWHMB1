using UnityEngine;

public class SpatulaController : MonoBehaviour
{
    public float pressedY = -4f;
    public float releasedY = 2f;
    public float movespeed = 10f;

    private Vector3 targetPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPos,
            Time.deltaTime * movespeed);

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            targetPos = new Vector3(transform.localPosition.x, pressedY, transform.localPosition.z);
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.X))
        {
            targetPos = new Vector3(transform.localPosition.x, releasedY, transform.localPosition.z);
        }
    }
}
