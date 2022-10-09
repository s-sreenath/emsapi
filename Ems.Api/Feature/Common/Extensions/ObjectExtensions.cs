// <copyright file="ObjectExtensions.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Common.Extensions
{
    using System.Text.Json;

    public static class ObjectExtensions
    {
        public static string ToJson<T>(this T inputObject)
        {
            return JsonSerializer.Serialize(inputObject);
        }
    }
}
