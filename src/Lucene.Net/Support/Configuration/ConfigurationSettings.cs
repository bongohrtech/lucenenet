using Lucene.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lucene.Net.Configuration
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

    public abstract class ConfigurationSettings
    {
        private static IConfigurationSettingsFactory configurationSettingsFactory = new SystemPropertiesConfigurationSettingsFactory();
        private readonly string name;

        /// <summary>
        /// Sets the <see cref="ICodecFactory"/> instance used to instantiate
        /// <see cref="Codec"/> subclasses.
        /// </summary>
        /// <param name="codecFactory">The new <see cref="ICodecFactory"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="codecFactory"/> parameter is <c>null</c>.</exception>
        public static void SetConfigFactory(IConfigurationSettingsFactory configFactory)
        {
            if (configFactory == null)
                throw new ArgumentNullException("configurationSettingsFactory");
            ConfigurationSettings.configurationSettingsFactory = configFactory;
        }

        /// <summary>
        /// Gets the associated codec factory.
        /// </summary>
        /// <returns>The codec factory.</returns>
        public static IConfigurationSettingsFactory GetConfigFactory()
        {
            return configurationSettingsFactory;
        }

    }
}
