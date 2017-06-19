using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

namespace MvcGrid.Web
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class GridRegistration
    {
        /// <summary>
        /// 
        /// </summary>
        public static void RegisterAllGrids()
        {
            var gridRegistrationTypes =
                FilterTypesInAssemblies(IsGridRegistrationType);

            foreach (var gridRegistrationType in gridRegistrationTypes)
            {
                var registration =
                    (GridRegistration) Activator.CreateInstance(
                        gridRegistrationType);
                registration.RegisterGrids();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void RegisterGrids();

        private static IEnumerable<Type> FilterTypesInAssemblies(
            Predicate<Type> predicate)
        {
            // Go through all assemblies referenced by the application and search for types matching a predicate
            IEnumerable<Type> typesSoFar = Type.EmptyTypes;

            var assemblies = BuildManager.GetReferencedAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type[] typesInAsm;
                try
                {
                    typesInAsm = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    typesInAsm = ex.Types;
                }
                typesSoFar = typesSoFar.Concat(typesInAsm);
            }

            return typesSoFar.Where(
                type => TypeIsPublicClass(type) && predicate(type));
        }

        private static bool IsGridRegistrationType(Type type)
        {
            return typeof(GridRegistration).IsAssignableFrom(type)
                   && type.GetConstructor(Type.EmptyTypes) != null;
        }

        private static bool TypeIsPublicClass(Type type)
        {
            return type != null
                   && type.IsPublic
                   && type.IsClass
                   && !type.IsAbstract;
        }
    }
}