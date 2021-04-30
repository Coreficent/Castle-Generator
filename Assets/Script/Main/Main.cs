namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generation;
    using Coreficent.Setting;
    using Coreficent.Utility;
    using UnityEngine;

    internal enum State
    {
        Initialize,
        WaveFunctionCollapse,
        Fin,
    }

    public class Main : Script
    {
        [SerializeField]
        private Superposition superposition;

        private State gameState = State.Initialize;
        private readonly TimeController timeController = new TimeController();

        private World world;
        private WaveFunctionCollapse waveFunctionCollapse;

        void Update()
        {
            if (timeController.Reached)
            {
                switch (gameState)
                {
                    case State.Initialize:
                        world = new World(superposition);
                        waveFunctionCollapse = new WaveFunctionCollapse(world);

                        timeController.Reset();
                        timeController.SetTime(Tuning.StepInterval);

                        Transition(State.WaveFunctionCollapse);

                        break;

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
