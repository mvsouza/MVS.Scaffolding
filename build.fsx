#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target 
nuget FSharp.Data
nuget Fake.Runtime //"

#load ".fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet
open Fake.DotNet.NuGet
open Fake.DotNet.Testing
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators
open FSharp.Data
open System

let artifacts = __SOURCE_DIRECTORY__ + "/artifacts"
let nugetScaffoldingCache = Path.combineTrimEnd (Environment.environVar "HOME") "/.nuget/packages/mvs.scaffolding";;


Target.create "Clean" (fun _ ->
    !! "src/**/bin"
    ++ "src/**/obj"
    ++ "artifacts"
    ++ nugetScaffoldingCache
    |> Shell.cleanDirs 
)

Target.create "Build" (fun _ ->
    !! "src/**/*.*proj"
    |> Seq.iter (DotNet.build id)
)

Target.create "CleanCache" (fun _ ->
    !! nugetScaffoldingCache
    |> Shell.cleanDirs 
)

Target.create "PackTools" (fun _ ->
    !! "src/**/*.*proj"
    |> Seq.iter (DotNet.pack (fun (p) -> 
        { p with 
            OutputPath = Some artifacts
            VersionSuffix = Some "DEV-1"
        }))
)

Target.create "NuGetPush" (fun _ ->
    !! "artifacts/*.nupkg"
    |> Seq.iter (DotNet.nugetPush (fun opt -> 
    opt.WithPushParams(
      { opt.PushParams with 
          ApiKey = Some (Environment.environVarOrFail "NUGET_APIKEY")
          Source = Some "https://api.nuget.org/v3/index.json"
      })
  ))
)


Target.create "Restore" (fun _ ->
    !! "src/**/*.*proj"
    |> Seq.iter (DotNet.restore id)
)

Target.create "All" ignore

Target.create "Cover" ignore

"Packtools"
  ==> "NuGetPush"

"Clean"
  ==> "Restore"
  ==> "Build"
  ==> "Packtools"
  ==> "All"

Target.runOrDefault "All"
