using System;
using System.Collections.Generic;
using Core.PopupSystem.Configurations;
using UnityEngine;

namespace Core.PopupSystem
{
    public class PopupSystem : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
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

        public Popup SpawnPopup(Type type)
        {
            Popup popupPrefab = _popupSystemConfig.PopupPrefabs.Find(a => a.GetType() == type);
            Popup popUp = CreatePopUp(popupPrefab);
            popUp.Closing += (popUp) => DeletePopUp();
            _popups.Push(popUp);
            popUp.Show();
            return popUp;
        }

        public T SpawnPopup<T>() where T : Popup
        {
            return SpawnPopup(typeof(T)) as T;
        }

        private Popup CreatePopUp(Popup popupPrefab)
        {
            Popup popUp = Instantiate(popupPrefab, _canvas.transform);
            return popUp;
        }

        public void DeletePopUp()
        {
            Popup popup = _popups.Pop();
            popup.Hide();
        }
    }
}

