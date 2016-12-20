using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour
{

    public float zoomSpeed = 1;
    public float targetOrtho;
    public float smoothSpeed = 2.0f;
    public float minOrtho = 1.0f;
    public float maxOrtho = 20.0f;

    public float panSpeed = 3f;

    public float mapX = 61.4f;
    public float mapY = 41.4f;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private float vertExtent;
    private float horzExtent;

    private Camera cam;
    private GameManager manager;

    void Start()
    {
        targetOrtho = Camera.main.orthographicSize;
        cam = GetComponent<Camera>();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (manager.dragging)
            return;

        if (Input.GetMouseButton(0) )
        {
            transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * panSpeed * -1f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * panSpeed * -1f, 0f);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -20, 10f), Mathf.Clamp(transform.position.y, -20f, 10f), transform.position.z);
        }


        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);

    }

}