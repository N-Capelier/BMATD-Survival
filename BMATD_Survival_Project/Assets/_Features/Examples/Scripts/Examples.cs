using System.Collections;
using UnityEngine;

public class Examples : MonoBehaviour
{
	[SerializeField]
	private float _moveSpeed = 10f;

	[SerializeField]
	private Transform _targetTransform;

	[SerializeField]
	private Rigidbody _rigidbody;

	[ContextMenu("Run")]
	private void Start()
	{
		//Direction Vectors
		//Constant
		Vector3 vector = Vector3.up;

		print("Vector3.up");
		print(vector);

		//Will be changed based on the object rotation
		print("transform.up");
		print(transform.up);

		//World space position
		print("World position");
		print(transform.position);

		//Local position based on parent position
		print("Local position");
		print(transform.localPosition);

		//transform.Translate() vs Vector3.MoveTowards()


		//Set the position of the object based on a direction vector;
		//transform.Translate(Vector3.forward * _moveSpeed, Space.World);

		print("MoveTowards() Vector");
		Vector3 movement = Vector3.MoveTowards(transform.position, _targetTransform.position, 10f);
		print($"vector: {movement} with magnitude: {movement.magnitude}");

		//Rigidbody methods

		//Giving a "punch" to the object
		_rigidbody.AddForce(Vector3.up * 100f);

		// Override the physics movement
		_rigidbody.linearVelocity = Vector3.zero;




		//Dot Product
		print("Two vectors facing the same direction"); // = 1
		print(Vector3.Dot(Vector3.forward, Vector3.forward));

		print("Two vectors with a 90d angle"); // = 0
		print(Vector3.Dot(Vector3.forward, Vector3.up));

		print("Two vectors facing opposite directions"); // = -1
		print(Vector3.Dot(Vector3.forward, Vector3.back));

		//Cross Product
		print("Cross product");
		print(Vector3.Cross(Vector3.right, Vector3.up));
		// => The result will be Vector3.forward

		//Quaternions
		// https://youtu.be/eRVRioN4GwA?t=779


		Quaternion.Euler(Vector3.zero);
		// Zero degree angle = Quaternion.Euler(Vector3.zero) = Quaternion.identity;


		//transform.LookAt(_targetTransform.position);


		//[]
		//transform.rotation = Quaternion.LookRotation((_targetTransform.position - transform.position).normalized, Vector3.up);


		//Quaternion.RotateTowards(originRotation, targetRotation, maxRotationAngleInDegrees);






		//Lerp
		float result1 = Mathf.Lerp(0f, 10f, 0.5f);

		Vector3 result2 = Vector3.Lerp(transform.position, _targetTransform.position, .5f);

		Quaternion result3 = Quaternion.Lerp(transform.rotation, _targetTransform.rotation, .5f);

		//Slerp

		Quaternion result4 = Quaternion.Slerp(transform.rotation, _targetTransform.rotation, .5f);












		//Random.Range()


		//Random.value


		//Calculated Weighting


		//Animation Curves
	}


	[ContextMenu("Slerp")]
	private void StartSlerpCoroutine()
	{
		StartCoroutine(SlerpCubeCoroutine());
	}


	private IEnumerator SlerpCubeCoroutine()
	{
		Quaternion fromRotation = transform.rotation;
		Quaternion toRotation = _targetTransform.rotation;

		Vector3 fromPosition = transform.position;
		Vector3 toPosition = _targetTransform.position;

		float duration = 12f;
		float progress = 0f;

		while(progress < duration)
		{
			progress += Time.deltaTime;

			float delta = progress / duration;

			transform.position = Vector3.Lerp(fromPosition, _targetTransform.position, delta);
			transform.rotation = Quaternion.Slerp(fromRotation, _targetTransform.rotation, delta);

			yield return null;
		}

		transform.rotation = toRotation;
	}
}
