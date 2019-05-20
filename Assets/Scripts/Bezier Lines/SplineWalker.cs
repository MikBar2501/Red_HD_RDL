using UnityEngine;

public class SplineWalker : MonoBehaviour
{

    public BezierSpline spline;

    public float duration;

    public bool lookForward;

    public SplineWalkerMode mode;

    private float progress;
    private bool goingForward = true;
    private Transform parent;

    private void Start()
    {
        parent = transform.parent;
    }

    private void Update()
    {
        if (goingForward)
        {
            progress += Time.deltaTime / duration;
            if (progress > 1f)
            {
              /*  if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }*/
                if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        }
        else if (!goingForward)
        {
            
            progress -= Time.deltaTime / duration;
            if (progress < 0f)
            {
                progress = -progress;
                goingForward = true;
            }
        }
        Vector3 position = spline.GetProgress(progress);
        transform.localPosition = position - parent.position;
        //Vector3.SmoothDamp(transform.localPosition, position, );
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }
    }
}