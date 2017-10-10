//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2015 Jb Evain
// Copyright (c) 2008 - 2011 Novell, Inc.
//
// Licensed under the MIT/X11 license.
//

using System;
using SR = System.Reflection;

#if NET_CORE
using System.Collections.Generic;
#endif

namespace Mono {

#if NET_CORE
	enum TypeCode {
		Empty = 0,
		Object = 1,
		DBNull = 2,
		Boolean = 3,
		Char = 4,
		SByte = 5,
		Byte = 6,
		Int16 = 7,
		UInt16 = 8,
		Int32 = 9,
		UInt32 = 10,
		Int64 = 11,
		UInt64 = 12,
		Single = 13,
		Double = 14,
		Decimal = 15,
		DateTime = 16,
		String = 18
	}
#endif

	static class TypeExtensions {

#if NET_CORE
		private static readonly Dictionary<Type, TypeCode> TypeCodeMap = new Dictionary<Type, TypeCode>
		{
			{ typeof (bool), TypeCode.Boolean },
			{ typeof (char), TypeCode.Char },
			{ typeof (sbyte), TypeCode.SByte },
			{ typeof (byte), TypeCode.Byte },
			{ typeof (short), TypeCode.Int16 },
			{ typeof (ushort), TypeCode.UInt16 },
			{ typeof (int), TypeCode.Int32 },
			{ typeof (uint), TypeCode.UInt32 },
			{ typeof (long), TypeCode.Int64 },
			{ typeof (ulong), TypeCode.UInt64 },
			{ typeof (float), TypeCode.Single },
			{ typeof (double), TypeCode.Double },
			{ typeof (decimal), TypeCode.Decimal },
			{ typeof (DateTime), TypeCode.DateTime },
			{ typeof (string), TypeCode.String },
		};
#endif

		public static TypeCode GetTypeCode (this Type type)
		{
#if NET_CORE
			if (type == null)
				return TypeCode.Empty;

			TypeCode code;
			if (!TypeCodeMap.TryGetValue (type, out code))
				return TypeCode.Object;

			return code;
#else
			return Type.GetTypeCode (type);
#endif
		}

		public static Type[] GetGenericArguments (this Type type)
		{
#if NET_CORE
			var info = type.GetTypeInfo ();
			return info.IsGenericTypeDefinition ? info.GenericTypeParameters : info.GenericTypeArguments;
#else
			return type.GetGenericArguments ();
#endif
		}

#if NET_CORE
		public static SR.TypeInfo GetTypeInfo (this Type self)
		{
			return SR.IntrospectionExtensions.GetTypeInfo(self);
		}

		public static string GetContainingAssemblyLocation(this Type self)
		{
			return self.GetTypeInfo ().Module.FullyQualifiedName;
		}
#else
		public static Type GetTypeInfo (this Type self)
		{
			return self;
		}

		public static string GetContainingAssemblyLocation (this Type self)
		{
			return self.Assembly.Location;
		}
#endif
	}
}
