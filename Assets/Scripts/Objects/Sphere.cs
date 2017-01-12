using UnityEngine;

namespace outdated
{
    public class Sphere : MonoBehaviour
    {

        private Vector3 initialPosition;
        private Quaternion initialRotation;
        private Rigidbody2D body;

        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }

        void GameReset()
        {
            body.velocity = Vector2.zero;
            body.angularVelocity = 0f;
            transform.rotation = initialRotation;
            transform.position = initialPosition;
        }

        void OnEnable()
        {
            GameEventManager.GameReset += GameReset;
        }

        void OnDisable()
        {
            GameEventManager.GameReset -= GameReset;
        }
    }
}
