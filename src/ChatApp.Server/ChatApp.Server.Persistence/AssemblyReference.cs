﻿using System.Reflection;

namespace ChatApp.Server.Persistence;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}