// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace VSSample
{
    public static class HelloSequence
    {
        [FunctionName("FRONTEND")]
        public static async Task<List<string>> Run(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            var backendDelayMs = 4000;

            outputs.Add(await context.CallActivityAsync<string>("BACKEND_1", "ParamForFirstBackend"));

            DateTime deadline = context.CurrentUtcDateTime.Add(TimeSpan.FromMilliseconds(backendDelayMs));
            await context.CreateTimer(deadline, CancellationToken.None);

            outputs.Add(await context.CallActivityAsync<string>("BACKEND_2", "ParamForSecondBackend"));

            return outputs;
        }

        [FunctionName("BACKEND_1")]
        public static string SayHello([ActivityTrigger] string name)
        {
            return $"Hello from first backend {name}!";
        }

        [FunctionName("BACKEND_2")]
        public static string SaySecondHello([ActivityTrigger] string name)
        {
            return $"Hello from second backend {name}!";
        }
    }
 }
