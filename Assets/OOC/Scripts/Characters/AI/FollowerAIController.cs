using DG.Tweening;
using Pathfinding;
using System;
using System.Collections;
using UnityEngine;

namespace OOC.Characters.AI
{
    public class FollowerAIController : ControllerBase
    {
        [SerializeField]
        private float StopDistance = 1f;
        [SerializeField]
        private float PathUpdatePeriod = 0.5f;


        private Seeker Seeker;
        private Rigidbody2D RB;

        private Path Path;
        private int CurrentWaypoint;

        protected override void Awake()
        {
            base.Awake();

            Seeker = GetComponent<Seeker>();
            RB = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            StartCoroutine(UpdatePathCoroutine());
        }

        #region Path check and seek

        private IEnumerator UpdatePathCoroutine()
        {
            while (true)
            {
                if (IsWorking && Seeker.IsDone() /*&& Path == null*/)
                    SeekNewPath();

                yield return new WaitForSeconds(PathUpdatePeriod);
            }
        }

        private void SeekNewPath()
        {
            var destination = Root.Instance.GetPlayerPosition();
            Seeker.StartPath(RB.position, destination, OnPathComplete);
        }

        private void OnPathComplete(Path path)
        {
            if (path.error)
                return;

            Path = path;
            CurrentWaypoint = 0;
        }

        #endregion


        private void Update()
        {
            if (Motor == null)
                return;

            if (IsWorking == false)
                return;

            if (Path == null)
                return;

            if (CurrentWaypoint >= Path.vectorPath.Count)
            {
                Motor.Move(Vector2.zero);
                Path = null;
                return;
            }


            Vector2 point = (Vector2)Path.vectorPath[CurrentWaypoint];

            var vectorToNextPoint = point - RB.position;
            var distance = vectorToNextPoint.magnitude;
            var direction = vectorToNextPoint / distance;

            if (distance <= StopDistance)
                CurrentWaypoint++;
            else
                Motor.Move(direction);
        }
    }
}

