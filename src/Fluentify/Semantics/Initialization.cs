namespace Fluentify.Semantics;

/// <summary>
/// Represents the expressions to use when creating a default instance of a type.
/// </summary>
internal readonly record struct Initialization(string Explicit, string TargetTyped);
