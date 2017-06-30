using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

namespace MichaelBrandonMorris.MvcGrid.Web
{
    /// <summary>
    ///     Class GridRegistration.
    /// </summary>
    /// TODO Edit XML Comment Template for GridRegistration
    public abstract class GridRegistration
    {
        /// <summary>
        ///     Registers all grids.
        /// </summary>
        /// TODO Edit XML Comment Template for RegisterAllGrids
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
        ///     Registers the grids.
        /// </summary>
        /// TODO Edit XML Comment Template for RegisterGrids
        public abstract void RegisterGrids();

        /// <summary>
        ///     Filters the types in assemblies.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        /// TODO Edit XML Comment Template for FilterTypesInAssemblies
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

        /// <summary>
        ///     Determines whether [is grid registration type] [the
        ///     specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///     <c>true</c> if [is grid registration type] [the
        ///     specified type]; otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for IsGridRegistrationType
        private static bool IsGridRegistrationType(Type type)
        {
            return typeof(GridRegistration).IsAssignableFrom(type)
                   && type.GetConstructor(Type.EmptyTypes) != null;
        }

        /// <summary>
        ///     Types the is public class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// TODO Edit XML Comment Template for TypeIsPublicClass
        private static bool TypeIsPublicClass(Type type)
        {
            return type != null
                   && type.IsPublic
                   && type.IsClass
                   && !type.IsAbstract;
        }
    }
}