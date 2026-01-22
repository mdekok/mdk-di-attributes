using System;

namespace Mdk.DIAttributes;

/// <summary>Dependency Injection Attribute for registering hosted service types.</summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class AddHostedService : Attribute
{ }
