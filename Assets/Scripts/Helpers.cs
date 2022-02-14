using UnityEngine;

public class Helpers
{
    public static bool IsPlayerInRange(Transform transform, Transform player, float viewDistance, float viewAngle, LayerMask viewMask)
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                    return true;
            }
        }
        return false;
    }
}
