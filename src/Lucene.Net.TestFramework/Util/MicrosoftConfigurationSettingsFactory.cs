﻿using Lucene.Net.Configuration;
using Microsoft.Extensions.Configuration;
using System;

namespace Lucene.Net.Util
{
    /*
     * Licensed to the Apache Software Foundation (ASF) under one or more
     * contributor license agreements.  See the NOTICE file distributed with
     * this work for additional information regarding copyright ownership.
     * The ASF licenses this file to You under the Apache License, Version 2.0
     * (the "License"); you may not use this file except in compliance with
     * the License.  You may obtain a copy of the License at
     *
     *     http://www.apache.org/licenses/LICENSE-2.0
     *
     * Unless required by applicable law or agreed to in writing, software
     * distributed under the License is distributed on an "AS IS" BASIS,
     * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     * See the License for the specific language governing permissions and
     * limitations under the License.
     */

    public class MicrosoftConfigurationSettingsFactory : DefaultConfigurationSettingsFactory
    {
        public static IConfiguration config;
        
        public MicrosoftConfigurationSettingsFactory() : this("appsettings.json")
        {
         
        }
        public MicrosoftConfigurationSettingsFactory(string filePath)
        {
            Initialize(filePath);
        }
        protected override void Initialize()
        {
        }

#if NETSTANDARD2_1
        protected void Initialize(string filePath)
        {
            config = new ConfigurationBuilder()
              .AddJsonFile(filePath, optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();
            Initialize();
        }


        protected void Initialize(string filePath, string[] args)
        {
            config = new ConfigurationBuilder()
              .AddJsonFile(filePath, optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .AddCommandLine(args)
              .Build();
            Initialize();
        }
#elif NET451
            
            protected void Initialize(string filePath)
            {
                ConfigurationInternal = new ConfigurationBuilder()
                  .Build();
                Initialize();
            }
#else
            protected void Initialize(string filePath)
            {
                config = new ConfigurationBuilder()
                  .Build();
                Initialize();
            }
#endif

        public override string GetProperty(string key)
        {
            return config[key];
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
                setting = config[key];
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
                setting = config[key];
            }

            return string.IsNullOrEmpty(setting)
                ? defaultValue
                : conversionFunction(setting);
        }

        internal static bool ignoreSecurityExceptions = true;
    }
}
