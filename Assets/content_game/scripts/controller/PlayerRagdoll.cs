using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class PlayerRagdoll : MonoBehaviour
    {
        Rigidbody[] rigidbodies;
        Animator animator;
        CharacterController characterController;
        public static PlayerRagdoll singleton;
        private void Awake()
        {
            singleton = this;
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            rigidbodies = GetComponentsInChildren<Rigidbody>();
        }
        private void Start()
        {
            SetRagdoll(false);
        }
        public void SetRagdoll(bool value)
        {
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i].isKinematic = !value;
            }
            characterController.enabled = !value;
            animator.enabled = !value;
        }
    }
}
