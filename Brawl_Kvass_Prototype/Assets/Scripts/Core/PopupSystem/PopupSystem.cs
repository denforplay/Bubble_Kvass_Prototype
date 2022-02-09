using System;
using System.Collections.Generic;
using Core.PopupSystem.Configurations;
using UnityEngine;

namespace Core.PopupSystem
{
    public class PopupSystem : MonoBehaviour
    {
        [SerializeField] private List<Canvas> _canvases;
        [SerializeField] private Popup _startPopup;
        private readonly Stack<Popup> _popups = new Stack<Popup>();
        [SerializeField] private PopupSystemConfig _popupSystemConfig;

        private void Awake()
        {
            if (_startPopup)
            {
                _popups.Push(SpawnPopup(_startPopup.GetType()));
            }
        }

        public Popup SpawnPopup(Type type, int layer = 0)
        {
            Popup popupPrefab = _popupSystemConfig.PopupPrefabs.Find(a => a.GetType() == type);
            Popup popUp = CreatePopUp(popupPrefab, layer);
            popUp.Closing += (popup) => DeletePopUp();
            _popups.Push(popUp);
            popUp.Show();
            return popUp;
        }

        public T SpawnPopup<T>(int layer = 0) where T : Popup
        {
            return SpawnPopup(typeof(T), layer) as T;
        }

        private Popup CreatePopUp(Popup popupPrefab, int layer = 0)
        {
            var canvas = _canvases.Find(x => x.sortingOrder == layer);
            Popup popUp = Instantiate(popupPrefab, canvas.transform);
            return popUp;
        }

        public void DeletePopUp()
        {
            Popup popup = _popups.Pop();
            popup.Hide();
        }
    }
}

