using System;
using System.Threading.Tasks;
using UnityEngine;

public class OpenBehaviour : StateMachineBehaviour
{
    private static string _closeTrigger = "Close";

    [SerializeField] private float _closeDelay = 2f;

    public override async void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        await WaitForSecondsAsync(_closeDelay);
        animator.SetTrigger(_closeTrigger);
    }

    public async Task WaitForSecondsAsync(float seconds)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
    }
}
