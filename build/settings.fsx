#r "tools/FAKE/tools/FakeLib.dll"
open Fake

let projName="DomainBus.Autofac"
let projDir= "..\src" @@ projName
let testDir="..\src" @@ "Tests"

let localNugetRepo="E:/Libs/nuget"
let nugetExeDir="tools"



