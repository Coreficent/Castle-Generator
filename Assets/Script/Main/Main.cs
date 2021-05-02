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
        Boundary,
        Queue,
        WaveFunctionCollapse,
        Success,
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
        private Boundary boundary;
        private Ground ground;

        private WaveFunctionCollapse waveFunctionCollapse;

        protected virtual void Update()
        {
            if (timeController.Reached)
            {
                switch (gameState)
                {
                    case State.Initialization:
                        Test.Log("Initializing");

                        world = new World(superposition, board);
                        boundary = new Boundary(world);
                        ground = new Ground(world);
                        waveFunctionCollapse = new WaveFunctionCollapse(world);

                        timeController.SetTime(Tuning.StepInterval);
                        timeController.Reset();

                        QualitySettings.shadows = Tuning.ShadowSetting;

                        Transition(State.Boundary);

                        break;

                    case State.Boundary:
                        Process(boundary, State.World, true);

                        break;

                    case State.World:
                        Process(ground, State.Queue, true);

                        break;

                    case State.Queue:
                        waveFunctionCollapse.QueueUncollapsedModules();
                        Transition(State.WaveFunctionCollapse);

                        break;

                    case State.WaveFunctionCollapse:
                        Process(waveFunctionCollapse, State.Success, Tuning.InstantRendering);

                        break;
                    case State.Success:
                        if (Input.GetKey(KeyCode.R))
                        {
                            world.Clear();
                            Transition(State.Initialization);
                        }

                        break;

                    default:
                        Test.Warn("unexpected game state");

                        break;

                }
                timeController.Reset();
            }
        }

        private void Process(IAnimatable animatable, State next, bool instant)
        {
            if (instant)
            {
                while (animatable.HasNext())
                {
                    animatable.Next();
                }
                Transition(next);
            }
            else
            {
                if (animatable.HasNext())
                {
                    animatable.Next();
                }
                else
                {
                    Transition(next);
                }
            }
        }

        private void Transition(State next)
        {
            timeController.Reset();
            gameState = next;
            Test.Log("processing", next);
        }
    }
}
