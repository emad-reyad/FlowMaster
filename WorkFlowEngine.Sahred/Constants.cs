using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowEngine.Shared
{
    public static class Constants
    {
        public static class Activities
        {
            public static readonly string StartActivity = "StartActivity";
            public static readonly string EndActivity = "EndActivity";
        }
        public static class Actions
        {
            public static readonly string Execute = "Execute";
        } 
        public static class Error
        {
            public static readonly string CanNotNull = "Can not be null or empty.";
            public static readonly string IsMissing = "Property is missing";
            public static readonly string NoNextActivity = "Can not find any configured activity";
            public static readonly string NoStartActivity = "Can not find configured start activity";
            public static readonly string ActionNotAllowed = "this action is allowed for this activity";
        }

    }
}
