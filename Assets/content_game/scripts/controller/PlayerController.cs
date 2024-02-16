using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;

namespace BerTaDEV
{
    public class PlayerController : MonoBehaviour
    {
        public float directionSpeed = 5f;
        public float wayDistance = 2.5f;
        public float jumpForce = 10f;
        public float gravity = -9.81f;
        public float superLand = -25.0f;
        public LayerMask groundLayers;
        [Space]
        public CinemachineVirtualCamera cmVirtual;
        public bool thirdPerson;
        [Space]
        [ReadOnlyInspector] public CinemachineCameraOffset cmOffset;
        [ReadOnlyInspector] public int currentWayIndex = 1;
        [ReadOnlyInspector] public bool isGrounded;
        [ReadOnlyInspector] public bool isSliding;
        [ReadOnlyInspector] public CharacterController controller;
        [ReadOnlyInspector] public Animator animator;
        [ReadOnlyInspector] public Vector3 velocity;
        
        Vector3 awakeCenter;
        float awakeHeight;
        Vector3 m_cmOffset;
        public static PlayerController singleton;
        private void Awake()
        {
            singleton = this;
            cmOffset = cmVirtual.GetComponent<CinemachineCameraOffset>();
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();

            awakeCenter = controller.center;
            awakeHeight = controller.height;
        }
        void Update()
        {
            SetCinemachineOffset();
            if (!GameManager.singleton.isGame) return;
            Movement();
        }
        void Movement()
        {

            isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayers);
            animator.SetBool("isGrounded", isGrounded);

            if (isGrounded && velocity.y < 0) // -gravity engelleme
            {
                velocity.y = -2f;
            }

            if (SwipeManager.swipeRight && currentWayIndex < 2 || Input.GetKeyDown(KeyCode.D) && currentWayIndex < 2) // saða kaydýrma
            {
                currentWayIndex++;
                if (!isSliding)
                    animator.CrossFade("ForwardsRight", 0.25f);
            }
            else if (SwipeManager.swipeLeft && currentWayIndex > 0 || Input.GetKeyDown(KeyCode.A) && currentWayIndex > 0) // sola kaydýrma
            {
                currentWayIndex--;
                if (!isSliding)
                    animator.CrossFade("ForwardsLeft", 0.25f);
            }

            if (SwipeManager.swipeUp && isGrounded || Input.GetKeyDown(KeyCode.W) && isGrounded) // Jump
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
                animator.Play("Jump");
            }
            if (SwipeManager.swipeDown || Input.GetKeyDown(KeyCode.S))
            {
                if (isGrounded) // kayma
                {
                    StopCoroutine(SlideEvent());
                    StartCoroutine(SlideEvent());
                }
                else // havadayken yere sert iniþ
                {
                    velocity.y -= superLand;
                    StopCoroutine(SlideEvent());
                    StartCoroutine(SlideEvent());
                }
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            MoveToLane(currentWayIndex);
        }
        void MoveToLane(int way)
        {
            Vector3 targetPosition = transform.position;
            targetPosition.x = (way - 1) * wayDistance;
            controller.Move((targetPosition - transform.position) * directionSpeed * Time.deltaTime);
        }
        IEnumerator SlideEvent()
        {
            isSliding = true;
            thirdPerson = true;
            animator.Play("Falling To Roll");
            controller.height = 0.5f;
            controller.center = new Vector3(0.0f, 0.5f, 0.0f);
            yield return new WaitForSeconds(1);
            isSliding = false;
            thirdPerson = false;
            controller.height = awakeHeight;
            controller.center = awakeCenter;
        }
        void SetCinemachineOffset()
        {
            float _x = thirdPerson && currentWayIndex == 0 ? -0.4f : thirdPerson && currentWayIndex == 2 ? 0.4f : 0.0f;
            float _z = thirdPerson ? -4.0f : 0.0f;
            m_cmOffset = new Vector3(_x, 0.0f, _z);
            cmOffset.m_Offset = Vector3.Lerp(cmOffset.m_Offset, m_cmOffset, 10 * Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            var tileMove = other.GetComponentInParent<TileMove>();
            if (tileMove)
            {
                tileMove.onTriggerEnterCharacter();
            }
            if (other.CompareTag("Obstacle"))
            {
                StopCoroutine(SlideEvent());
                GameManager.singleton.FailGame();
            }
            if (other.CompareTag("Coin"))
            {
                other.gameObject.SetActive(false);
                GameManager.singleton.AddCoin(1);
            }
        }
    }
}

