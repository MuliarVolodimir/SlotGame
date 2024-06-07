using UnityEngine;
using UnityEngine.UI;

public class SlotGameUI : MonoBehaviour
{
    [SerializeField] Button _spinButton;
    [SerializeField] SlotSystem _slotSystem;

    [SerializeField] AudioClip _applayClip;

    private void Start()
    {
        _slotSystem = GetComponent<SlotSystem>();
        _spinButton.onClick.AddListener(() => { Spin(); });
    }

    private void Spin()
    {
        AudioManager.Instance.PlayOneShotSound(_applayClip);
        _slotSystem.SpinSlots();    
    }
}
