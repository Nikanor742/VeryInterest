using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour,IMovable
{
    public void Move(Vector3[] path, float time, int loop,int smoothPath)
    {
        DOTween.Kill(transform);
        if (smoothPath == 1)
        {
            transform.DOPath(path, time, PathType.CatmullRom).OnComplete(() =>
            {
                if (loop == 1)
                    Move(path, time, loop,smoothPath);
            });
        }
        else
        {
            transform.DOPath(path, time).OnComplete(() =>
            {
                if (loop == 1)
                    Move(path, time, loop, smoothPath);
            });
        }
        
    }
}
