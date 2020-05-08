using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lucene.Net.Configuration
{
    public class SystemPropertiesConfigurationSettingsFactory : DefaultConfigurationSettingsFactory
    {
        public override string GetProperty(string key)
        {
            return SystemProperties.GetProperty(key);
        }

        /// <summary>
        /// Retrieves the value of an environment variable from the current process, 
        /// with a default value if it doens't exist or the caller doesn't have 
        /// permission to read the value.
        /// </summary>
        /// <param name="key">The name of the environment variable.</param>
        /// <param name="defaultValue">The value to use if the environment variable does not exist 
        /// or the caller doesn't have permission to read the value.</param>
        /// <returns>The environment variable value.</returns>
        public override string GetProperty(string key, string defaultValue)
        {
            return GetProperty<string>(key, defaultValue,
                (str) =>
                {
                    return str;
                }
            );
        }

        /// <summary>
        /// Retrieves the value of an environment variable from the current process
        /// as <see cref="bool"/>. If the value cannot be cast to <see cref="bool"/>, returns <c>false</c>.
        /// </summary>
        /// <param name="key">The name of the environment variable.</param>
        /// <returns>The environment variable value.</returns>
        public override bool GetPropertyAsBoolean(string key)
        {
            return GetPropertyAsBoolean(key, false);
        }

        /// <summary>
        /// Retrieves the value of an environment variable from the current process as <see cref="bool"/>, 
        /// with a default value if it doens't exist, the caller doesn't have permission to read the value, 
        /// or the value cannot be cast to a <see cref="bool"/>.
        /// </summary>
        /// <param name="key">The name of the environment variable.</param>
        /// <param name="defaultValue">The value to use if the environment variable does not exist,
        /// the caller doesn't have permission to read the value, or the value cannot be cast to <see cref="bool"/>.</param>
        /// <returns>The environment variable value.</returns>
        public override bool GetPropertyAsBoolean(string key, bool defaultValue)
        {
            return GetProperty<bool>(key, defaultValue,
                (str) =>
                {
                    bool value;
                    return bool.TryParse(str, out value) ? value : defaultValue;
                }
            );
        }

        /// <summary>
        /// Retrieves the value of an environment variable from the current process
        /// as <see cref="int"/>. If the value cannot be cast to <see cref="int"/>, returns <c>0</c>.
        /// </summary>
        /// <param name="key">The name of the environment variable.</param>
        /// <returns>The environment variable value.</returns>
        public override int GetPropertyAsInt32(string key)
        {
            return GetPropertyAsInt32(key, 0);
        }

        /// <summary>
        /// Retrieves the value of an environment variable from the current process as <see cref="int"/>, 
        /// with a default value if it doens't exist, the caller doesn't have permission to read the value, 
        /// or the value cannot be cast to a <see cref="int"/>.
        /// </summary>
        /// <param name="key">The name of the environment variable.</param>
        /// <param name="defaultValue">The value to use if the environment variable does not exist,
        /// the caller doesn't have permission to read the value, or the value cannot be cast to <see cref="int"/>.</param>
        /// <returns>The environment variable value.</returns>
        public override int GetPropertyAsInt32(string key, int defaultValue)
        {
            return GetProperty<int>(key, defaultValue,
                (str) =>
                {
                    int value;
                    return int.TryParse(str, out value) ? value : defaultValue;
                }
            );
        }

        private T GetProperty<T>(string key, T defaultValue, Func<string, T> conversionFunction)
        {
            string setting;
            if (ignoreSecurityExceptions)
            {
                setting = Environment.GetEnvironmentVariable(key);
                //try
                //{
                //    setting = Environment.GetEnvironmentVariable(key);
                //}
                //catch (SecurityException)
                //{
                //    setting = null;
                //}
            }
            else
            {
                setting = Environment.GetEnvironmentVariable(key);
            }

            return string.IsNullOrEmpty(setting)
                ? defaultValue
                : conversionFunction(setting);
        }

        protected override void Initialize()
        {
            throw new NotImplementedException();
        }

        internal static bool ignoreSecurityExceptions = true;

    }
}
