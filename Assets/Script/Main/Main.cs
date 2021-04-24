namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : Script
    {
        private readonly TimeController timeController = new TimeController();

        void Start()
        {
            timeController.Reset();
            timeController.SetTime(1.0f);

            Test.Log("main initialied");
        }

        // Update is called once per frame
        void Update()
        {
            if (timeController.Reached)
            {
                Test.Log("next");

                timeController.Reset();
            }
        }
    }
}
