using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SomeDOTween : MonoBehaviour
{
    private Queue<Vector3> _pos = new();
    private bool _isPlaying = false;
    private void Start()
    {
        DOTween.Init(logBehaviour: LogBehaviour.Verbose);
        transform.DORotate(new Vector3(0, 180, 0), 2).SetEase(Ease.Linear).SetLoops(-1);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                _pos.Enqueue(hit.point);
            }
        }
        if (!_isPlaying && _pos.Count > 0)
        {
            _isPlaying = true;
            transform.DOMove(_pos.Dequeue(), 2).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                _isPlaying = false;
            });
        }
    }
}
