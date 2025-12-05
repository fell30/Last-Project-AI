using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerController player) : base(player) { }

    public override void EnterState()
    {
        player.Jump();
    }

    public override void UpdateState()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Arah kamera
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = camForward * v + camRight * h;
        moveDir.Normalize();

        player.Move(moveDir);

        // Saat mendarat --> state transition normal
        if (player.isGrounded)
        {
            if (moveDir.magnitude > 0)
                player.SwitchState(player.runState);
            else
                player.SwitchState(player.idleState);
        }
    }
}
