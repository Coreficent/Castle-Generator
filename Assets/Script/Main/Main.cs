namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generation;
    using Coreficent.Setting;
    using Coreficent.Utility;
    using UnityEngine;
    using TMPro;

    internal enum State
    {
        Initialization,
        World,
        WorldFinalization,
        Queue,
        WaveFunctionCollapse,
        Finalization,
        Success,
    }

    public class Main : Script
    {
        public float actionsPerFrame = 10.0f;

        [SerializeField]
        private Superposition superposition;

        [SerializeField]
        private GameObject board;

        [SerializeField]
        private GameObject progress;

        [SerializeField]
        private TMP_Text text;

        [SerializeField]
        private Style style;

        private State gameState = State.Initialization;
        private readonly TimeController timeController = new TimeController();

        private World world;

        private WaveFunctionCollapse waveFunctionCollapse;

        protected virtual void Update()
        {
            if (timeController.Reached)
            {
                --actionsPerFrame;

                if (actionsPerFrame < Tuning.ActionsPerSecond)
                {
                    actionsPerFrame = Tuning.ActionsPerSecond;
                }

                switch (gameState)
                {
                    case State.Initialization:
                        Test.Log("initialize");

                        world = new World(superposition, board);
                        waveFunctionCollapse = new WaveFunctionCollapse(world);

                        timeController.SetTime(Tuning.StepInterval);
                        timeController.Reset();

                        text.text = "Loading...";

                        QualitySettings.shadows = Tuning.ShadowSetting;

                        actionsPerFrame = Tuning.ActionsPerSecond;

                        Transition(State.World);

                        break;

                    case State.World:
                        Process(world, State.WorldFinalization, Tuning.InstantRendering);
                        progress.transform.localScale = new Vector3(1.0f - world.Progress, 1.0f, 1.0f);

                        text.text = "Loading...";

                        break;

                    case State.WorldFinalization:
                        if (!world.Validated)
                        {
                            Test.Warn("invalid world");
                        }

                        progress.transform.localScale = Vector3.zero;

                        text.text = "";

                        style.Randomize();

                        Transition(State.WaveFunctionCollapse);

                        break;

                    case State.WaveFunctionCollapse:
                        Process(waveFunctionCollapse, State.Finalization, Tuning.InstantRendering);

                        break;

                    case State.Finalization:
                        waveFunctionCollapse.PrintStatistics();
                        QualitySettings.shadows = ShadowQuality.All;

                        Transition(State.Success);
                        break;

                    case State.Success:
                        if (Input.GetKey(KeyCode.R))
                        {
                            Reset();
                        }

                        break;

                    default:
                        Test.Warn("unexpected game state");

                        break;

                }
                timeController.Reset();
            }
            else
            {
                actionsPerFrame += 5.0f;
            }
        }

        public void Restart()
        {
            if (gameState == State.Success)
            {
                Reset();
            }
        }

        private void Reset()
        {
            world.Clear();
            Transition(State.Initialization);
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
                int actionCount = Mathf.RoundToInt(actionsPerFrame);

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
