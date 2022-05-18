using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Player player;

    void Start()
    {
        player.CameraToPlayerWrap += MoveCameraToPlayer;
    }

    void MoveCameraToPlayer(Vector3 PlayerPosition)
    {
        PlayerPosition.z = this.transform.position.z;
        transform.SetPositionAndRotation(PlayerPosition, transform.rotation);
    }
}
