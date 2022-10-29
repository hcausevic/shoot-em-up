using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float _lookAhead;
    private void Update()
    {
        var playerPosition = player.position;
        transform.position = new Vector3(playerPosition.x + _lookAhead, playerPosition.y + 1, transform.position.z);
        _lookAhead = Mathf.Lerp(_lookAhead, aheadDistance * player.localScale.x, Time.deltaTime * cameraSpeed);
    }
}
