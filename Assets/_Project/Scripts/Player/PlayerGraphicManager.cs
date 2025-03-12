using System;
using UnityEngine;

namespace MagneticMayhem
{
    public class PlayerGraphicManager : MonoBehaviour
    {
        [SerializeField] private Sprite north;
        [SerializeField] private Sprite south;

        private SpriteRenderer spriteRenderer;
        private Magnetism magnetism;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            magnetism = GetComponentInParent<Magnetism>();
        }
        private void OnEnable()
        {
            magnetism.SuscribeListener(OnPoleChange);
        }

        private void OnDisable()
        {
            magnetism.RemoveListener(OnPoleChange);
        }
        void OnPoleChange(MagnetStatus _newPoleStatus)
            
        {
            if(_newPoleStatus.pole != MagenticPole.positive)
            {
                spriteRenderer.sprite = north;
            }
            else
            {
                spriteRenderer.sprite = south;
            }
        }
    }
}
