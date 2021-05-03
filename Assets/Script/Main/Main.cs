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
        Ground,
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

        [SerializeField]
        private GameObject progress;

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
                        Test.Log("initialize");

                        world = new World(superposition, board);
                        boundary = new Boundary(world);
                        ground = new Ground(world);
                        waveFunctionCollapse = new WaveFunctionCollapse(world);

                        QualitySettings.shadows = Tuning.ShadowSetting;

                        timeController.SetTime(Tuning.StepInterval);
                        timeController.Reset();

                        Transition(State.World);

                        break;

                    case State.World:
                        Process(world, State.Boundary, Tuning.InstantRendering);
                        progress.transform.localScale = new Vector3(1.0f, 1.0f - world.Progress, 1.0f);

                        break;

                    case State.Boundary:
                        progress.transform.localScale = Vector3.zero;
                        Process(boundary, State.Ground, true);

                        break;

                    case State.Ground:
                        Process(ground, State.WaveFunctionCollapse, Tuning.InstantRendering);

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
                int actionCount = Mathf.RoundToInt(Tuning.ActionPerSecond * Time.deltaTime);

                if (actionCount < 1)
                {
                    actionCount = 1;
                }

                for (int i = 0; i < actionCount; ++i)
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
        }

        private void Transition(State next)
        {
            timeController.Reset();
            gameState = next;
            Test.Log("process", next);
        }
    }
}
