﻿using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace DotnetSkeleton.SharedKernel.Utils.Extensions;

public class ToKebabParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value) => value != null
        ? Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower()
        : null;
}