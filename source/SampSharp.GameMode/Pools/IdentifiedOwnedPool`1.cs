﻿using System;
using System.Linq;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Pools
{
    /// <summary>
    /// Keeps track of a pool of owned and identified instances.
    /// </summary>
    /// <typeparam name="T">Base type of instances to keep track of.</typeparam>
    public abstract class IdentifiedOwnedPool<T> : Pool<T> where T : class, IIdentifyable, IOwnable
    {
        protected static Type Type;

        /// <summary>
        ///     Registers the type to use when initializing new instances.
        /// </summary>
        /// <typeparam name="T2">The Type to use when initializing new instances.</typeparam>
        public static void Register<T2>()
        {
            Type = typeof(T2);
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="owner" /> and <paramref name="id" />".
        /// </summary>
        /// <param name="owner">The owner of the instance to find.</param>
        /// <param name="id">The identity of the instance to find.</param>
        /// <returns>The found instance.</returns>
        public static T Find(Player owner, int id)
        {
            return All.FirstOrDefault(i => i.Player == owner && i.Id == id);
        }

        /// <summary>
        ///     Initializes a new instance with the given <paramref name="owner" /> and <paramref name="id" />.
        /// </summary>
        /// <param name="owner">The owner of the instance to create.</param>
        /// <param name="id">The identity of the instance to create.</param>
        /// <returns>The initialized instance.</returns>
        public static T Add(Player owner, int id)
        {
            return (T)Activator.CreateInstance(Type, owner, id);
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="owner" /> and <paramref name="id" /> or initializes a new one.
        /// </summary>
        /// <param name="owner">The owner of the instance to find or create.</param>
        /// <param name="id">The identity of the instance to find or create.</param>
        /// <returns>The found instance.</returns>
        public static T FindOrCreate(Player owner, int id)
        {
            return Find(owner, id) ?? Add(owner, id);
        }
    }
}