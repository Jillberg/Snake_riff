using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundry;
    CinemachineConfiner confiner;
    [SerializeField] float DistanceToTeleport=2f;
    [SerializeField] Direction direction;

    enum Direction { Down,Left,Right,Up};

    private void Awake()
    {
        confiner=FindObjectOfType<CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("hej");
            confiner.m_BoundingShape2D = mapBoundry;
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += DistanceToTeleport;
                break;
            case Direction.Down:
                newPos.y -= DistanceToTeleport;
                break;
            case Direction.Left:
                newPos.x -= DistanceToTeleport;
                break;
            case Direction.Right:
                newPos.x += DistanceToTeleport;
                break;
        }

        player.transform.position = newPos;
    }
}
