using UnityEngine;

public class RockObstacle : MonoBehaviour
{
	[SerializeField] new private Rigidbody2D rigidbody;
	[SerializeField] private Vector2 speedRange;
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField]
	private float[] speedDecrease;
	public float RockSize => circleCollider2D.radius;
	private Vector2 screenSize;

	private void Start()
	{
		screenSize = CameraCallback.CreateCameraSize();
		spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
	}

	private void Update()
	{
		if (transform.position.x > screenSize.x + RockSize || transform.position.x < -screenSize.x - RockSize)
		{
			Destroy(gameObject);
		}
	}

	public void SetSpeed(Vector2 direction, int speedDecreaseIndex)
	{
		rigidbody.velocity = direction * Random.Range(speedRange.x, speedRange.y) / speedDecrease[speedDecreaseIndex];
	}
}
