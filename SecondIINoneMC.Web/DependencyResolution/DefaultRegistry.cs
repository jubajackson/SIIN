// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Web.Pipeline;
using SecondIINoneMC.Web.Services.Abstract;
using SecondIINoneMC.Web.Services.Concrete;
using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Web.Data;
using SecondIINoneMC.Web.Data.EntityFramework;

namespace SecondIINoneMC.Web.DependencyResolution
{
    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.SingleImplementationsOfInterface();
                });

            /*
             * Simple objects used by controllers
             */
            For<IAddressService>().Use(c => CreateAddressService(c));
            For<ICharterService>().Use(c => CreateCharterService(c));
            For<IGuestbookService>().Use(c => CreateGuestbookService(c));
            For<IOfficerPositionService>().Use(c => CreateOfficerPositionService(c));
            For<IRoleService>().Use(c => CreateRoleService(c));
            For<ISecondIINoneMCService>().Use(c => CreateSecondIINoneMCService(c));
            For<IUserService>().Use(c => CreateUserService(c));
            For<IVideoGalleryService>().Use(c => CreateVideoGalleryService(c));
        }

        private static IAddressService CreateAddressService(IContext c)
        {
            return new AddressService();
        }

        private static ICharterService CreateCharterService(IContext c)
        {
            return new CharterService();
        }

        private static IGuestbookService CreateGuestbookService(IContext c)
        {
            return new GuestbookService();
        }

        private static IOfficerPositionService CreateOfficerPositionService(IContext c)
        {
            return new OfficerPositionService();
        }

        private static IRoleService CreateRoleService(IContext c)
        {
            return new RoleService();
        }

        private static ISecondIINoneMCService CreateSecondIINoneMCService(IContext c)
        {
            return new SecondIINoneMCService();
        }

        private static IUserService CreateUserService(IContext c)
        {
            return new UserService();
        }

        private static IVideoGalleryService CreateVideoGalleryService(IContext c)
        {
            return new VideoGalleryService();
        }


        #endregion
    }
}