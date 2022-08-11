using UnityEngine;
using Tobii.Research.Unity;
using System.Collections;


    public class DetectEyeGaze : MonoBehaviour
{
        /// <summary>
        /// Get <see cref="GazeTrail"/> instance. This is assigned
        /// in Awake(), so call earliest in Start().
        /// </summary>
        public static DetectEyeGaze Instance { get; private set; }

        private EyeTracker _eyeTracker;

        protected  void OnAwake()
        {

            Instance = this;
        }

        protected  void OnStart()
        {

            _eyeTracker = EyeTracker.Instance;
        }

        protected  bool GetRay(out Ray ray)
        {
            if (_eyeTracker == null)
            {
                ray = default(Ray);
                return false;
            }

            var data = _eyeTracker.LatestGazeData;
        //Debug.Log(data);
            ray = data.CombinedGazeRayScreen;
        Debug.Log(ray);
        
            return data.CombinedGazeRayScreenValid;

            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            //{
            //    if (hit.collider.CompareTag("gaze"))
            //    {
            //        Debug.Log("gazing at " + hit.collider.name);
            //    }
            //}
        }

        protected  bool HasEyeTracker
        {
            get
            {
                return _eyeTracker != null;
            }
        }

    }
