using UnityEngine;

public class DanceAnim : MonoBehaviour
{
    public Animator anim;
    public string danceName;

    void Start()
    {
        anim.Play(danceName);
    }
}
