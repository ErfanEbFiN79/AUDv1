using System;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class PlayerController : MonoBehaviour
{
    #region Variables

    public static PlayerController instance;
    
    [Header("See and Rotate")] 
    [SerializeField] private Transform viewPoint;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private bool invertLook;
    [SerializeField] private float minLock;
    [SerializeField] private float maxLock;
    private Camera cam;
    private float verticalRotStore;
    private Vector2 mouseInput;
   
    
    [Header("Move")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private MoveType moveType;
    [SerializeField] private CharacterController cR;
    
    private Vector3 moveDir, movment;
    private float activeMoveSpeed;
    private enum MoveType
    {
        Transform,
        CharacterController
    }
    
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityMod;

    [Header("Attack 1 (Electric)")]
    [SerializeField] private GameObject electObject;
    [SerializeField] private float timeBtwElectShot;
    
    [Header("Attack 2 (Fire")]
    [SerializeField] private GameObject fireObject;
    [SerializeField] private float timeBtwFireShot;
    
    private float timeBtwShotSet;
    private float timrBtwShotSet2;

    [Header("Change weapon")] 
    [SerializeField] private GameObject[] fxGunHand1;
    [SerializeField] private GameObject[] fxGunHand2;
    [SerializeField] private GameObject[] gunsHand1;
    [SerializeField] private GameObject[] gunsHand2;
    [SerializeField] private Gun[] gunsHand1G;
    [SerializeField] private Gun[] gunsHand2G;
    private int _selectorGun1;
    private int _selectorGun2;
    private int _selectSwitchGun;
 
    [Header("Change Weapon Ui")]
    [SerializeField] private GameObject panelChangeWeapon;
    private int _selectChanger = 0;

    [Header("Hp")] 
    [SerializeField] private float hp;
    #endregion  

    #region Unity Methods

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // lock camera
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
        cR = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Time and Data
        PlayerPrefs.SetFloat("TimePlay", PlayerPrefs.GetFloat("TimePlay") + 1 *Time.deltaTime);
        PlayerPrefs.SetFloat("PlayerPoseX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPoseY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPoseZ", transform.position.z);
        // Get data
        GetData();
            
        // Rotate
        RotatePlayer();
        RotateGun();
        
        // Move
        ManageMove();
        
        //Gravity
        ManageGravity();
        
        // Mouse
        FreeMouse();
        
        // Shot
        ManageShot();
        
        // Manage gun
        ManageSelectedGun();
        ManageSelectChangeGun();
        ManageUiChangeGun();
    }
    
    #endregion

    #region Data

    private void GetData()
    {
        // Rotate left and right
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        
        // Rotate up and down
        verticalRotStore += mouseInput.y;
        verticalRotStore = Mathf.Clamp(verticalRotStore, minLock,  maxLock);
        
        //Move
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        float yVel = movment.y;
        movment = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized;
        movment.y = yVel;
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = runSpeed;
        }
        else
        {
            activeMoveSpeed = moveSpeed;
        }
    }

    #endregion
    
    #region Manage

    private void ManageMove()
    {
        switch (moveType)
        {
            case MoveType.Transform:
                Move();
                break;

            case MoveType.CharacterController:
                MoveCharacterController();
                break;
        }
        
    }

    private void ManageGravity()
    {
        if (cR.isGrounded)
        {
            Jump();
        }
        
        movment.y += Physics.gravity.y * Time.deltaTime * gravityMod;
    }
    
    private void FreeMouse()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Cursor.lockState == CursorLockMode.None)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void ManageShot()
    {
        if (Input.GetMouseButton(1))
        {
            Shot();
        }

        if (Input.GetMouseButton(0))
        {
            Shot2();
        }
    }


    #endregion

    private void ManageSelectedGun()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (_selectSwitchGun == 0)
            {
                _selectorGun1++;
                
                if (_selectorGun1 >= gunsHand1.Length)
                {
                    _selectorGun1 = 0;
                }
            }
            else
            {
                _selectorGun2++;
                
                if (_selectorGun2 >= gunsHand2.Length)
                {
                    _selectorGun2 = 0;
                }
            }

            ActionSelectedGun();
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (_selectSwitchGun == 0)
            {
                _selectorGun1--;
                if (_selectorGun1 < 0)
                {
                    _selectorGun1 = gunsHand1.Length - 1;
                }
            }
            else
            {
                _selectorGun2--;
                if (_selectorGun2 < 0)
                {
                    _selectorGun2 = gunsHand2.Length - 1;
                }
            }
            ActionSelectedGun();
        }
    }

    private void ManageSelectChangeGun()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (_selectSwitchGun == 0)
            {
                _selectSwitchGun = 1;
            }
            else
            {
                _selectSwitchGun = 0;
            }
        }
    }

    private void ManageUiChangeGun()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if (UiManager.instance.changeHappen)
            {
                _selectorGun1 = UiManager.instance.codeGunH1;
                _selectorGun2 = UiManager.instance.codeGunH2;
                ActionSelectedGun();
            }

            Cursor.lockState = CursorLockMode.None;
            panelChangeWeapon.SetActive(true);
            
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            UiManager.instance.changeHappen = false;
            Cursor.lockState = CursorLockMode.Locked;
            panelChangeWeapon.SetActive(false);
        }
    }

    #region Actions

    
    private void RotatePlayer()
    {
        var rotation = transform.rotation;
        rotation = Quaternion.Euler(
            rotation.eulerAngles.x,
            rotation.eulerAngles.y + mouseInput.x,
            rotation.eulerAngles.z
        );
        transform.rotation = rotation;
        
    }

    private void RotateGun()
    {
        if (!invertLook)
        {
            viewPoint.rotation = Quaternion.Euler(
                verticalRotStore,
                viewPoint.rotation.eulerAngles.y,
                viewPoint.rotation.eulerAngles.z
                );
        }
        else
        {
            viewPoint.rotation = Quaternion.Euler(
                -verticalRotStore,
                viewPoint.rotation.eulerAngles.y,
                viewPoint.rotation.eulerAngles.z
                );
        }
        
    }
    
    private void Move()
    {
        transform.position += movment * activeMoveSpeed * Time.deltaTime;
    }

    private void MoveCharacterController()
    {
        cR.Move(movment * activeMoveSpeed * Time.deltaTime);
    }
    
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            movment.y = jumpForce;
        }
        
    }

    private void Shot()
    {
        timeBtwShotSet += Time.deltaTime;
        if (timeBtwShotSet >= gunsHand1G[_selectorGun1].TimeBtwShotGet)
        {
            Ray ray = cam.ViewportPointToRay(
                new Vector3(
                    0.5f,
                    0.5f,
                    0
                    )
                );

            ray.origin = cam.transform.position;
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Instantiate(
                    fxGunHand1[_selectorGun1],
                    hit.point,
                    quaternion.identity
                );

                if (hit.collider.CompareTag("Enemy"))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject != null)
                        {
                            if (hit.collider.gameObject.GetComponent<HpController>() != null)
                            {
                                hit.collider.gameObject.GetComponent<HpController>().HpSet(true, 
                                    gunsHand1G[_selectorGun1].DamageGet);
                            }
                        }
                    }

                }
            }

            timeBtwShotSet = 0;
        }
    }

    private void Shot2()
    {
        timrBtwShotSet2 += Time.deltaTime;
        if (timrBtwShotSet2 >= gunsHand2G[_selectorGun2].TimeBtwShotGet)
        {
            Ray ray = cam.ViewportPointToRay(
                new Vector3(
                    0.5f,    
                    0.5f,
                    0
                )
            );

            ray.origin = cam.transform.position;
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Instantiate(
                    fxGunHand2[_selectorGun2],
                    hit.point,
                    quaternion.identity
                );

                if (hit.collider.CompareTag("Enemy"))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject != null)
                        {
                            if (hit.collider.gameObject.GetComponent<HpController>() != null)
                            {
                                hit.collider.gameObject.GetComponent<HpController>().HpSet(true, 
                                    gunsHand2G[_selectorGun2].DamageGet);
                            }
                        }
                    }
                }
            }

            timrBtwShotSet2 = 0;
        }
    }

    private void ActionSelectedGun()
    {
        foreach (GameObject obj in gunsHand1)
        {
            obj.SetActive(false);
        }
        
        foreach (GameObject obj in gunsHand2)
        {
            obj.SetActive(false);
        }
        
        gunsHand1[_selectorGun1].SetActive(true);
        gunsHand2[_selectorGun2].SetActive(true);
    }
    #endregion

    #region Hp Manager

    public void HpSystem(bool increase, float value) => HpSystemAction(increase, value);

    private void HpSystemAction(bool increase, float value)
    {
        if (increase)
        {
            hp += value;
        }
        else
        {
            hp -= value;
        }
    }

    #endregion

    #region Detecter

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            print("we touch wall");
            PlayerPrefs.SetInt("WallTouch", PlayerPrefs.GetInt("WallTouch") + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            print("we touch wall");
            PlayerPrefs.SetInt("WallTouch", PlayerPrefs.GetInt("WallTouch") + 1);
        }
    }

    #endregion
    
}
