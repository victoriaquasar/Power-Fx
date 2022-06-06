// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using Microsoft.PowerFx.Core.App.ErrorContainers;
using Microsoft.PowerFx.Core.Errors;
using Microsoft.PowerFx.Core.Localization;
using Microsoft.PowerFx.Core.Types;
using Microsoft.PowerFx.Core.Utils;
using Microsoft.PowerFx.Syntax;

namespace Microsoft.PowerFx.Core.Functions
{
    internal class ParameterConstraint
    {
        public readonly bool HasCustomDataflowGraphRepresentation;

        public ParameterConstraint(bool hasCustomDataflowGraphRepresentation = false)
        {
            HasCustomDataflowGraphRepresentation = hasCustomDataflowGraphRepresentation;
        }
    }
}
