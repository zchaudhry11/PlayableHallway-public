using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGhostIK : MonoBehaviour
{
    public float ikWeightRight = 1;
    public float ikWeightLeft = 1;

    public float ikTransitionSpeed = 1.0f;

    public Transform leftHandIKTarget;
    public Transform rightHandIKTarget;
    public Transform leftFootIKTarget;
    public Transform rightFootIKTarget;

    private bool disabledIKLeft = true;

    private Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        // Set IK Targets
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeightRight);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKTarget.position);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeightLeft);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKTarget.position);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ikWeightLeft);
        anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootIKTarget.position);

        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, ikWeightLeft);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootIKTarget.position);

        // Set IK Rotations
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeightRight);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandIKTarget.rotation);

        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeightLeft);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandIKTarget.rotation);

        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, ikWeightLeft);
        anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootIKTarget.rotation);

        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, ikWeightLeft);
        anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootIKTarget.rotation);
    }
}