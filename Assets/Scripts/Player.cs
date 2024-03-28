using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    [SerializeField] private InputAction joystick;
    [SerializeField] private Button interact;

    [SerializeField] private float movementSpeed = 2f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;

    private bool _isInteractable;
    private Interactable _interactObject;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        var character = Resources.Load<GameObject>(PlayerPrefs.GetString("PlayerPrefabs"));
        var instance = Instantiate(character, gameObject.transform);
        instance.transform.localScale = new Vector3(4, 4, 4);

        _animator = instance.GetComponent<Animator>();
        _sprite = instance.GetComponent<SpriteRenderer>();

        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerCoordinateX"), PlayerPrefs.GetFloat("PlayerCoordinateY"), 0);
        
        interact.onClick.AddListener(Interact);
    }

    private void OnEnable()
    {
        joystick.Enable();
        interact.interactable = true;
    }

    private void OnDisable()
    {
        joystick.Disable();
    }

    private void FixedUpdate()
    {
        interact.interactable = !IsInteracting() && (_isInteractable && _interactObject is not null);
        
        var move = joystick.ReadValue<Vector2>();
        if (IsInteracting())
        {
            move = Vector2.zero;
        }
        if (move.x != 0)
        {
            _sprite.flipX = move.x < 0;
        }

        _animator.SetBool("walk", move is not { y: 0f, x: 0f });
        _rigidbody.velocity = move * movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "InteractableObject") return;

        _isInteractable = true;
        _interactObject = col.GetComponent<Interactable>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "InteractableObject")
        {
            _isInteractable = true;
        }

        var ourPosition = gameObject.GetComponent<BoxCollider2D>().transform.position.y;
        var position = col.gameObject.GetComponent<BoxCollider2D>().transform.position.y;
        if (position > ourPosition)
        {
            _sprite.sortingOrder = col.GetComponentInChildren<SpriteRenderer>().sortingOrder + 2;
        }
        else if (position <= ourPosition)
        {
            _sprite.sortingOrder = col.GetComponentInChildren<SpriteRenderer>().sortingOrder - 2;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "InteractableObject") return;

        _isInteractable = false;
        if (_interactObject == col.GetComponent<Interactable>())
        {
            _interactObject = null;
        }
    }

    private void Interact()
    {
        if (IsInteracting()) return;
        
        PlayerPrefs.SetFloat("PlayerCoordinateX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerCoordinateY", transform.position.y);
        
        interact.interactable = false;
        _animator.SetTrigger("interact");
        _interactObject.Interact();
    }

    private bool IsInteracting()
    {
        return _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Interact");
    }
}