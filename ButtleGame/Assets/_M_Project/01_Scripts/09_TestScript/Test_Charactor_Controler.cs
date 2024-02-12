using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Test_Charactor_Controler : MonoBehaviour
{
    [Header("�\��������J����")]
    public Camera mainCamera;

    // �ړ����x
    public float speed = 3.0f;
    public float moveX;
    public float moveZ;
    public float rotSpeed = 3.0f;

    private Rigidbody rb;
    public float gravityPower = 9.8f;
    public float velo = 5.0f;
    public float jump_y = 0.0f;

    //public bool footHit = false; // �����p�̃R���C�_�[�̔���

    // �A�j���[�V��������
    public enum eMoveDir
    {
        None,
        Forward,
        Back,
        Left,
        Right
    }
    public eMoveDir moveDir;
    public bool isMoving;
    public bool isLockOn = false;  // �t���[�J���������b�N�I���J������
    public GameObject LockOnObject; // ���b�N�I������I�u�W�F�N�g
    public bool isAttack;

    public bool isJumping;
    public bool isSecondJump;

    // Hit Ball
    [Header("�U������𐶐�����ʒu")]
    public GameObject[] attackCollider;
    [Header("�����ݒ�F�U������̍��W")]
    public Transform[] attackCollider_Pos;

    [Header("��������q�b�g����v���n�u")]
    public GameObject damageBall;

    // �U��������ꎞ�I�ɐ�������ϐ�
    private GameObject tmp_DmgBall = null;

    // �e�X�g�p
    [Header("�e�X�g�\��")]
    public Vector3 n_CameraForward = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        // ���W�b�h�{�f�B�̎擾
        rb = this.gameObject.GetComponent<Rigidbody>();

        // �U������̃I�u�W�F�N�g�̔z��̒�`
        attackCollider_Pos = new Transform[attackCollider.Length];

        // �U������̃I�u�W�F�N�g��`
        for(int i = 0; i < attackCollider.Length; i++)
        {
            attackCollider_Pos[i] = attackCollider[i].transform;
        }
    }
    void Update()
    {
        // ���b�N���[�h�̐ݒ�
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (isLockOn == true) isLockOn = false;
            else isLockOn = true;
        }
        this.gameObject.GetComponent<Animator>().SetBool("isLockOn", isLockOn);

        // �v���C���[�̈ړ��A�ړ��A�j���[�W���̎��s
        PlayerMove();

        Gravity();

        this.gameObject.GetComponent<Animator>().SetBool("isJumping", isJumping);
        this.gameObject.GetComponent<Animator>().SetBool("isSecondJump", isSecondJump);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("FootingObject"))
        {
            isJumping = false;
            isSecondJump = false;
        }
    }

    /* Move Function */
    private void PlayerMove()
    {
        // �U�����̂݃X�s�[�h��������
        speed = isAttack ? 1.0f : 3.0f;

        // �v���C���[�̈ړ��F���K���ς�
        //moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;




        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        // �J�����̕������� X-Z���ʂ̒P�ʃx�N�g�����擾
        //Vector3 n_CameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
        n_CameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;

        // �������͂ƃJ�����̌�������ړ�����������
        Vector3 moveForward = n_CameraForward * moveZ + mainCamera.transform.right * moveX;

        // �ړ������ɃX�s�[�h��K������
        rb.velocity = (moveForward * speed + new Vector3(0, jump_y, 0) * Time.deltaTime);

        // �L�����N�^�[�̌�����i�s�����Ɍ�����
        if(moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
            isMoving = true;
        }




        //// �ړ������̍쐬
        //Vector3 n_moveDir = new Vector3(moveX, 0, moveZ);

        //// �A�j���[�V�����p�t���O�̊Ǘ�
        //isMoving = n_moveDir == Vector3.zero ? false : true;

        // �ړ��ʂ����W�ɓK��
        //this.transform.position += n_moveDir;

        // �A�j���[�V�����̐ݒ�
        this.gameObject.GetComponent<Animator>().SetBool("isMoving", isMoving);
        this.gameObject.GetComponent<Animator>().SetBool("isAttack", isAttack);

        if (isLockOn == true)
        {
            moveDir = eMoveDir.None;
            if (moveX > 0) moveDir = eMoveDir.Right;
            if (moveX < 0) moveDir = eMoveDir.Left;
            if (moveZ > 0) moveDir = eMoveDir.Forward;
            if (moveZ < 0) moveDir = eMoveDir.Back;
            this.gameObject.GetComponent<Animator>().SetInteger("moveDir", (int)moveDir);
            // ���ʕ��������b�N�I���I�u�W�F�N�g�ɂȂ�悤�ɉ�]������
            Transform lockDir = LockOnObject.transform;
            this.transform.LookAt(lockDir);
        }

        // �t���[�J�����ł̈ړ�
        if (isLockOn == false)
        {
            // ���ʕ������i�s�����ɂȂ�悤�ɉ�]������
            //transform.forward = Vector3.Slerp(transform.forward, n_moveDir, Time.deltaTime * rotSpeed);
        }
    }
    /* Action Function */
    public void GamePad_Jump(InputAction.CallbackContext context)
    {
        // �{�^���̉����l��臒l���z�������𔻒肷��
        if (isSecondJump == true) return;

        if(isJumping == false || isSecondJump == false)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                Vector3 jump_vec = Vector3.up;
                //Vector3 jump_velo = jump_vec * velo;

                 jump_y = jump_vec.y * velo;

                //rb.velocity = jump_velo;
                if (isJumping == true) isSecondJump = true;
                isJumping = true;
                Debug.Log("�W�����v���܂���");
            }
        }
    }

    private void Gravity()
    {
        if(isJumping == true)
        {
            rb.AddForce(new Vector3(0.0f, -gravityPower, 0.0f));
        }
    }

    /* Attack Function */
    public void GamePad_Attack(InputAction.CallbackContext context)
    {
        // �{�^���̉����l��臒l���z�������𔻒肷��
        if(context.phase == InputActionPhase.Started)
        {
            // �U���A�j���[�V�����ݒ�F�U����
            isAttack = true;
            Debug.Log(context.phase);
            Debug.Log("�U�����܂���");
        }
    }
    public void ResetAttack()
    {   // �U����Ԃ����Z�b�g����
        isAttack = false;
    }

    /* HitBall Function */
    public void CreateDamageBall()
    {
        // �q�b�g�{�[���𐶐�
        tmp_DmgBall = Instantiate(damageBall);

        // �q�b�g�{�[���ɏ��L�҂�o�^�F���ʗp
        tmp_DmgBall.GetComponent<Test_DamageBall_Controler>().SetCreator(this.gameObject);

        // �����蔻��{�[�����q�b�gPos�ɒǏ]������
        tmp_DmgBall.transform.position = attackCollider_Pos[0].transform.position;
        tmp_DmgBall.transform.rotation = attackCollider_Pos[0].transform.rotation;
        tmp_DmgBall.transform.SetParent(attackCollider[0].transform);    // �{�[���̐e��ݒ�(����Ǐ]�p)

    }

    public void DeleteDamageBall()
    {   // �q�b�g�{�[�����폜����
        Destroy(tmp_DmgBall);
        tmp_DmgBall = null;
    }

    /* LegacyFunction */
    //private void LegacyPlayerMove()
    //{
    //    // �����ĂȂ��ꍇ�̏�Ԃ��쐬
    //    Vector3 n_transform = new Vector3(0.0f, 0.0f, 0.0f);
    //    moveDir = eMoveDir.None;
    //    this.gameObject.GetComponent<Animator>().SetBool("isMoving", false);
    //    this.gameObject.GetComponent<Animator>().SetInteger("moveDir", 0);

    //    // �ړ�
    //    if (Input.GetKey(KeyCode.W)) // MoveDir : 1
    //    {
    //        n_transform.z = transform.forward.z;
    //        moveDir = eMoveDir.Forward;
    //    }
    //    if (Input.GetKey(KeyCode.S)) // MoveDir : 2
    //    {
    //        n_transform.z = -transform.forward.z;
    //        moveDir = eMoveDir.Back;
    //    }
    //    if (Input.GetKey(KeyCode.A)) // MoveDir : 3
    //    {
    //        n_transform.x = -transform.right.x;
    //        moveDir = eMoveDir.Left;
    //    }
    //    if (Input.GetKey(KeyCode.D)) // MoveDir : 4
    //    {
    //        n_transform.x = transform.right.x;
    //        moveDir = eMoveDir.Right;
    //    }

    //    // �ړ��ʂ̍ŏI�v�Z
    //    transform.position += n_transform.normalized * speed * Time.fixedDeltaTime;
    //    // ���[�V�����ւ̒l���
    //    isMoving = moveDir == eMoveDir.None ? false : true;
    //    this.gameObject.GetComponent<Animator>().SetBool("isMoving", isMoving);
    //    this.gameObject.GetComponent<Animator>().SetInteger("moveDir", (int)moveDir);

    //}

    //private void InputMoveKey(KeyCode keyCode)
    //{
    //    // �����ĂȂ��ꍇ�̏�Ԃ��쐬
    //    Vector3 n_transform = new Vector3(0.0f, 0.0f, 0.0f);
    //    moveDir = eMoveDir.None;
    //    this.gameObject.GetComponent<Animator>().SetBool("isMoving", false);
    //    this.gameObject.GetComponent<Animator>().SetInteger("moveDir", 0);

    //    // ����
    //    if (keyCode == KeyCode.W) // MoveDir : 1
    //    {
    //        // �ړ��ʂ𐳋K������
    //        n_transform.z = transform.forward.z;

    //        // �ړ����̃��[�V�������Đ�����
    //        this.gameObject.GetComponent<Animator>().SetBool("isMoving", true);
    //        this.gameObject.GetComponent<Animator>().SetInteger("moveDir", 1);
    //    }
    //    if (keyCode == KeyCode.S) // MoveDir : 2
    //    {
    //        n_transform.z = -transform.forward.z;

    //        this.gameObject.GetComponent<Animator>().SetBool("isMoving", true);
    //        this.gameObject.GetComponent<Animator>().SetInteger("moveDir", 2);
    //    }
    //    if (keyCode == KeyCode.A) // MoveDir : 3
    //    {
    //        n_transform.x = -transform.right.x;

    //        this.gameObject.GetComponent<Animator>().SetBool("isMoving", true);
    //        this.gameObject.GetComponent<Animator>().SetInteger("moveDir", 3);
    //    }
    //    if (keyCode == KeyCode.D) // MoveDir : 4
    //    {
    //        n_transform.x = transform.right.x;

    //        this.gameObject.GetComponent<Animator>().SetBool("isMoving", true);
    //        this.gameObject.GetComponent<Animator>().SetInteger("moveDir", 4);
    //    }

    //    // �ړ��ʂ̍ŏI�v�Z
    //    transform.position += n_transform.normalized * speed * Time.deltaTime;
    //}
}
