using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayerController : MonoBehaviour
{
    public Player player;
    private InputAction switchAction;
    public float runSpeed = 0.75f;
    void Start()
    {
        player.xControlIsActive = false;
        switchAction = InputSystem.actions.FindAction("Attack");
    }
    private void Update()
    {
        player.MoveX(runSpeed);

        if (switchAction.WasPressedThisFrame())
        {
            player.FlipGravity();

            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.up * player.rb.gravityScale * -1, Mathf.Infinity);
            Debug.DrawRay(player.transform.position, Vector2.up * player.rb.gravityScale * -1, Color.yellow, 5f);
            if (hit)
            {
                Debug.Log(hit.distance);
                player.transform.position = new(hit.distance + player.transform.position.x, player.transform.position.y, player.transform.position.z);
            }
        }
    }
}
