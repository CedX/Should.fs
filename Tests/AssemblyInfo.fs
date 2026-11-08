namespace Belin.Should

open Microsoft.VisualStudio.TestTools.UnitTesting

// The assembly properties.
[<assembly: Parallelize(Scope = ExecutionScope.MethodLevel)>]
do ()
