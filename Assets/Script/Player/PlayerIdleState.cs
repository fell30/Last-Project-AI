using UnityEngine;
using UnityEngine.Playables;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController player) : base(player) { }

    public override void UpdateState()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(h, 0, v);

        if (inputDir.magnitude > 0)
        {
            player.SwitchState(player.runState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        {
            player.SwitchState(player.jumpState);
        }
    }
}
