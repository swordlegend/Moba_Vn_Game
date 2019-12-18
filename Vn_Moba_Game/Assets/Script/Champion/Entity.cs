using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

namespace Script.Champion
{
    public enum Team {Red, Blue, Monster}
    
    [RequireComponent(typeof(Rigidbody))]
    public class Entity : NetworkBehaviour
    {
        [Header("Component")] public NavMeshAgent agent;
        public NetworkProximityChecker proximityChecker;
        public NetworkIdentity networkIdentity;
        public Animator animator;
        public new Collider collider;
        
        [SyncVar, SerializeField, Header("State")] 
        private string _state = "IDLE";
        public string state
        {
            get { return _state; }
        }

        [Header("Target"), SyncVar]
        private GameObject _target;

        public Entity target
        {
            get { return _target != null ? _target.GetComponent<Entity>() : null; }
            set { _target = value != null ? value.gameObject : null; }
        }
        
        [SerializeField, SyncVar, Header("Level")]
        public int level = 1;
        
    }
}