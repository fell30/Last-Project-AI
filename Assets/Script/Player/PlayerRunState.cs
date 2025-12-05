using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(PlayerController player) : base(player) { }

    public override void UpdateState()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(h, 0, v);

        if (inputDir.magnitude == 0)
        {
            player.SwitchState(player.idleState);
            return;
        }

        //Camera Direction
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = camForward * v + camRight * h;
        moveDir.Normalize();

        // Move
        player.Move(moveDir);

        // Rotate player mengikuti movement
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRot, 10f * Time.deltaTime);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        {
            player.SwitchState(player.jumpState);
        }
    }
}
