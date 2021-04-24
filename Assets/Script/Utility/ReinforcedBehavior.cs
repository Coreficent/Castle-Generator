namespace Coreficent.Utility
{
    using Coreficent.Setting;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

    public class ReinforcedBehavior : MonoBehaviour
    {
        protected virtual void Awake()
        {
            if (DebugMode.On)
            {
                foreach (FieldInfo field in GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(field => field.IsPublic || field.GetCustomAttributes(typeof(SerializeField), true).Length > 0))
                {
                    object value = field.GetValue(this);

                    if (value == null || value.ToString() == "null" || value is false)
                    {
                        DebugUtility.Warn(string.Format("{0} in {1} is {2}", field.Name, this, field.GetValue(this)));
                    }
                }
            }
        }
    }
}
