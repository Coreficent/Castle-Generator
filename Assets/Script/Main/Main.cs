namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generation;
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : Script
    {
        [SerializeField]
        private SuperPosition superPosition;

        private readonly TimeController timeController = new TimeController();

        private WaveFunctionCollapse waveFunctionCollapse;

        void Start()
        {
            waveFunctionCollapse = new WaveFunctionCollapse(superPosition);

            timeController.Reset();
            timeController.SetTime(1.0f);

            Test.Log("main initialied");
        }

        // Update is called once per frame
        void Update()
        {
            if (timeController.Reached)
            {
                if (waveFunctionCollapse.HasNext())
                {
                    waveFunctionCollapse.Next();
                }

                timeController.Reset();
            }
        }
    }
}
