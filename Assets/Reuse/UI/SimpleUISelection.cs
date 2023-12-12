using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Reuse.InputManagement.InputManagerIndependent;

namespace Reuse.UI
{
    public class SimpleUISelection : MonoBehaviour
    {
        [Serializable]
        public class SelectionLine{
            public SelectionOption[] column;
        }
        
        [SerializeField] private Transform arrow;
        [SerializeField] private SelectionLine[] options;
        private int _line;
        private int _column;

        private int _lastLine;
        private int _lastColumn;

        [SerializeField] private float keyDelay = 0.2f;
        private float _actualKeyDelay = 0f;
        
        private void Start() 
        {
            _actualKeyDelay = keyDelay;
            PlaceSelectionArrow();
            arrow.gameObject.SetActive(true);
        }

        private void Update() 
        {
            if(_actualKeyDelay < keyDelay){
                _actualKeyDelay += Time.deltaTime;
                return;
            }

            ArrowInputCheck();
            FixLineAndColumn();
            PlaceSelectionArrow();
            SelectCheck();
        }

        private void FixLineAndColumn()
        {
            _line = Mathf.Clamp(_line,0, options.Length - 1);
            _column = Mathf.Clamp(_column, 0, options[_line].column.Length - 1);
        }

        private void PlaceSelectionArrow()
        {
            var option = options[_line].column[_column];
            var curOptionTransform = option.ArrowPosition;
            arrow.SetPositionAndRotation(curOptionTransform.position, curOptionTransform.rotation);

            option.Select();
        }

        public void SetSelection(SelectionOption option)
        {
            if(!this.enabled) return;

            this._line = option.Line;
            this._column = option.Column;
            FixLineAndColumn();
            PlaceSelectionArrow();
        }

        public void SelectCheck()
        {
            if(InputManager.GetAllKey("Select") > 0){
                ExecuteSelection();
            }
        }
        
        public void ExecuteSelection()
        {
            if(!this.enabled) return;
            options[_line].column[_column].Execute();
            this.enabled = false;
        }

        private void ArrowInputCheck()
        {
            if(options[_line].column[_column].Locked) return;

            _lastLine = _line;
            _lastColumn = _column;

            if(InputManager.GetAllKey("Down") > 0){
                _line++;
                _actualKeyDelay = 0f;
            }
            else if(InputManager.GetAllKey("Up") > 0){
                _line--;
                _actualKeyDelay = 0f;
            }
            
            if(InputManager.GetAllKey("Right") > 0){
                _column++;
                _actualKeyDelay = 0f;
            }
            else if(InputManager.GetAllKey("Left") > 0){
                _column--;
                _actualKeyDelay = 0f;
            }
        }
    }
}
