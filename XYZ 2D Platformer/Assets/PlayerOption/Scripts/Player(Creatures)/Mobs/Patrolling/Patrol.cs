using System.Collections;
using UnityEngine;

namespace PlayerOption.Scripts.Player_Creatures_.Mobs.Patrolling
{
    public abstract class Patrol : MonoBehaviour

    {
        public abstract IEnumerator DoPatrol();
    }
}