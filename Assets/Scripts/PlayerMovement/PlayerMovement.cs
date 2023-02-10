using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //�ϐ�
    [HideInInspector] public float moveSpeed;
    public float walkSpeed = 4f;
    public float sprintSpeed = 6f;
    public float aimSpeed = 3f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public float jumpCoolDown = 0.2f;
    /*[HideInInspector]*/ public int playerHP = 100, maxPlayerHP;
    private bool isGround;
    private bool readyToJump;
    private CapsuleCollider capCol;

    //���C���[�}�X�N
    public LayerMask whatIsGround;

    //�x�N�g��
    Vector3 velocity;

    //�Q��
    public CharacterController controller;
    public GunController gunCtrl;
    public Transform groundCheck;
    private Rigidbody rb;
    public Slider hpBer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Update�ȉ��̊֐�
        SpeedChange();

        //�O���E���h�`�F�b�N
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        controller.Move(move * moveSpeed * Time.deltaTime);

        //�W�����v
        if (Input.GetKey(KeyCode.Space) && isGround && readyToJump)
        {
            readyToJump = false;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Invoke("ResetJump", jumpCoolDown);
        }

        //�d��
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    //�W�����v�̃N�[���_�E������
    private void ResetJump()
    {
        readyToJump = true;
    }

    //�X�s�[�h�ω�&�A�j���[�V����
    private void SpeedChange()
    {
        if (isGround && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && !gunCtrl.shooting)
        {
            moveSpeed = sprintSpeed;
            gunCtrl.anim.SetBool("run", true);
        }
        else if (isGround && !gunCtrl.aiming)
        {
            moveSpeed = walkSpeed;
            gunCtrl.anim.SetBool("run", false);
        }
        else if (gunCtrl.aiming)
        {
            moveSpeed = aimSpeed;
        }
        else
        {
            gunCtrl.anim.SetBool("run", false);
        }
    }

    //HP�Ǘ�
    public void TakeHit(float damage)
    {
        playerHP = (int)Mathf.Clamp(playerHP - damage, 0, playerHP);
        hpBer.value = playerHP;

        if (playerHP <= 0 && !GManager.instance.gameOver)
        {
            GManager.instance.gameOver = true;
        }
    }
}
