using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Entity : MonoBehaviour
{
    [System.Serializable]
    public struct CharacterControllerSettings
    {
        public Vector3 center;
        public float height;
    }

    protected CharacterController controller;
    public CharacterControllerSettings standingCcSettings;
    public CharacterControllerSettings crouchCcSettings;

    private Coroutine ccSettingsTransitionCoroutine;

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public virtual void MoveEntity(Vector3 direction, float speed)
    {
        controller.Move(direction * speed * Time.deltaTime);
    }

    public virtual void Uncrouch()
    {
        if (ccSettingsTransitionCoroutine != null)
            StopCoroutine(ccSettingsTransitionCoroutine);
        ccSettingsTransitionCoroutine = StartCoroutine(AnimateCcSettingsTransition(standingCcSettings, 0.3f));
    }

    public virtual void Crouch()
    {
        if (ccSettingsTransitionCoroutine != null)
            StopCoroutine(ccSettingsTransitionCoroutine);
        ccSettingsTransitionCoroutine = StartCoroutine(AnimateCcSettingsTransition(crouchCcSettings, 0.3f));
    }

    private IEnumerator AnimateCcSettingsTransition(CharacterControllerSettings targetCcSettings, float duration)
    {
        float t = 0;
        AnimationCurve easeInOutCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        float startHeight = controller.height;
        Vector3 startCenter = controller.center;

        while(t<1)
        {
            controller.height = Mathf.Lerp(startHeight, targetCcSettings.height, easeInOutCurve.Evaluate(t));
            controller.center = Vector3.Lerp(startCenter, targetCcSettings.center, easeInOutCurve.Evaluate(t));

            t += Time.deltaTime / duration;
            yield return null;
        }

        controller.height = targetCcSettings.height;
        controller.center = targetCcSettings.center;
    }
}
