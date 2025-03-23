using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private float speed = 5;
    [SerializeField] private float rotateSpeed = 500;

    private Quaternion targetRotation;
    private Animator animator;
    private Rigidbody rigidbody;
    private NavMeshAgent navMeshAgent;

    [HideInInspector]
    public static bool isPauseDialogShow = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();
        if (cameraController == null)
            cameraController = Camera.main.GetComponent<CameraController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //open pause dialog
            if (pauseDialog != null)
            {    
                pauseDialog.SetActive(!pauseDialog.activeSelf);
                Time.timeScale = pauseDialog.activeSelf ? 0 : 1;
                Cursor.visible = pauseDialog.activeSelf ? true : false;
                Cursor.lockState = CursorLockMode.None;
                isPauseDialogShow = pauseDialog.activeSelf;
            }
        }
        
            
        if(pauseDialog.activeSelf)
            return;
            
        Time.timeScale = 1;
        
        float h = Input.GetAxis("Horizontal");
        
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Clamp(Mathf.Abs(h) + Mathf.Abs(v), 0, 1);

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Calllllllllllllllll");
            rigidbody.AddForce(new Vector3(0, 600, 0));

        }

        var moveInput = (new Vector3(h, 0, v)).normalized;
        var moveDirection = cameraController.GetDirection * moveInput;

        if (moveAmount > 0)
        {
            transform.position += moveDirection * Time.deltaTime * speed;
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

    
    }
    
    [SerializeField] private Text labelScore;
    [SerializeField]
     private GameObject pauseDialog = null;
     
    [HideInInspector]
    public static int itemCount = 3 ;
    
    

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Item")
        {   
            //increase item count
            itemCount++;
            labelScore.text = "Score : " + itemCount.ToString();
            Debug.Log("Score : " + itemCount);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            //decrease health bar
            itemCount--;
            labelScore.text = "Score : " + itemCount.ToString();
            Debug.Log("Score : " + itemCount);
            
            
        }
    }
}
