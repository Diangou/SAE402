using UnityEngine;
using System.Collections;

// https://learn.unity.com/tutorial/extension-methods
public static class Extensions
{
    public static IEnumerator MoveBackAndForth(this Transform transform, Vector3 endPosition) {
        Vector3 initialPosition = transform.localPosition;

        yield return Move(transform, initialPosition, endPosition);
        yield return Move(transform, endPosition, initialPosition);
    }

    public static IEnumerator Move(Transform transform, Vector3 from, Vector3 to) {
        float elapsed = 0f;
        float duration = 0.125f;

        while(elapsed < duration) {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }
}