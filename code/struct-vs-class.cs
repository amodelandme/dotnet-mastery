// -----------------------------------------------------------------------------
// Sandbox: struct vs class — identity, copies, and the foreach trap
// Section: 1.1
// Run:     dotnet run 01-1-struct-vs-class.cs   (.NET 10+)
// Goal:    Turn "value vs reference" from a phrase into instinct.
// -----------------------------------------------------------------------------
//
// 🔬 HOW TO USE THIS SANDBOX
//    1. Read each scenario. Write down what you THINK it prints BEFORE running.
//    2. Run it. Compare.
//    3. Wrong prediction? Good — that's the lesson. Note it in the journal.
//
// -----------------------------------------------------------------------------
 
using System;
using System.Collections.Generic;
 
 
// === Scenario 1a: foreach over a List<struct> ===============================
Console.WriteLine("=== Scenario 1a: foreach a List<struct> ===");
 
var structList = new List<PointS> { new() { X = 1 }, new() { X = 2 }, new() { X = 3 } };
 
foreach (var p in structList)
    p.X = 99;      // compiles. But does it actually mutate the list?
 
foreach (var p in structList)
    Console.WriteLine($"  X = {p.X}");
 
 
// === Scenario 1b: same loop, but with a class ==============================
Console.WriteLine();
Console.WriteLine("=== Scenario 1b: same loop, but with a class ===");
 
var classList = new List<PointC> { new() { X = 1 }, new() { X = 2 }, new() { X = 3 } };
 
foreach (var p in classList)
    p.X = 99;
 
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
// 🎯 After running — answer in your own words (no googling):
//    1. Why did Scenario 1a print 1, 2, 3 instead of 99, 99, 99?
//    2. Why did ReassignInside NOT change original.X, but MutateInside DID?
//    3. How can rc1 == rc2 be true when rc1 and rc2 are separate instances?
//
// 📝 Prediction journal (fill in AFTER running):
//
//    Scenario 1a  — predicted: _______     actual: _______     right? y/n ___
//    Scenario 1b  — predicted: _______     actual: _______     right? y/n ___
//    Scenario 2   — predicted: _______     actual: _______     right? y/n ___
//    Scenario 3   — predicted: _______     actual: _______     right? y/n ___
// -----------------------------------------------------------------------------
 
public struct PointS { public int X; }
public class   PointC { public int X; }
 
public record class  PointRC(int X);
public record struct PointRS(int X);
 