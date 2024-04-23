﻿/*
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Tests.Serialization
{
    public partial class ResourceParsingTests
    {
        [TestMethod]
        public async Tasks.Task ParseBinaryForR4andHigher()
        {
            var json = "{\"resourceType\":\"Binary\",\"data\":\"ZGF0YQ==\"}";
            var binary = await new FhirJsonParser().ParseAsync<Binary>(json);

            var result = new FhirJsonSerializer().SerializeToString(binary);

            result.Should().Be(json);
            binary.Data.Should().NotBeNull();
            binary.Content.Should().BeNull();
        }

        [TestMethod]
        public async Tasks.Task ParseBinaryForR4andHigherWithUnknownSTU3Element()
        {
            var json = "{\"resourceType\":\"Binary\",\"content\":\"ZGF0YQ==\"}";
            Func<Tasks.Task> act = () => new FhirJsonParser().ParseAsync<Binary>(json);

            await act.Should().ThrowAsync<StructuralTypeException>();
        }
    }
}
