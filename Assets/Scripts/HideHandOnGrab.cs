using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseControllerInteractor))]
public class HideHandOnGrab : MonoBehaviour
{
    // Assign this in the Inspector to your HandOffset_* GameObject
    public GameObject handModelRoot;

    XRBaseControllerInteractor _interactor;

    void Awake()
    {
        _interactor = GetComponent<XRBaseControllerInteractor>();
    }

    void OnEnable()
    {
        _interactor.selectEntered.AddListener(OnGrab);
        _interactor.selectExited .AddListener(OnRelease);
    }

    void OnDisable()
    {
        _interactor.selectEntered.RemoveListener(OnGrab);
        _interactor.selectExited .RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        handModelRoot.SetActive(false);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        handModelRoot.SetActive(true);
    }
}
