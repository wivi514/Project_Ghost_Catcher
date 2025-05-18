using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float lookRotationSpeed = 30f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //Gèle les rotation X et Z dans le cas ou elle n'ont pas été cocher dans rigidbody
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void Move(float xMoveAmt, float yMoveAmt, Camera camera)
    {
        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        LookRotation(cameraForward);

        Vector3 moveDirection = cameraForward.normalized * yMoveAmt + cameraRight.normalized * xMoveAmt;

        rb.linearVelocity = new Vector3(moveDirection.x * walkSpeed, rb.linearVelocity.y, moveDirection.z * walkSpeed);
    }

    private void LookRotation(Vector3 cameraForward)
    {
        if (cameraForward.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookRotationSpeed);
        }
    }
}
