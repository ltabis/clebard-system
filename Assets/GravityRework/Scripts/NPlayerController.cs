using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPlayerController : MonoBehaviour
{
	[SerializeField]
	private Transform model = default;
	[SerializeField]
	private Transform playerInputSpace = default;
	[SerializeField, Range(0f, 100f)]
	float maxSpeed = 10f;

	[SerializeField, Range(0f, 100f)]
	float maxAcceleration = 10f, maxAirAcceleration = 1f;

	[SerializeField, Range(0f, 10f)]
	float jumpHeight = 2f;

	[SerializeField, Range(0, 5)]
	int maxAirJumps = 0;

	[SerializeField, Range(0, 90)]
	float maxGroundAngle = 25f, maxStairsAngle = 50f;

	[SerializeField, Range(0f, 100f)]
	float maxSnapSpeed = 100f;

	[SerializeField, Min(0f)]
	float probeDistance = 1f;

	[SerializeField]
	LayerMask probeMask = -1, stairsMask = -1;

	Rigidbody body;

	Vector3 velocity, desiredVelocity;

	bool desiredJump;

	Vector3 contactNormal, steepNormal;

	int groundContactCount, steepContactCount;

	bool OnGround => groundContactCount > 0;

	bool OnSteep => steepContactCount > 0;

	int jumpPhase;

	float minGroundDotProduct, minStairsDotProduct;

	int stepsSinceLastGrounded, stepsSinceLastJump;

	Vector3 worldUp, worldRight, worldForward;

	// debug.
	public bool debugPlayerPath = true, debugPlayerAxis = true;
	private List<Vector3> playerPath;

	void OnValidate()
	{
		minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
		minStairsDotProduct = Mathf.Cos(maxStairsAngle * Mathf.Deg2Rad);
	}

	void Awake()
	{
		playerPath = new List<Vector3>();
		body = GetComponent<Rigidbody>();

		// we are using a custom gravity script.
		body.useGravity = false;
	}

	void Update()
	{
		Vector2 playerInput;
		playerInput.x = Input.GetAxis("Horizontal");
		playerInput.y = Input.GetAxis("Vertical");
		playerInput = Vector2.ClampMagnitude(playerInput, 1f);

		if (playerInputSpace)
		{
			// computing the direction of the veclocity.
			worldRight = ProjectDirectionOnPlane(playerInputSpace.right, worldUp);
			worldForward = ProjectDirectionOnPlane(playerInputSpace.forward, worldUp);

			// aligning the player model to the forward direction.
			float threshold = 0.001f;

			if (playerInput.x < -threshold || playerInput.x > threshold ||
				playerInput.y < -threshold || playerInput.y > threshold) {
				model.forward = worldForward.normalized;
				model.right = worldRight.normalized;
			}
			model.up = worldUp.normalized;

			if (debugPlayerAxis) {
				Debug.DrawLine(transform.position, transform.position + worldForward.normalized, Color.blue, 0.01f);
				Debug.DrawLine(transform.position, transform.position + worldRight.normalized, Color.red, 0.01f);
				Debug.DrawLine(transform.position, transform.position + worldUp.normalized, Color.green, 0.01f);

				// Debug.DrawLine(transform.position, transform.position + playerInputSpace.forward.normalized, Color.blue, 0.01f);
				// Debug.DrawLine(transform.position, transform.position + playerInputSpace.forward.normalized, Color.red, 0.01f);
				// Debug.DrawLine(transform.position, transform.position + playerInputSpace.up.normalized, Color.green, 0.01f);
			}
		}
		else
		{
			worldRight = ProjectDirectionOnPlane(Vector3.right, worldUp);
			worldForward = ProjectDirectionOnPlane(Vector3.forward, worldUp);
		}

		desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
		desiredJump |= Input.GetButtonDown("Jump");
	}

	void FixedUpdate()
	{
		Vector3 gravity = CustomGravity.GetGravity(body.position, out worldUp);

		UpdateState();
		AdjustVelocity();

		if (desiredJump)
		{
			desiredJump = false;
			Jump(gravity);
		}

		velocity += gravity * Time.deltaTime;

		body.velocity = velocity;
		playerPath.Add(body.position);
		ClearState();
	}

	void ClearState()
	{
		groundContactCount = steepContactCount = 0;
		contactNormal = steepNormal = Vector3.zero;
	}

	void UpdateState()
	{
		stepsSinceLastGrounded += 1;
		stepsSinceLastJump += 1;
		velocity = body.velocity;
		if (OnGround || SnapToGround() || CheckSteepContacts())
		{
			stepsSinceLastGrounded = 0;
			if (stepsSinceLastJump > 1)
			{
				jumpPhase = 0;
			}
			if (groundContactCount > 1)
			{
				contactNormal.Normalize();
			}
		}
		else
		{
			contactNormal = worldUp;
		}
	}

	bool SnapToGround()
	{
		if (stepsSinceLastGrounded > 1 || stepsSinceLastJump <= 2)
		{
			return false;
		}
		float speed = velocity.magnitude;
		if (speed > maxSnapSpeed)
		{
			return false;
		}
		if (!Physics.Raycast(
			body.position, -worldUp, out RaycastHit hit,
			probeDistance, probeMask
		))
		{
			return false;
		}
		if (Vector3.Dot(worldUp, hit.normal) < GetMinDot(hit.collider.gameObject.layer))
		{
			return false;
		}

		groundContactCount = 1;
		contactNormal = hit.normal;
		float dot = Vector3.Dot(velocity, hit.normal);
		if (dot > 0f)
		{
			velocity = (velocity - hit.normal * dot).normalized * speed;
		}
		return true;
	}

	bool CheckSteepContacts()
	{
		if (steepContactCount > 1)
		{
			steepNormal.Normalize();
			if (Vector3.Dot(worldUp, steepNormal) >= minGroundDotProduct)
			{
				steepContactCount = 0;
				groundContactCount = 1;
				contactNormal = steepNormal;
				return true;
			}
		}
		return false;
	}

	void AdjustVelocity()
	{
		Vector3 xAxis = ProjectDirectionOnPlane(worldRight, contactNormal);
		Vector3 zAxis = ProjectDirectionOnPlane(worldForward, contactNormal);

		float currentX = Vector3.Dot(velocity, xAxis);
		float currentZ = Vector3.Dot(velocity, zAxis);

		float acceleration = OnGround ? maxAcceleration : maxAirAcceleration;
		float maxSpeedChange = acceleration * Time.deltaTime;

		float newX =
			Mathf.MoveTowards(currentX, desiredVelocity.x, maxSpeedChange);
		float newZ =
			Mathf.MoveTowards(currentZ, desiredVelocity.z, maxSpeedChange);

		velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
	}

	void Jump(Vector3 gravity)
	{
		Vector3 jumpDirection;
		if (OnGround)
		{
			jumpDirection = contactNormal;
		}
		else if (OnSteep)
		{
			jumpDirection = steepNormal;
			jumpPhase = 0;
		}
		else if (maxAirJumps > 0 && jumpPhase <= maxAirJumps)
		{
			if (jumpPhase == 0)
			{
				jumpPhase = 1;
			}
			jumpDirection = contactNormal;
		}
		else
		{
			return;
		}

		stepsSinceLastJump = 0;
		jumpPhase += 1;
		float jumpSpeed = Mathf.Sqrt(2f * gravity.magnitude * jumpHeight);
		jumpDirection = (jumpDirection + worldUp).normalized;
		float alignedSpeed = Vector3.Dot(velocity, jumpDirection);
		if (alignedSpeed > 0f)
		{
			jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
		}
		velocity += jumpDirection * jumpSpeed;
	}

	void OnCollisionEnter(Collision collision)
	{
		EvaluateCollision(collision);
	}

	void OnCollisionStay(Collision collision)
	{
		EvaluateCollision(collision);
	}

	void EvaluateCollision(Collision collision)
	{
		float minDot = GetMinDot(collision.gameObject.layer);
		for (int i = 0; i < collision.contactCount; i++)
		{
			Vector3 normal = collision.GetContact(i).normal;
			float dotUp = Vector3.Dot(worldUp, normal);
			if (dotUp >= minDot)
			{
				groundContactCount += 1;
				contactNormal += normal;
			}
			else if (dotUp > -0.01f)
			{
				steepContactCount += 1;
				steepNormal += normal;
			}
		}
	}

	Vector3 ProjectDirectionOnPlane(Vector3 direction, Vector3 normal)
	{
		return (direction - normal * Vector3.Dot(direction, normal)).normalized;
	}

	float GetMinDot(int layer)
	{
		return (stairsMask & (1 << layer)) == 0 ?
			minGroundDotProduct : minStairsDotProduct;
	}

	void OnDrawGizmos()
	{
		if (!debugPlayerPath)
			return;
	
		Gizmos.color = Color.yellow;
		for (int i = 0; playerPath != null && i < playerPath.Count - 1; ++i)
	        Gizmos.DrawLine(playerPath[i], playerPath[i + 1]);
	}
}
