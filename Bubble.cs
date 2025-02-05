using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] float timer;
    [SerializeField] protected float gravityScale;

    private Collider2D bubbleCol;
    private SpriteRenderer bubbleSpriteRend;

    [SerializeField] private GameObject fireEffect;
    [SerializeField] private GameObject waterEffect;
    [SerializeField] private GameObject iceEffect;

	private void Start()
	{
        bubbleCol = this.gameObject.GetComponent<Collider2D>();
        bubbleSpriteRend = this.gameObject.GetComponent<SpriteRenderer>();
	}

	protected BlackBoard blackboard => GameManager.instance.blackBoard;
    protected PlayerController playerController => 
        blackboard.GetValue<PlayerController>("PlayerController",out PlayerController _playerController)?_playerController:null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.Interact(this);
            StartCoroutine(DisableBubbles(this.gameObject));
        }
        if (collision.CompareTag("ground"))
        {
            StartCoroutine(DisableBubbles(this.gameObject));
        }
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.Interact(this);
            StartCoroutine(DisableBubbles(this.gameObject));

        }
        if (collision.gameObject.CompareTag("ground"))
        {
            StartCoroutine(DisableBubbles(this.gameObject));
        }
    }

    private IEnumerator DisableBubbles(GameObject bubbleToDestroy)
	{
        bubbleCol.enabled = false;
        bubbleSpriteRend.enabled = false;


        GameObject objectToSpawwn = this.gameObject;

		if (this is FireBubble)
            objectToSpawwn = fireEffect;
        else if (this is WaterBubble)
            objectToSpawwn = waterEffect;
        else if (this is FreezeBubble)
            objectToSpawwn = iceEffect;

        if(objectToSpawwn == this.gameObject) { Debug.LogError("wtf"); yield return null; }
        Instantiate(objectToSpawwn, transform.position, Quaternion.identity);

        //Spawn particle effects here

        yield return new WaitForSeconds(1f);
        Destroy(bubbleToDestroy);
	}
	public virtual void BubbleBehaviour(float angle, Vector2 gunDirection)
    {
        StartCoroutine(Timer());
        Vector2 normalizedDirection = gunDirection.normalized;
        rb.velocity = (new Vector2(normalizedDirection.x, normalizedDirection.y) * speed) + playerController.rb.velocity ;
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);//Balon patlama efekti
    }

}
