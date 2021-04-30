﻿namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generation;
    using Coreficent.Setting;
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : Script
    {
        [SerializeField]
        private Superposition superPosition;

        [SerializeField]
        private Superposition emptyPosition;

        private enum State
        {
            WaveFunctionCollapse,
            Fin,
        }

        private State gameState = State.WaveFunctionCollapse;

        private readonly TimeController timeController = new TimeController();

        private WaveFunctionCollapse waveFunctionCollapse;



        void Start()
        {
            waveFunctionCollapse = new WaveFunctionCollapse(superPosition, emptyPosition);

            timeController.Reset();
            timeController.SetTime(Tuning.StepInterval);

            Test.Log("main initialied");
        }

        // Update is called once per frame
        void Update()
        {


            if (timeController.Reached)
            {
                Test.Log("on update");

                switch (gameState)
                {
                    case State.WaveFunctionCollapse:

                        if (waveFunctionCollapse.HasNext())
                        {
                            waveFunctionCollapse.Next();
                        }
                        else
                        {
                            Transition(State.Fin);
                        }


                        break;
                    case State.Fin:

                        Test.Log("finished");

                        
                        break;

                    default:
                        Test.Warn("unexpected game state");
                        break;

                }
                timeController.Reset();
            }

        }

        private void Transition(State next)
        {
            timeController.Reset();
            gameState = next;
        }
    }
}
