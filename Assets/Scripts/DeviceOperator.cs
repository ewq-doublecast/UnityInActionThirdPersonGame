using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    private const string FireButton = "Fire3";

    [SerializeField]
    private float _interactionRadius = 1.5f;

    [SerializeField]
    private string _methodName = "Operate";

    private void Update()
    {
        if (Input.GetButtonDown(FireButton))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _interactionRadius);

            foreach (Collider collider in colliders) 
            {
                Vector3 direction = collider.transform.position - transform.forward;

                if (Vector3.Dot(transform.forward, direction) > 0.5f)
                {
                    collider.SendMessage(_methodName, SendMessageOptions.DontRequireReceiver);
                }
            }
        }    
    }
}
