// -----------------------------------------------------------------------------
// Sandbox: struct vs class — identity, copies, and the foreach trap
// Section: 1.1
// Run:     dotnet run 01-1-struct-vs-class.cs   (.NET 10+)
// Goal:    Turn "value vs reference" from a phrase into instinct.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;


// === Scenario 1a: mutating a List<struct> ===================================
//
// The intuitive version does NOT compile:
//
//     foreach (var p in structList) p.X = 99;
//     // error CS1654: Cannot modify members of 'p' because it is a
//     //              'foreach iteration variable'
//
// `p` is a COPY of each struct element, so the mutation would be silently
// lost. Modern C# refuses to let you write that bug. To actually update a
// List<struct>, REPLACE the whole element by index:
Console.WriteLine("=== Scenario 1a: List<struct> must be mutated by replacement ===");

var structList = new List<PointS> { new() { X = 1 }, new() { X = 2 }, new() { X = 3 } };

for (int i = 0; i < structList.Count; i++)
    structList[i] = new PointS { X = 99 };

foreach (var p in structList)
    Console.WriteLine($"  X = {p.X}");


// === Scenario 1b: same loop, but with a class ==============================
Console.WriteLine();
Console.WriteLine("=== Scenario 1b: foreach on a List<class> mutates through the reference ===");

var classList = new List<PointC> { new() { X = 1 }, new() { X = 2 }, new() { X = 3 } };

foreach (var p in classList)
    p.X = 99;     // works — p is a reference to the shared heap object

foreach (var p in classList)
    Console.WriteLine($"  X = {p.X}");


// === Scenario 2: reassign vs mutate inside a method =========================
Console.WriteLine();
Console.WriteLine("=== Scenario 2: passing a class to a method ===");

var original = new PointC { X = 1 };

ReassignInside(original);
Console.WriteLine($"  after ReassignInside:  original.X = {original.X}");

MutateInside(original);
Console.WriteLine($"  after MutateInside:    original.X = {original.X}");

static void ReassignInside(PointC p) => p = new PointC { X = 999 };
static void MutateInside(PointC p)   => p.X = 999;


// === Scenario 3: records and equality =======================================
Console.WriteLine();
Console.WriteLine("=== Scenario 3: record class vs record struct ===");

var rc1 = new PointRC(42);
var rc2 = new PointRC(42);
Console.WriteLine($"  rc1 == rc2:               {rc1 == rc2}");
Console.WriteLine($"  ReferenceEquals(rc1,rc2): {ReferenceEquals(rc1, rc2)}");

var rs1 = new PointRS(42);
var rs2 = new PointRS(42);
Console.WriteLine($"  rs1 == rs2:               {rs1 == rs2}");


// -----------------------------------------------------------------------------
// 🎯 The three rules you should be able to state in one sentence each:
//    1. Value semantics = each variable holds its own data; mutations don't
//       propagate.
//    2. Reference semantics = multiple variables can point at one object;
//       mutations through ANY of them are visible to ALL of them.
//    3. `record` overrides `==` and GetHashCode to compare by VALUE on all
//       properties — regardless of whether it's a record class or record
//       struct. Use `ReferenceEquals` to bypass the override.
// -----------------------------------------------------------------------------

public struct PointS { public int X; }
public class   PointC { public int X; }

public record class  PointRC(int X);
public record struct PointRS(int X);