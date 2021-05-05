namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generation;
    using Coreficent.Setting;
    using Coreficent.Utility;
    using UnityEngine;
    using TMPro;
    using System.Collections.Generic;

    internal enum State
    {
        Initialization,
        World,
        Boundary,
        Ground,
        Queue,
        WaveFunctionCollapse,
        Finalization,
        Success,
    }

    public class Main : Script
    {
        public float actionsPerFrame = 1;
        public int Granularity = 5; // how many frames to wait until you re-calculate the FPS
        List<double> times = new List<double>();
        int counter = 5;
        private float steadyFrameRate = 60.0f;

        [SerializeField]
        private Superposition superposition;

        [SerializeField]
        private GameObject board;

        [SerializeField]
        private GameObject progress;

        [SerializeField]
        private TMP_Text text;

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

                        timeController.SetTime(Tuning.StepInterval);
                        timeController.Reset();

                        text.text = "Loading...";

                        QualitySettings.shadows = Tuning.ShadowSetting;


                        Transition(State.World);

                        break;

                    case State.World:
                        Process(world, State.Boundary, Tuning.InstantRendering);
                        progress.transform.localScale = new Vector3(1.0f - world.Progress, 1.0f, 1.0f);

                        text.text = "Loading...";

                        break;

                    case State.Boundary:
                        progress.transform.localScale = Vector3.zero;

                        text.text = "";

                        Process(boundary, State.Ground, true);

                        break;

                    case State.Ground:
                        Process(ground, State.WaveFunctionCollapse, Tuning.InstantRendering);

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

        public double CalcFPS()
        {
            double sum = 0;
            foreach (double F in times)
            {
                sum += F;
            }

            double average = sum / times.Count;
            double fps = 1 / average;


            return fps;
            // update a GUIText or something
        }

        private void UpdateActionPerSecond()
        {
            if (counter <= 0)
            {
                double frameRate = CalcFPS();
                counter = Granularity;

                if (frameRate > steadyFrameRate + 5.0f)
                {
                    ++actionsPerFrame;
                }
                else if (frameRate < steadyFrameRate - 5.0f)
                {
                    --actionsPerFrame;
                }
            }

            if (actionsPerFrame < 1.0f)
            {
                actionsPerFrame = 1.0f;
            }

            times.Add(Time.deltaTime);
            counter--;
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
                UpdateActionPerSecond();

                int actionCount = Mathf.RoundToInt(Tuning.ActionsPerSecond * Time.deltaTime);

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
