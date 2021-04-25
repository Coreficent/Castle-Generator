namespace Coreficent.Utility
{
    using System.Linq;
    using Coreficent.Setting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class Test
    {
        private static readonly string _delimiter = "::";
        private static readonly string _ender = ".";

        public static void Draw(Vector3 start, Vector3 end, Color color)
        {
            if (DebugMode.On)
            {
                Debug.DrawLine(start, end, color);
            }
        }

        public static void Assert(string name, bool condition)
        {
            if (!condition)
            {
                Warn("Assert" + _delimiter + "Failed" + _delimiter + name);
            }
        }

        public static void Bug(params object[] message)
        {
            Warn("Bug", message);
        }
        public static void ToDo(params object[] message)
        {
            Warn("Todo", message);
        }
        public static void Start(params object[] message)
        {
            Initialize("Start", message);
        }
        public static void Initialize(string name, params object[] message)
        {
            Log("Initialized" + _delimiter + name, message);
        }
        public static void Log(params object[] message)
        {
            Output("Log", message);
        }
        public static void Warn(params object[] message)
        {
            Output("Warn", message);
        }

        public static void Error(params object[] message)
        {
            Output("Error", message);
        }

        private static void Output(string messageType, params object[] variables)
        {
            if (DebugMode.On)
            {
                string message = "";

                foreach (object i in variables)
                {
                    message += _delimiter;

                    if (i == null)
                    {
                        message += "null";
                    }
                    else
                    {
                        if (i.GetType().GetInterface(nameof(ICollection)) != null)
                        {
                            message += "[";
                            IEnumerable enumerable = (i as IEnumerable);

                            foreach (var item in enumerable)
                            {
                                message += item;
                                message += ",";
                            }
                            //message += string.Join(",", enumerable);

                            message += "]";
                        }
                        else
                        {
                            message += i;
                        }
                    }


                }

                switch (messageType)
                {
                    case "Error":
                        Debug.LogError(messageType + message + _ender);
                        break;

                    case "Warn":
                        Debug.LogWarning(messageType + message + _ender);
                        break;

                    case "Todo":
                        Debug.Log(messageType + message + _ender);
                        break;

                    default:
                        Debug.Log(messageType + message + _ender);
                        break;
                }
            }
        }
    }
}
