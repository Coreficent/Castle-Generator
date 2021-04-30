namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generation;
    using Coreficent.Setting;
    using Coreficent.Utility;
    using UnityEngine;

    internal enum State
    {
        Initialization,
        World,
        WaveFunctionCollapse,
        Fin,
    }

    public class Main : Script
    {
        [SerializeField]
        private Superposition superposition;

        [SerializeField]
        private GameObject board;

        private State gameState = State.Initialization;
        private readonly TimeController timeController = new TimeController();

        private World world;
        private Border border;
        private WaveFunctionCollapse waveFunctionCollapse;

        void Update()
        {
            if (timeController.Reached)
            {
                Test.Log("current state", gameState);

                switch (gameState)
                {
                    case State.Initialization:
                        world = new World(superposition, board);
                        border = new Border(world);
                        waveFunctionCollapse = new WaveFunctionCollapse(world);

                        timeController.Reset();
                        timeController.SetTime(Tuning.StepInterval);

                        Transition(State.World);

                        break;

                    case State.World:
                        if (border.HasNext())
                        {
                            border.Next();
                        }
                        else
                        {
                            Transition(State.WaveFunctionCollapse);
                        }

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
