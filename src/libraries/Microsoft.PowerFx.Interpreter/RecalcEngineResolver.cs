﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.PowerFx.Core.Binding;
using Microsoft.PowerFx.Core.Binding.BindInfo;
using Microsoft.PowerFx.Core.Glue;
using Microsoft.PowerFx.Core.Utils;

namespace Microsoft.PowerFx
{
    /// <summary>
    /// <see cref="INameResolver"/> implementation for <see cref="RecalcEngine"/>.
    /// </summary>
    internal class RecalcEngineResolver : SimpleResolver, INameResolver2
    {
        private readonly RecalcEngine _parent;
        private readonly PowerFxConfig _powerFxConfig;

        public IReadOnlyDictionary<string, NameLookupInfo> GlobalSymbols => new ReadOnlyDictionary<string, NameLookupInfo>(_parent.Formulas.Select(f =>
        {
            var description = $"{f.Key} variable";
            return (f.Key, Value: new NameLookupInfo(BindKind.PowerFxResolvedObject, f.Value.Value.Type._type, DPath.Root, 0, f.Value, displayName: new DName(description)));
        }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value));

        public RecalcEngineResolver(RecalcEngine parent, PowerFxConfig powerFxConfig, IReadOnlyDictionary<string, NameLookupInfo> globalSymbols = null)
            : base(powerFxConfig)
        {
            _parent = parent;
            _powerFxConfig = powerFxConfig;
        }

        public override bool Lookup(DName name, out NameLookupInfo nameInfo, NameLookupPreferences preferences = NameLookupPreferences.None)
        {
            // Kinds of globals:
            // - global formula
            // - parameters 
            // - environment symbols

            var str = name.Value;

            if (_parent.Formulas.TryGetValue(str, out var fi))
            {
                var data = fi;
                var type = fi._type._type;
                var description = $"{str} variable";

                nameInfo = new NameLookupInfo(BindKind.PowerFxResolvedObject, type, DPath.Root, 0, data, displayName: new DName(description));                
                return true;
            }

            return base.Lookup(name, out nameInfo, preferences);
        }

        public class ParameterData
        {
            public string ParameterName;
        }
    }
}
