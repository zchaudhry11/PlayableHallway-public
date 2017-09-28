using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandling : MonoBehaviour
{
    public float ikWeightRight = 1;
    public float ikWeightLeft = 0;

    public float ikTransitionSpeed = 1.0f;

    public Transform leftIKTarget;
    public Vector3 leftIKTargetPos;
    public Transform rightIKTarget;

    private bool disabledIKLeft = true;

    private Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (disabledIKLeft)
        {
            if (ikWeightLeft > 0)
            {
                ikWeightLeft -= (Time.deltaTime * ikTransitionSpeed); // Move hand back to rest
            }
        }
        else
        {
            if (ikWeightLeft < 1)
            {
                ikWeightLeft += (Time.deltaTime * ikTransitionSpeed / 2); // Move hand towards object
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        // Set IK Targets
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeightRight);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightIKTarget.position);

        // Set IK Rotations
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeightRight);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightIKTarget.rotation);

        // Set left hand IK
        leftIKTarget.position = leftIKTargetPos;

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeightLeft);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftIKTarget.position);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeightLeft);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftIKTarget.rotation);
    }

    /// <summary>
    /// Enables or disables the left hand's IK.
    /// </summary>
    /// <param name="status">Flag that determines if left hand IK is used or not.</param>
    public void SetIKLeftStatus(bool status)
    {
        disabledIKLeft = status;
    }
}