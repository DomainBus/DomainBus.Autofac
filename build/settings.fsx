#r "tools/FAKE/tools/FakeLib.dll"
open Fake

let projName="DomainBus.Autofac"
let projDir= "..\src" @@ projName
let testDir="..\src" @@ "Tests"

let testOnCore=true
let additionalPack=[]

let localNugetRepo="E:/Libs/nuget"
let nugetExeDir="tools"



