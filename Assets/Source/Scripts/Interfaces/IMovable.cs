using UnityEngine;

public interface IMovable
{
    public void Move(Vector3[] path,float time,int loop,int smoothPath);
}
